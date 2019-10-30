using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CarTemplate
{

    public class Steering
    {
        public Axle axle;
        public float turnRadius = 10f;
        public float wheelBase;
        public float rearAxleTrack;

        private float ackermannAngleLeft;
        private float ackermannAngleRight;

        public void SetSteeringInput (float input)
        {
            
            if (input > 0) //Turning right
            {
                ackermannAngleLeft = Mathf.Rad2Deg * Mathf.Atan(wheelBase / (turnRadius + (rearAxleTrack / 2))) * input;
                ackermannAngleRight = Mathf.Rad2Deg * Mathf.Atan(wheelBase / (turnRadius - (rearAxleTrack / 2))) * input;
            }
            else if (input < 0) //Turning left
            {
                ackermannAngleLeft = Mathf.Rad2Deg * Mathf.Atan(wheelBase / (turnRadius - (rearAxleTrack / 2))) * input;
                ackermannAngleRight = Mathf.Rad2Deg * Mathf.Atan(wheelBase / (turnRadius + (rearAxleTrack / 2))) * input;
            }
            else //No steer angle
            {
                ackermannAngleLeft = 0f;
                ackermannAngleRight = 0f;
            }

            axle.leftWheel.steerAngle = ackermannAngleLeft;
            axle.rightWheel.steerAngle = ackermannAngleRight;
        }
    }

}
