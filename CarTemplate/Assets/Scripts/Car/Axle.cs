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

        public WheelInfo LeftWheelInfo
        {
            get { return GetWheelInfo(leftWheel); }
        }

        public WheelInfo RightWheelInfo
        {
            get { return GetWheelInfo(rightWheel); }
        }

        public struct WheelInfo
        {
            public float sidewaySlip;
            public float forwardSlip;
            public float forceOnContact;
            public PhysicMaterial contactMaterial;
        }

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

        WheelInfo GetWheelInfo (WheelCollider wheel)
        {
            WheelInfo wheelInfo;
            WheelHit hit = new WheelHit();

            if (wheel.GetGroundHit(out hit))
            {
                wheelInfo.forwardSlip = hit.forwardSlip;
                wheelInfo.sidewaySlip = hit.sidewaysSlip;
                wheelInfo.forceOnContact = hit.force;
                wheelInfo.contactMaterial = hit.collider.sharedMaterial;
            }
            else
            {
                wheelInfo.forwardSlip = 0;
                wheelInfo.sidewaySlip = 0;
                wheelInfo.forceOnContact = 0;
                wheelInfo.contactMaterial = null;
            }

            return wheelInfo;
        }

    }

}
