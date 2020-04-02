using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CarTemplate;

public class AxleDebug : MonoBehaviour
{
    public Car car;

    public WheelInfoDebug FLWheelDebug;
    public WheelInfoDebug FRWheelDebug;
    public WheelInfoDebug RLWheelDebug;
    public WheelInfoDebug RRWheelDebug;

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        Wheel FLWheel = car.frontAxle.leftWheel;
        Wheel FRWheel = car.frontAxle.rightWheel;
        Wheel RLWheel = car.rearAxle.leftWheel;
        Wheel RRWheel = car.rearAxle.rightWheel;

        //Axle.WheelInfo FLWheelInfo = car.frontAxle.LeftWheelInfo;
        //Axle.WheelInfo FRWheelInfo = car.frontAxle.RightWheelInfo;

        //Axle.WheelInfo RLWheelInfo = car.rearAxle.LeftWheelInfo;
        //Axle.WheelInfo RRWheelInfo = car.rearAxle.RightWheelInfo;

        FLWheelDebug.force = FLWheel.collisionInfo.forceOnContact;
        FLWheelDebug.sideSlip = FLWheel.collisionInfo.sidewaySlip;
        FLWheelDebug.forwardSlip = FLWheel.collisionInfo.forwardSlip;

        FRWheelDebug.force = FRWheel.collisionInfo.forceOnContact;
        FRWheelDebug.sideSlip = FRWheel.collisionInfo.sidewaySlip;
        FRWheelDebug.forwardSlip = FRWheel.collisionInfo.forwardSlip;

        RLWheelDebug.force = RLWheel.collisionInfo.forceOnContact;
        RLWheelDebug.sideSlip = RLWheel.collisionInfo.sidewaySlip;
        RLWheelDebug.forwardSlip = RLWheel.collisionInfo.forwardSlip;

        RRWheelDebug.force = RRWheel.collisionInfo.forceOnContact;
        RRWheelDebug.sideSlip = RRWheel.collisionInfo.sidewaySlip;
        RRWheelDebug.forwardSlip = RRWheel.collisionInfo.forwardSlip;


    }
}
