using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engine
{

    //Engine settings
    public AnimationCurve powerCurve;
    public float maxRpm = 7000.0f;
    public float minRpm = 1000.0f;
    public float peakPower = 150.0f;
    public float engineDelay = 0.1f;
    //public float engineInertia = 0.05f;
    //public float engineDrag = 0.01f;
    //public float engineBrake = 200f;

    private float engineProtectionCutoff = 0.02f;
    private float engineProtectionTimer = 0.0f;
    private bool isProtectionOn = false;

    private float engineRpm = 0.0f;
    private float targetRpm = 0.0f;
    public bool isEngaged = false; 

    private float engineSpeed = 0.0f;

    //Inputs
    public float transmissionConnection = 1.0f;
    private float acceleratorInput = 0.0f;
    private float inputRpm = 0.0f;

    //Outputs
    private float outputTorque = 0.0f;
    

    private float smoothDampVelocity = 0.0f;
    
    public float EngineRpm
    {
        get { return engineRpm; }
    }

    public float OutputTorque
    {
        get { return outputTorque; }
    }

    //Constructor
    public Engine (EngineData data)
    {
        powerCurve = data.torqueCurve;
        maxRpm = data.maxRpm;
        minRpm = data.minRpm;
        peakPower = data.peakPower;
    }

    public void EngineUpdate ()
    {

        if (engineRpm > (maxRpm - 100))
        {
            isProtectionOn = true;
            engineProtectionTimer = 0f;
        }

        if (isProtectionOn)
        {
            engineProtectionTimer += Time.deltaTime;
            if (engineProtectionTimer >= engineProtectionCutoff)
            {
                isProtectionOn = false;
            }
        }

        //Hack to simulate neutral gear
        if (isEngaged)
        {
            targetRpm = Mathf.Lerp(maxRpm * acceleratorInput, inputRpm, transmissionConnection);
        }
        else
        {
            targetRpm = maxRpm * acceleratorInput;
        }

        engineRpm = Mathf.SmoothDamp(engineRpm, targetRpm, ref smoothDampVelocity, engineDelay);

        outputTorque = GetTorqueFromRpm(engineRpm);
        engineRpm = Mathf.Clamp(engineRpm, minRpm, maxRpm);

        //outputTorque = GetTorqueFromRpm(engineRpm);

        //float engineAcc = (outputTorque / engineInertia);

        //engineRpm = ((engineRpm + engineAcc * Time.deltaTime)) * (1 - Time.deltaTime * engineDrag);



    }

    public void SetAcceleratorInput(float accelerator)
    {
        if (isProtectionOn)
        {
            acceleratorInput = 0.0f;
        }
        else
        {
        acceleratorInput = accelerator;
        }
    }

    public float GetTorqueFromRpm (float rpm)
    {
        
        float t = Mathf.Clamp(rpm, minRpm, maxRpm) / maxRpm;

        float torque;

        float power = peakPower * powerCurve.Evaluate(t);
        torque = (9.5488f * power * 1000) / Mathf.Clamp(rpm, minRpm, maxRpm);
        torque *= acceleratorInput;

        //if (acceleratorInput > 0.0f)
        //{
        //    float power = peakPower * powerCurve.Evaluate(t);
        //    torque = (9.5488f * power * 1000) / Mathf.Clamp(rpm, minRpm, maxRpm);
        //    torque *= acceleratorInput;
        //}
        //else
        //{
        //    if (rpm > minRpm)
        //    {
        //        torque = -engineBrake * t;
        //    }
        //    else
        //    {
        //        torque = 0;
        //    }
            
        //}
        

        return torque;

    }

    public void SetInputRpm (float rpm)
    {
        inputRpm = rpm;
    }
}
