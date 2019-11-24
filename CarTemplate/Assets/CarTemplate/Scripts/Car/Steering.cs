using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CarTemplate
{

    /// <summary>
    /// Steering with Ackermann angle calculations. Ackermann uses the distance between the two axles and the lenght of the rear axle
    /// to get the optimal steering angle for each of the front wheels.
    /// </summary>
    public class Steering
    {
        public Axle axle;
        public float turnRadius = 10f;
        public float wheelBase;
        public float rearAxleTrack;

        private float ackermannAngleLeft;
        private float ackermannAngleRight;
        private float steeringInput = 0;

        /// <summary>
        /// Left wheel angle after ackermann calculations
        /// </summary>
        public float AckermannAngleLeft
        {
            get { return ackermannAngleLeft; }
        }

        /// <summary>
        /// Right wheel angle after ackermann calculations
        /// </summary>
        public float AckermannAngleRight
        {
            get { return ackermannAngleRight; }
        }

        /// <summary>
        /// Current steering input aplied.
        /// </summary>
        public float SteeringInput
        {
            get { return steeringInput; }
        }

        /// <summary>
        /// Change steering angle. Value is a range between -1 (left) and +1 (right).
        /// The maximum turn radius is set on turnRadius variable.
        /// </summary>
        /// <param name="input"></param>
        public void SetSteeringInput (float input)
        {

            steeringInput = input;

            if (axle != null && axle.leftWheel != null && axle.rightWheel != null)
            {

                if (steeringInput > 0) //Turning right
                {
                    ackermannAngleLeft = Mathf.Rad2Deg * Mathf.Atan(wheelBase / (turnRadius + (rearAxleTrack / 2))) * steeringInput;
                    ackermannAngleRight = Mathf.Rad2Deg * Mathf.Atan(wheelBase / (turnRadius - (rearAxleTrack / 2))) * steeringInput;
                }
                else if (steeringInput < 0) //Turning left
                {
                    ackermannAngleLeft = Mathf.Rad2Deg * Mathf.Atan(wheelBase / (turnRadius - (rearAxleTrack / 2))) * steeringInput;
                    ackermannAngleRight = Mathf.Rad2Deg * Mathf.Atan(wheelBase / (turnRadius + (rearAxleTrack / 2))) * steeringInput;
                }
                else //No steer angle
                {
                    ackermannAngleLeft = 0f;
                    ackermannAngleRight = 0f;
                }

                axle.leftWheel.steerAngle = ackermannAngleLeft;
                axle.rightWheel.steerAngle = ackermannAngleRight;

            }
            else
            {
                Debug.LogWarning("Steering doesn't have a axle assigned or wheels are missing! Won't change steering.");
            }
        }
    }

}
