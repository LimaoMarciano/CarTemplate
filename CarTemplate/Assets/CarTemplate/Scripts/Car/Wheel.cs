using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace CarTemplate
{
    [Serializable]
    public class Wheel
    {
        public WheelCollider collider;

        public WheelInfo collisionInfo
        {
            get { return GetWheelInfo(); }
        }

        /// <summary>
        /// Wheel collision information. (forward and sideway slip, force on contact and detected physics material)
        /// </summary>
        public struct WheelInfo
        {
            public float sidewaySlip;
            public float forwardSlip;
            public float forceOnContact;
            public PhysicMaterial contactMaterial;
        }

        /// <summary>
        /// Returns wheel collision information
        /// </summary>
        /// <returns>Returns a struct with wheel collision hit info</returns>
        private WheelInfo GetWheelInfo()
        {
            WheelInfo wheelInfo = new WheelInfo();
            WheelHit hit = new WheelHit();

            if (collider.GetGroundHit(out hit))
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
