using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CarTemplate
{
    public class Gearbox : DriveTrain
    {

        public GearboxData data;
        private int currentGear = 0;
        private TransmittedRpm outputRpm = new TransmittedRpm(0f);
        private TransmittedTorque outputTorque = new TransmittedTorque(0f);

        public int CurrentGear
        {
            get { return currentGear; }
        }

        public void IncreaseGear()
        {
            if (currentGear < data.gearRatios.Count - 1)
                currentGear += 1;
        }

        public void DecreaseGear()
        {
            if (currentGear > 0)
            {
                currentGear -= 1;
            }
        }

        protected override void ProcessInputRpm()
        {

            if (data.gearRatios.Count != 0)
            {
                outputRpm.rpm = inputRpm.rpm * data.gearRatios[currentGear] * data.finalGear;
            }
            else
            {
                Debug.LogWarning("Gearbox don't have any gears. Output RPM will be set to zero. Please, set the Gearbox component correctly.");
                outputRpm.rpm = 0.0f;
            }

            rpmOutputDriveTrain.SetInputRpm(outputRpm);

        }

        protected override void ProcessInputTorque()
        {

            if (data.gearRatios.Count != 0)
            {
                outputTorque.torque = inputTorque.torque * data.gearRatios[currentGear] * data.finalGear;
            }
            else
            {
                Debug.LogWarning("Gearbox don't have any gears. Output torque will be set to zero. Please, set the Gearbox component correctly.");
                outputTorque.torque = 0.0f;
            }

            torqueOutputDriveTrain.SetInputTorque(outputTorque);
        }

    }
}

