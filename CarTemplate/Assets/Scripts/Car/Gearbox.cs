using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CarTemplate
{
    /// <summary>
    /// The gearbox allows to multiply the received torque while losing speed and vice-versa through different gear ratios.
    /// <para>A common car have several gears, with the lower ones amplifying torque and the higher ones
    /// aiming for high speed.</para>
    /// <para>The gear ratios values are crucial to tweak car acceleration and top speed.</para>
    /// <para>It's possible to create as many gears you wish, with configurable ratios. The only fixed gears are neutral and reverse.</para>
    /// </summary>
    public class Gearbox : DriveTrain
    {

        public GearboxData data;
        private int currentGear = 0;
        private TransmittedRpm outputRpm = new TransmittedRpm(0f);
        private TransmittedTorque outputTorque = new TransmittedTorque(0f);

        /// <summary>
        /// The current selected gear.
        /// <para>Reverse = -2, Neutral = -1, 1st Gear = 0</para>
        /// </summary>
        public int CurrentGear
        {
            get { return currentGear; }
        }

        /// <summary>
        /// Increase current gear by one. It won't increase if it's on last gear.
        /// </summary>
        public void IncreaseGear()
        {
            if (currentGear < data.gearRatios.Count - 1)
                currentGear += 1;
        }

        /// <summary>
        /// Decreases current gear by one. It's won't decrease below reverse gear.
        /// </summary>
        public void DecreaseGear()
        {
            if (currentGear > -2)
            {
                currentGear -= 1;
            }
        }

        /// <summary>
        /// Set directly to a specific gear. If the value is off range, it'll be clamped between -2 (reverse) and the last gear.
        /// </summary>
        /// <param name="gear"></param>
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

