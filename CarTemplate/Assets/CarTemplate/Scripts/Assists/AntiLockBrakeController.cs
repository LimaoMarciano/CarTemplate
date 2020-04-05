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

    private AntiLockBrakeSensor FLAbs;
    private AntiLockBrakeSensor FRAbs;
    private AntiLockBrakeSensor RLAbs;
    private AntiLockBrakeSensor RRAbs;

    private bool isAbsActive = false;

    public bool IsAbsActive
    {
        get { return isAbsActive; }
    }

    public AntiLockBrakeSensor FrontLeftAbs
    {
        get { return FLAbs; }
    }

    public AntiLockBrakeSensor FrontRightAbs
    {
        get { return FRAbs; }
    }

    public AntiLockBrakeSensor RearLeftAbs
    {
        get { return RLAbs; }
    }

    public AntiLockBrakeSensor RearRightAbs
    {
        get { return RRAbs; }
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

        FLAbs = new AntiLockBrakeSensor(FLWheel, slipLimit, refreshTime, brakeChangePerRefresh);
        FRAbs = new AntiLockBrakeSensor(FRWheel, slipLimit, refreshTime, brakeChangePerRefresh);
        RLAbs = new AntiLockBrakeSensor(RLWheel, slipLimit, refreshTime, brakeChangePerRefresh);
        RRAbs = new AntiLockBrakeSensor(RRWheel, slipLimit, refreshTime, brakeChangePerRefresh);
    }

    // Update is called once per frame
    public void ApplyBrakes(float brakeInput, float handbrakeInput)
    {
        //float FLBrakeInput = FLAbs.Update(brakeInput);
        //car.brakes.ApplyPressureToIndividualWheel(CarAxle.front, AxleWheel.left, FLBrakeInput, handbrakeInput);

        //float FRBrakeInput = FRAbs.Update(brakeInput);
        //car.brakes.ApplyPressureToIndividualWheel(CarAxle.front, AxleWheel.right, FRBrakeInput, handbrakeInput);

        //float RLBrakeInput = RLAbs.Update(brakeInput);
        //car.brakes.ApplyPressureToIndividualWheel(CarAxle.rear, AxleWheel.left, RLBrakeInput, handbrakeInput);

        //float RRBrakeInput = RRAbs.Update(brakeInput);
        //car.brakes.ApplyPressureToIndividualWheel(CarAxle.rear, AxleWheel.right, RRBrakeInput, handbrakeInput);

        float FLBrakeInput = FLAbs.Update(brakeInput);
        float FRBrakeInput = FRAbs.Update(brakeInput);
        float RLBrakeInput = RLAbs.Update(brakeInput);
        float RRBrakeInput = RRAbs.Update(brakeInput);

        car.brakes.ApplyPressure(FLBrakeInput, FRBrakeInput, RLBrakeInput, RRBrakeInput, handbrakeInput);

        if (FLAbs.IsActive == true || FRAbs.IsActive == true || RLAbs.IsActive == true || RRAbs.IsActive == true)
        {
            isAbsActive = true;
        }
        else
        {
            isAbsActive = false;
        }

    }
}
