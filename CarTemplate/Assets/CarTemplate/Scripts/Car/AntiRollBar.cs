using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CarTemplate
{

    /// <summary>
    /// The anti roll bar connects the two wheels on an axle, creating resistance to difference between both wheels' suspension compressions.
    /// <para>It's a crucial part for cornering stability, avoiding excessive body roll</para>
    /// </summary>
    public class AntiRollBar
    {

        public Axle axle;
        public Rigidbody rb;
        public float strength = 5000f;

        public void Update ()
        {
            WheelHit hit;
            float travelL = 1.0f;
            float travelR = 1.0f;

            if (axle != null && axle.leftWheel != null && axle.rightWheel != null)
            {

                bool groundedL = axle.leftWheel.GetGroundHit(out hit);
                if (groundedL)
                {
                    travelL = (-axle.leftWheel.transform.InverseTransformPoint(hit.point).y - axle.leftWheel.radius) / axle.leftWheel.suspensionDistance;
                }

                bool groundedR = axle.rightWheel.GetGroundHit(out hit);
                if (groundedR)
                {
                    travelR = (-axle.rightWheel.transform.InverseTransformPoint(hit.point).y - axle.rightWheel.radius) / axle.rightWheel.suspensionDistance;
                }

                float antiRollForce = (travelL - travelR) * strength;

                if (rb != null)
                {

                    if (groundedL)
                    {
                        rb.AddForceAtPosition(axle.leftWheel.transform.up * -antiRollForce, axle.leftWheel.transform.position);
                    }

                    if (groundedR)
                    {
                        rb.AddForceAtPosition(axle.rightWheel.transform.up * antiRollForce, axle.rightWheel.transform.position);
                    }

                }
                else
                {
                    Debug.LogWarning("Anti roll bar doens't have a RigidBody set! Can't apply forces.");
                }
            }
            else
            {
                Debug.LogWarning("Anti roll bar doesn't have a axle set! Assign a axle with wheels set up correctly.");
            }


        }

    }
}
