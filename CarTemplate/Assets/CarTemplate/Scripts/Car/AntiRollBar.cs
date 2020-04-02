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

                bool groundedL = axle.leftWheel.collider.GetGroundHit(out hit);
                if (groundedL)
                {
                    travelL = (-axle.leftWheel.collider.transform.InverseTransformPoint(hit.point).y - axle.leftWheel.collider.radius) / axle.leftWheel.collider.suspensionDistance;
                }

                bool groundedR = axle.rightWheel.collider.GetGroundHit(out hit);
                if (groundedR)
                {
                    travelR = (-axle.rightWheel.collider.transform.InverseTransformPoint(hit.point).y - axle.rightWheel.collider.radius) / axle.rightWheel.collider.suspensionDistance;
                }

                float antiRollForce = (travelL - travelR) * strength;

                if (rb != null)
                {

                    if (groundedL)
                    {
                        rb.AddForceAtPosition(axle.leftWheel.collider.transform.up * -antiRollForce, axle.leftWheel.collider.transform.position);
                    }

                    if (groundedR)
                    {
                        rb.AddForceAtPosition(axle.rightWheel.collider.transform.up * antiRollForce, axle.rightWheel.collider.transform.position);
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
