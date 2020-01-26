using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CarTemplate
{

    public class Differential : DriveTrain
    {
        public float lockStrenght = 0f;
        protected float speed;
        protected float torqueSplit;
        protected float rpmDifferenceRatio;

        protected TransmittedRpm outputRpm = new TransmittedRpm(0f);

        //Public getters
        //=================================================================================================

        /// <summary>
        /// Speed measured in m/s from differential RPM
        /// </summary>
        public float Speed
        {
            get { return speed; }
        }

        /// <summary>
        /// Torque distribution between the driven axle wheels. 
        /// (0 = 100% torque on left, 1 = 100% torque on right)
        /// </summary>
        public float TorqueSplit
        {
            get { return torqueSplit; }
        }

        /// <summary>
        /// RPM difference ratio between the driven axle wheels.
        /// Positive values means that right wheel is faster than left one.
        /// </summary>
        public float RpmDifferenceRatio
        {
            get { return rpmDifferenceRatio; }
        }

        public float Rpm
        {
            get { return outputRpm.rpm; }
        }

        public virtual void Update()
        {
            if (rpmOutputDriveTrain != null)
            {
                rpmOutputDriveTrain.SetInputRpm(new TransmittedRpm(0f));
            }
            else
            {
                Debug.LogWarning("Differential doesn't have a RPM output. Won't transmit RPM.");
            }
        }

        protected float CalculateTorqueSplit (float rpm1, float rpm2)
        {
            float split = 0.5f;

            //Calculate rpm difference between the two values
            //Negative values means that rpm1 wheel is faster than rpm2
            if (rpm1 != 0 || rpm2 != 0)
            {
                rpmDifferenceRatio = (rpm1 - rpm2) / (rpm1 + rpm2);
            }
            else
            {
                rpmDifferenceRatio = 0;
            }

            //Calculating the torque proportion that each element will receive. 1 = 100% rpm2 element, 0 = 100% rpm1 element
            //Lock strenght defines if torque goes to most slipping wheel (open diff) or to less slipping wheel (locked diff)
            float bias = Mathf.Clamp(rpmDifferenceRatio, -1, 1) * 0.5f;
            split = 0.5f + (bias * Mathf.Clamp(lockStrenght, -1, 1));

            return split;
        }
    }
}
