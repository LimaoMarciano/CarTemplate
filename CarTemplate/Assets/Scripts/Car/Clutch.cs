using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CarTemplate
{
    /// <summary>
    /// The clutch disconnects engine and gearbox to allow a gear change.
    /// <para>When not fully depressed, it'll slip and progressively match the rpm of both, while losing torque transfer efficiency.
    /// The grip variable defines how fast the clutch will match engine and gearbox RPM</para>
    /// <para>When the clutch is not depressed, it will simply pass RPM and torque through</para>
    /// </summary>
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

