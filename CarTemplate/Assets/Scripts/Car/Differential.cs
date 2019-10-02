using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CarTemplate
{
    public class Differential : DriveTrain
    {
        public WheelCollider[] wheels;

        public void Update()
        {

            float totalWheelsRPM = 0;
            for (int i = 0; i < wheels.Length; i++)
            {
                totalWheelsRPM += wheels[i].rpm;
            }
            float averageRPM = totalWheelsRPM / wheels.Length;

            outputRPM = averageRPM;

            SendRPMOutput();
        }

        protected override void ProcessTorque()
        {
            for (int i = 0; i < wheels.Length; i++)
            {
                wheels[i].motorTorque = inputTorque / wheels.Length;
            }
        }
    }
}

