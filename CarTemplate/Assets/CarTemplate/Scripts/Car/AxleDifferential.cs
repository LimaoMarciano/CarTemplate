using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CarTemplate
{
    /// <summary>
    /// The differential is responsible for splitting the engine torque between the two axle wheels.
    /// A car may have more than one differential if it's a all-wheel-drive.
    /// <para>The implementation here is similar to what would be a limited slip differential. 
    /// Lock strenght defines if it's more open (value -1) or more locked (value 1)</para>
    /// </summary>
    public class AxleDifferential : Differential
    {
        public WheelCollider[] wheels;
        public Axle axle;
        //public float lockStrenght = 0f;

        //private TransmittedRpm outputRpm = new TransmittedRpm(0f);
        //private float speed;
        //private float torqueSplit;
        //private float rpmDifferenceRatio;

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
        /// Torque distribution between the driven axle wheels. 
        /// (0 = 100% torque on left, 1 = 100% torque on right)
        /// </summary>
        //public float TorqueSplit
        //{
        //    get { return torqueSplit; }
        //}

        /// <summary>
        /// RPM difference ratio between the driven axle wheels.
        /// Positive values means that right wheel is faster than left one.
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

            if (axle != null && axle.leftWheel != null && axle.rightWheel != null)
            {
                outputRpm.rpm = (axle.rightWheel.collider.rpm + axle.leftWheel.collider.rpm) / 2f;
                speed = CalculateSpeed(outputRpm.rpm, axle.rightWheel.collider.radius);
            }
            else
            {
                Debug.LogWarning("Differential doesn't have a axle assigned or wheels are missing! Output RPM will be zero.");
                outputRpm.rpm = 0f;
                speed = 0;
            }

            if (rpmOutputDriveTrain != null)
            {
                rpmOutputDriveTrain.SetInputRpm(outputRpm);
            }
            else
            {
                Debug.LogWarning("Differential doesn't have a RPM output. Won't transmit RPM.");
            }
            
        }

        protected override void ProcessInputTorque()
        {

            if (axle != null && axle.leftWheel != null && axle.rightWheel != null)
            {
                
                //Calculate rpm difference between the two wheels
                //Negative values means that right wheel is faster than left
                float leftWheelRpm = axle.leftWheel.collider.rpm;
                float rightWheelRpm = axle.rightWheel.collider.rpm;

                torqueSplit = CalculateTorqueSplit(rightWheelRpm, leftWheelRpm);
                axle.leftWheel.collider.motorTorque = inputTorque.torque * torqueSplit;
                axle.rightWheel.collider.motorTorque = inputTorque.torque * (1 - torqueSplit);

                //if (leftWheelRpm != 0 || rightWheelRpm != 0)
                //{
                //    rpmDifferenceRatio = (rightWheelRpm - leftWheelRpm) / (rightWheelRpm + leftWheelRpm);
                //}
                //else
                //{
                //    rpmDifferenceRatio = 0;
                //}

                ////Calculating the torque proportion that each wheel will receive. 1 = 100% left wheel, 0 = 100% right wheel
                ////Lock strenght defines if torque goes to most slipping wheel (open diff) or to less slipping wheel (locked diff)
                //float bias = Mathf.Clamp(rpmDifferenceRatio, -1, 1) * 0.5f;
                //torqueSplit = 0.5f + (bias * Mathf.Clamp(lockStrenght, -1, 1));

                //axle.leftWheel.motorTorque = inputTorque.torque * torqueSplit;
                //axle.rightWheel.motorTorque = inputTorque.torque * (1 - torqueSplit);

            }
            else
            {
                Debug.LogWarning("Differential doesn't have a axle assigned or wheels are missing! Can't transfer torque to wheels.");
            }


        }

        /// <summary>
        /// Calculates speed in m/s based on rpm and wheel radius
        /// </summary>
        /// <param name="rpm">Wheel RPM</param>
        /// <param name="wheelRadius">Wheel radius</param>
        /// <returns>Returns speed in meters per second</returns>
        private float CalculateSpeed(float rpm, float wheelRadius)
        {
            float wheelCircunference = wheelRadius * 2.0f * Mathf.PI;
            float speed = (rpm * wheelCircunference) / 60.0f;

            return speed;
        }

    }
}

