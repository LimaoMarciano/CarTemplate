using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace CarTemplate
{
    [Serializable]
    public class Axle
    {
        public WheelCollider leftWheel;
        public WheelCollider rightWheel;

        /// <summary>
        /// Returns the axle mid point position
        /// </summary>
        /// <returns></returns>
        public Vector3 GetAxleMidPoint ()
        {
            Vector3 track = (leftWheel.transform.position + rightWheel.transform.position) / 2;
            return track;
        }

        /// <summary>
        /// Returns distance between the two wheels in a axle
        /// </summary>
        /// <returns></returns>
        public float GetAxleTrack ()
        {
            return Vector3.Distance(leftWheel.transform.position, rightWheel.transform.position);
        }

    }

}
