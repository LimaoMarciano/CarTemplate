using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CarTemplate
{
    public class Clutch : DriveTrain
    {
        public float grip = 10f;
        public float clutchInput = 0f;

        private TransmittedRpm outputRpm = new TransmittedRpm(0f);
        private TransmittedTorque outputTorque = new TransmittedTorque(0f);

        protected override void ProcessInputRpm()
        {

            if (inputRpm.connectionSlip == 0)
            {
                outputRpm.connectionSlip = clutchInput;
            }
            else
            {
                outputRpm.connectionSlip = inputRpm.connectionSlip;
            }
            

            float rpmDifference = inputRpm.rpm - outputRpm.rpm;
            outputRpm.rpm += rpmDifference * grip * (1f - clutchInput) * Time.deltaTime;

            rpmOutputDriveTrain.SetInputRpm(outputRpm);
        }

        protected override void ProcessInputTorque()
        {
            outputTorque.torque = inputTorque.torque * (1f - clutchInput);

            torqueOutputDriveTrain.SetInputTorque(outputTorque);
        }

    }
}

