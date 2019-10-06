using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CarTemplate
{
    public class Differential : DriveTrain
    {
        public WheelCollider[] wheels;

        private TransmittedRpm outputRpm = new TransmittedRpm(0f);

        public void Update()
        {

            float totalWheelsRpm = 0;
            for (int i = 0; i < wheels.Length; i++)
            {
                totalWheelsRpm += wheels[i].rpm;
            }
            float averageRpm = totalWheelsRpm / wheels.Length;

            outputRpm.rpm = averageRpm;

            rpmOutputDriveTrain.SetInputRpm(outputRpm);
        }

        protected override void ProcessInputTorque()
        {
            for (int i = 0; i < wheels.Length; i++)
            {
                wheels[i].motorTorque = inputTorque.torque / wheels.Length;
            }
        }
    }
}

