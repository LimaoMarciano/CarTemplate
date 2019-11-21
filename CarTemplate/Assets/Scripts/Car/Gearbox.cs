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
            if (currentGear > -2)
            {
                currentGear -= 1;
            }
        }

        public void SetCurrentGear(int gear)
        {
            currentGear = Mathf.Clamp(gear, -2, data.gearRatios.Count - 1);
        }

        protected override void ProcessInputRpm()
        {

            if (data.gearRatios.Count != 0)
            {
                

                //If gear ratio is zero, it means it's neutral. If it's neutral, then set connection slip to 1, meaning that no power is beign transmitted
                //This is necessary to simulate engine free spinning when on neutral
                if (currentGear == -1)  
                {
                    //Neutral
                    outputRpm.connectionSlip = 1f;
                    outputRpm.rpm = 0f;
                }
                else if (currentGear == -2) 
                {
                    //Reverse
                    outputRpm.connectionSlip = 0f;
                    outputRpm.rpm = inputRpm.rpm * -data.reverseGearRatio * data.finalGear;
                }
                else
                {
                    //Normal gears
                    outputRpm.connectionSlip = 0f;
                    outputRpm.rpm = inputRpm.rpm * data.gearRatios[currentGear] * data.finalGear;
                }
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
                if (currentGear == -1)
                {
                    //Neutral
                    outputTorque.torque = 0;
                }
                else if (currentGear == -2)
                {
                    //Reverse
                    outputTorque.torque = inputTorque.torque * -data.reverseGearRatio * data.finalGear;
                }
                else
                {
                    //Normal gears
                    outputTorque.torque = inputTorque.torque * data.gearRatios[currentGear] * data.finalGear;
                }

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

