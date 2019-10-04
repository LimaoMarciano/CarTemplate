using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CarTemplate
{
    public class Gearbox : DriveTrain
    {

        public GearboxData data;
        private int currentGear = 0;

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

        protected override void ProcessRPM()
        {

            if (data.gearRatios.Count != 0)
            {
                outputRPM = inputRPM * data.gearRatios[currentGear];
            }
            else
            {
                Debug.LogWarning("Gearbox don't have any gears. Output RPM will be set to zero. Please, set the Gearbox component correctly.");
                outputRPM = 0.0f;
            }

            SendOutputRPM();

        }

        protected override void ProcessTorque()
        {
            if (data.gearRatios.Count != 0)
            {
                outputTorque = inputTorque * data.gearRatios[currentGear];
            }
            else
            {
                Debug.LogWarning("Gearbox don't have any gears. Output torque will be set to zero. Please, set the Gearbox component correctly.");
                outputTorque = 0.0f;
            }

            SendOutputTorque();
        }

    }
}

