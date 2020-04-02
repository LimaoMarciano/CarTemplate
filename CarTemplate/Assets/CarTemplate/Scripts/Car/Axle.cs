using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace CarTemplate
{
    /// <summary>
    /// The axle holds two wheels together! The common car have two of them: front and rear axle.
    /// Differentials apply torque to one of the car's axles or to both on all-wheel drives.
    /// <para>Useful class to have a pair of wheels, with explicit left and right wheels</para>
    /// </summary>
    [Serializable]
    public class Axle
    {

        public Wheel leftWheel;
        public Wheel rightWheel;

        //public WheelCollider leftWheel;
        //public WheelCollider rightWheel;

        //Public getters
        //================================================================================================================

        /// <summary>
        /// Left wheel collision info
        /// </summary>
        //public WheelInfo LeftWheelInfo
        //{
        //    get { return GetWheelInfo(leftWheel); }
        //}

        /// <summary>
        /// Right wheel collision info
        /// </summary>
        //public WheelInfo RightWheelInfo
        //{
        //    get { return GetWheelInfo(rightWheel); }
        //}

        /// <summary>
        /// Wheel collision information. (forward and sideway slip, force on contact and detected physics material)
        /// </summary>
        //public class WheelInfo
        //{
        //    public float sidewaySlip;
        //    public float forwardSlip;
        //    public float forceOnContact;
        //    public PhysicMaterial contactMaterial;
        //}
        

        //Methods
        //================================================================================================================

        /// <summary>
        /// Returns the axle mid point position
        /// </summary>
        /// <returns></returns>
        public Vector3 GetAxleMidPoint ()
        {
            Vector3 track = (leftWheel.collider.transform.position + rightWheel.collider.transform.position) / 2;
            return track;
        }

        /// <summary>
        /// Returns distance between the two wheels in a axle
        /// </summary>
        /// <returns></returns>
        public float GetAxleTrack ()
        {
            return Vector3.Distance(leftWheel.collider.transform.position, rightWheel.collider.transform.position);
        }

        /// <summary>
        /// Returns wheel collision information
        /// </summary>
        /// <param name="wheel"></param>
        /// <returns>Returns a struct with wheel collision hit info</returns>
        //private WheelInfo GetWheelInfo (WheelCollider wheel)
        //{
        //    WheelInfo wheelInfo = new WheelInfo();
        //    WheelHit hit = new WheelHit();

        //    if (wheel.GetGroundHit(out hit))
        //    {
        //        wheelInfo.forwardSlip = hit.forwardSlip;
        //        wheelInfo.sidewaySlip = hit.sidewaysSlip;
        //        wheelInfo.forceOnContact = hit.force;
        //        wheelInfo.contactMaterial = hit.collider.sharedMaterial;
        //    }
        //    else
        //    {
        //        wheelInfo.forwardSlip = 0;
        //        wheelInfo.sidewaySlip = 0;
        //        wheelInfo.forceOnContact = 0;
        //        wheelInfo.contactMaterial = null;
        //    }

        //    return wheelInfo;
        //}

        /// <summary>
        /// Applies tyre data on the axle's wheels
        /// </summary>
        /// <param name="model">Data assets with wheel parameters</param>
        public void SetTyreModel (TyreModel model)
        {
            leftWheel.collider.forwardFriction = CreateFrictionCurve(model.forwardFriction);
            leftWheel.collider.sidewaysFriction = CreateFrictionCurve(model.sidewaysFriction);
            leftWheel.collider.mass = model.mass;
            leftWheel.collider.radius = model.radius;

            rightWheel.collider.forwardFriction = CreateFrictionCurve(model.forwardFriction);
            rightWheel.collider.sidewaysFriction = CreateFrictionCurve(model.sidewaysFriction);
            rightWheel.collider.mass = model.mass;
            rightWheel.collider.radius = model.radius;
        }

        /// <summary>
        /// Applies suspension data on the axle's wheel's suspension
        /// </summary>
        /// <param name="model">Data asset with suspension parameters</param>
        public void SetSuspensionModel (SuspensionModel model)
        {
            
            JointSpring suspensionSpring = new JointSpring();
            suspensionSpring.spring = model.springForce;
            suspensionSpring.damper = model.damper;
            suspensionSpring.targetPosition = model.targetPosition;

            leftWheel.collider.suspensionSpring = suspensionSpring;
            leftWheel.collider.suspensionDistance = model.suspensionDistance;

            rightWheel.collider.suspensionSpring = suspensionSpring;
            rightWheel.collider.suspensionDistance = model.suspensionDistance;
        }

        /// <summary>
        /// Helper method to create Unity's friction curve from tyre model data
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private WheelFrictionCurve CreateFrictionCurve (TyreModel.FrictionCurveData data)
        {
            WheelFrictionCurve frictionCurve = new WheelFrictionCurve();

            frictionCurve.extremumSlip = data.extremumSlip;
            frictionCurve.extremumValue = data.extremumValue;
            frictionCurve.asymptoteSlip = data.asymptoteSlip;
            frictionCurve.asymptoteValue = data.asymptoteValue;
            frictionCurve.stiffness = data.stiffness;

            return frictionCurve;
        }
    }

}
