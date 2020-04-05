using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CarTemplate;

public class AntiLockBrakeSensor 
{

    public float slipLimit = 0.5f;
    public float refreshTime = 0.06f;
    public float brakeChange = 0.05f;

    private float brakeChangeBoost = 0.5f;

    public Wheel wheel;
    private float timer;
    private float currentSlip;
    private float allowedBrakePressure = 1f;

    private bool isActive = false;

    public bool IsActive
    {
        get { return isActive; }
    }

    public float AllowedBrakePressure
    {
        get { return allowedBrakePressure; }
    }

    public AntiLockBrakeSensor (Wheel wheel, float slipLimit, float refreshTime, float brakeChange)
    {
        this.wheel = wheel;
        this.slipLimit = slipLimit;
        this.refreshTime = refreshTime;
        this.brakeChange = brakeChange;
    }


    public float Update(float input)
    {

        if (input != 0)
        {

            timer += Time.deltaTime;

            if (timer >= refreshTime)
            {
                currentSlip = Mathf.Abs(wheel.collisionInfo.forwardSlip);
                if (currentSlip >= slipLimit)
                {
                    //Add more change rate if slip is too much beyond the limit
                    float overLimitSlip = currentSlip - slipLimit;
                    float additionalChange = Mathf.Lerp(0, brakeChangeBoost, overLimitSlip);

                    allowedBrakePressure -= brakeChange + additionalChange;
                    isActive = true;
                }
                else
                {
                    allowedBrakePressure += brakeChange;
                    isActive = false;
                }

                allowedBrakePressure = Mathf.Clamp01(allowedBrakePressure);
                timer = 0;

            }
        }
        else
        {
            allowedBrakePressure = 1f;
            isActive = false;
            timer = 0;
        }

        return Mathf.Clamp(input, 0, allowedBrakePressure);
    }
}
