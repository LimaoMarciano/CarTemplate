using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CarTemplate
{

    public class CenterDifferential : Differential
    {
        public AxleDifferential frontDifferential;
        public AxleDifferential rearDifferential;
        //public float lockStrenght = 0f;

        //private TransmittedRpm outputRpm = new TransmittedRpm(0f);

        //private float speed;
        //private float torqueSplit;
        //private float rpmDifferenceRatio;

        private float frontTorque = 0f;
        private float rearTorque = 0f;

        //Public getters
        //=================================================================================================

        /// <summary>
        /// Speed measured in m/s from differential RPM
        /// </summary>
        //public float Speed
        //{
        //    get { return speed; }
        //}

        /// <summary>
        /// Torque distribution between the differentials. 
        /// (0 = 100% torque on rear, 1 = 100% torque on front)
        /// </summary>
        //public float TorqueSplit
        //{
        //    get { return torqueSplit; }
        //}

        /// <summary>
        /// RPM difference ratio between the front and rear differentials.
        /// Positive values means that front diff is faster than the rear.
        /// </summary>
        //public float RpmDifferenceRatio
        //{
        //    get { return rpmDifferenceRatio; }
        //}

        //public float Rpm
        //{
        //    get { return outputRpm.rpm; }
        //}

        //Methods
        //=================================================================================================
        public override void Update()
        {
            if (frontDifferential != null && rearDifferential != null)
            {
                frontDifferential.Update();
                rearDifferential.Update();

                outputRpm.rpm = (frontDifferential.Rpm + rearDifferential.Rpm) / 2f;

                speed = (frontDifferential.Speed + rearDifferential.Speed) / 2f;
            }
            else
            {
                Debug.LogWarning("Center differential doesn't have a front and/or rear differential assigned. Output RPM will be zero");
                outputRpm.rpm = 0f;
                speed = 0;
            }

            if (rpmOutputDriveTrain != null)
            {
                rpmOutputDriveTrain.SetInputRpm(outputRpm);
            }
            else
            {
                Debug.LogWarning("Center differential doesn't have a RPM output. Won't transmit RPM");
            }

        }

        protected override void ProcessInputTorque()
        {
            
            if (frontDifferential != null && rearDifferential != null)
            {


                torqueSplit = CalculateTorqueSplit(frontDifferential.Rpm, rearDifferential.Rpm);

                //Calculate rpm difference between the two differentials
                //Negative values means that front is faster than rear
                //if (frontDifferential.Rpm != 0 || rearDifferential.Rpm != 0)
                //{
                //    rpmDifferenceRatio = (frontDifferential.Rpm - rearDifferential.Rpm) / (frontDifferential.Rpm + rearDifferential.Rpm);
                //}
                //else {
                //    rpmDifferenceRatio = 0f;
                //}
                
                //Calculating the torque proportion that each differential will receive. 1 = 100% rear differential, 0 = 100% front differential
                //Lock strenght defines if torque goes to most slipping differential (open diff) or to less slipping differential (locked diff)
                //float bias = Mathf.Clamp(rpmDifferenceRatio, -1, 1) * 0.5f;
                //torqueSplit = 0.5f + (bias * Mathf.Clamp(lockStrenght, -1, 1));

                rearTorque = inputTorque.torque * torqueSplit;
                rearDifferential.SetInputTorque(new TransmittedTorque(rearTorque));

                frontTorque = inputTorque.torque * (1 - torqueSplit);
                frontDifferential.SetInputTorque(new TransmittedTorque(frontTorque));

            }
            else
            {
                Debug.LogWarning("Center differential doesn't have a front and/or rear differential assigned. Won't transmit torque.");
            }


        }

    }

}
