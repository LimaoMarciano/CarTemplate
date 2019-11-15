using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CarTemplate
{
    public class Brakes
    {
        public Axle frontAxle;
        public Axle rearAxle;

        public BrakesData data;
        public float brakeBias = 0.5f;

        public void ApplyPressure (float brakeInput, float handbrakeInput)
        {

            brakeBias = Mathf.Clamp01(brakeBias);

            float frontPower = (data.frontBrakePower * brakeBias * brakeInput);
            float rearPower = (data.rearBrakePower * (1 - brakeBias) * brakeInput) + (data.handbrakePower * handbrakeInput);

            frontAxle.leftWheel.brakeTorque = frontPower;
            frontAxle.rightWheel.brakeTorque = frontPower;
            rearAxle.leftWheel.brakeTorque = rearPower;
            rearAxle.rightWheel.brakeTorque = rearPower;
        }

    }


}
