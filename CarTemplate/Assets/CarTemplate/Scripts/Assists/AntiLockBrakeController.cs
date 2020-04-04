using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CarTemplate;

public class AntiLockBrakeController
{

    public float slipLimit = 0.5f;
    public float refreshTime = 0.06f;
    public float brakeChangePerRefresh = 0.05f;

    private Car car;
    private Wheel FLWheel;
    private Wheel FRWheel;
    private Wheel RLWheel;
    private Wheel RRWheel;

    private AntiLockBrake FLAbs;
    private AntiLockBrake FRAbs;
    private AntiLockBrake RLAbs;
    private AntiLockBrake RRAbs;

    private float averageBrakeInput = 0f;
    private bool isAbsActive = false;

    public float AverageBrakeInput
    {
        get { return averageBrakeInput; }
    }

    public bool IsAbsActive
    {
        get { return isAbsActive; }
    }
    
    public AntiLockBrakeController (float slipLimit, float refreshTime, float brakeChangePerRefresh)
    {
        this.slipLimit = slipLimit;
        this.refreshTime = refreshTime;
        this.brakeChangePerRefresh = brakeChangePerRefresh;
    }

    // Start is called before the first frame update
    public void Init(Car car)
    {
        this.car = car;
        FLWheel = car.frontAxle.leftWheel;
        FRWheel = car.frontAxle.rightWheel;
        RLWheel = car.rearAxle.leftWheel;
        RRWheel = car.rearAxle.rightWheel;

        FLAbs = new AntiLockBrake(FLWheel, slipLimit, refreshTime, brakeChangePerRefresh);
        FRAbs = new AntiLockBrake(FRWheel, slipLimit, refreshTime, brakeChangePerRefresh);
        RLAbs = new AntiLockBrake(RLWheel, slipLimit, refreshTime, brakeChangePerRefresh);
        RRAbs = new AntiLockBrake(RRWheel, slipLimit, refreshTime, brakeChangePerRefresh);
    }

    // Update is called once per frame
    public void Update(float brakeInput, float handbrakeInput)
    {
        float FLBrakeInput = FLAbs.Update(brakeInput);
        car.brakes.ApplyPressureToIndividualWheel(CarAxle.front, AxleWheel.left, FLBrakeInput, handbrakeInput);

        float FRBrakeInput = FRAbs.Update(brakeInput);
        car.brakes.ApplyPressureToIndividualWheel(CarAxle.front, AxleWheel.right, FRBrakeInput, handbrakeInput);

        float RLBrakeInput = RLAbs.Update(brakeInput);
        car.brakes.ApplyPressureToIndividualWheel(CarAxle.rear, AxleWheel.left, RLBrakeInput, handbrakeInput);

        float RRBrakeInput = RRAbs.Update(brakeInput);
        car.brakes.ApplyPressureToIndividualWheel(CarAxle.rear, AxleWheel.right, RRBrakeInput, handbrakeInput);

        if (FLAbs.IsActive == true || FRAbs.IsActive == true || RLAbs.IsActive == true || RRAbs.IsActive == true)
        {
            isAbsActive = true;
        }
        else
        {
            isAbsActive = false;
        }

        averageBrakeInput = (FLBrakeInput + FRBrakeInput + RLBrakeInput + RRBrakeInput) / 4f;

    }
}
