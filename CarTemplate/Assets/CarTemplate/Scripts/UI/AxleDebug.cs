using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CarTemplate;

public class AxleDebug : MonoBehaviour
{
    public Car car;

    public WheelInfoDebug FLWheel;
    public WheelInfoDebug FRWheel;
    public WheelInfoDebug RLWheel;
    public WheelInfoDebug RRWheel;

    // Update is called once per frame
    void Update()
    {

        Axle.WheelInfo FLWheelInfo = car.frontAxle.LeftWheelInfo;
        Axle.WheelInfo FRWheelInfo = car.frontAxle.RightWheelInfo;

        Axle.WheelInfo RLWheelInfo = car.rearAxle.LeftWheelInfo;
        Axle.WheelInfo RRWheelInfo = car.rearAxle.RightWheelInfo;

        FLWheel.force = FLWheelInfo.forceOnContact;
        FLWheel.sideSlip = FLWheelInfo.sidewaySlip;
        FLWheel.forwardSlip = FLWheelInfo.forwardSlip;

        FRWheel.force = FRWheelInfo.forceOnContact;
        FRWheel.sideSlip = FRWheelInfo.sidewaySlip;
        FRWheel.forwardSlip = FRWheelInfo.forwardSlip;

        RLWheel.force = RLWheelInfo.forceOnContact;
        RLWheel.sideSlip = RLWheelInfo.sidewaySlip;
        RLWheel.forwardSlip = RLWheelInfo.forwardSlip;

        RRWheel.force = RRWheelInfo.forceOnContact;
        RRWheel.sideSlip = RRWheelInfo.sidewaySlip;
        RRWheel.forwardSlip = RRWheelInfo.forwardSlip;


    }
}
