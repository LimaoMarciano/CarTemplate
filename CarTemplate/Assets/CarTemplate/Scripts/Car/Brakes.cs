using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CarTemplate
{

    /// <summary>
    /// Class responsible for brakes and handbreak behaviour. Brakes are applied to all wheels, while handbrake is applied only to rear wheels.
    /// <para>The brake bias variable sets how applied input will be split between front and rear wheels.</para>
    /// </summary>
    public class Brakes
    {
        public Axle frontAxle;
        public Axle rearAxle;

        public BrakesData data;

        private float brakeBias = 0.5f;
        private float brakeInput = 0f;
        private float handbrakeInput = 0f;

        private float maxFrontBrakeTorque = 0f;
        private float maxRearBrakeTorque = 0f;

        private float FLBrakeTorque = 0f;
        private float FRBrakeTorque = 0f;
        private float RLBrakeTorque = 0f;
        private float RRBrakeTorque = 0f;
        private float handbrakeTorque = 0f;

        #region Public getters and setters  
        /// <summary>
        /// Defines how brake pressure will be split between front and rear wheels. Range between 0 and 1.
        /// <para>0 = 100% rear, 1 = 100% front</para>
        /// </summary>
        public float BrakeBias
        {
            get { return brakeBias; }
            set
            {
                brakeBias = value;
                RefreshBrakeTorqueBiasSplit();
            }
        }

        /// <summary>
        /// Current brake input. The value is a average of all four brakes.
        /// </summary>
        public float BrakeInput
        {
            get { return brakeInput; }
        }
        
        /// <summary>
        /// Current handbrake input
        /// </summary>
        public float HandbrakeInput
        {
            get { return handbrakeInput; }
        }

        /// <summary>
        /// Max braking torque on the front accounting for current brake bias.
        /// </summary>
        public float MaxFrontBrakeTorque
        {
            get { return maxFrontBrakeTorque; }
        }

        /// <summary>
        /// Max braking torque on the rear accounting for current brake bias.
        /// </summary>
        public float MaxRearBrakeTorque
        {
            get { return maxRearBrakeTorque; }
        }

        /// <summary>
        /// Current braking torque on front left brake
        /// </summary>
        public float FrontLeftBrakeTorque
        {
            get { return FLBrakeTorque; }
        }

        /// <summary>
        /// Current braking torque on front right brake
        /// </summary>
        public float FrontRightBrakeTorque
        {
            get { return FRBrakeTorque; }
        }

        /// <summary>
        /// Current braking torque on rear left brake
        /// </summary>
        public float RearLeftBrakeTorque
        {
            get { return RLBrakeTorque; }
        }

        /// <summary>
        /// Current braking torque on rear right brake
        /// </summary>
        public float RearRightBrakeTorque
        {
            get { return RRBrakeTorque; }
        }

        /// <summary>
        /// Current braking torque applied by handbrake
        /// </summary>
        public float HandbrakeBrakeTorque
        {
            get { return handbrakeTorque; }
        }

        #endregion

        #region Public methods
        /// <summary>
        /// Apply same brake pressure on all wheels plus handbrake pressure on rear wheels, respecting the configured brake bias.
        /// </summary>
        /// <param name="brakeInput">Brake pressure. Value between 0 and 1.</param>
        /// <param name="handbrakeInput">Handbrake pressure. Value between 0 and 1</param>
        public void ApplyPressure (float brakeInput, float handbrakeInput)
        {

            this.brakeInput = Mathf.Clamp01(brakeInput);
            this.handbrakeInput = Mathf.Clamp01(handbrakeInput);

            FLBrakeTorque = FRBrakeTorque = maxFrontBrakeTorque * this.brakeInput;
            RLBrakeTorque = RRBrakeTorque = maxRearBrakeTorque * this.BrakeInput;
            handbrakeTorque = data.handbrakePower * this.handbrakeInput;

            frontAxle.leftWheel.collider.brakeTorque = FLBrakeTorque;
            frontAxle.rightWheel.collider.brakeTorque = FRBrakeTorque;
            rearAxle.leftWheel.collider.brakeTorque = RLBrakeTorque + handbrakeTorque;
            rearAxle.rightWheel.collider.brakeTorque = RRBrakeTorque + handbrakeTorque;
        }

        /// <summary>
        /// Apply different brake pressures to each wheel plus handbrake pressure to rear wheels, respecting the configured brake bias
        /// </summary>
        /// <param name="FLBrakeInput">Front left brake pressure. Value between 0 and 1.</param>
        /// <param name="FRBrakeInput">Front right brake pressure. Value between 0 and 1.</param>
        /// <param name="RLBrakeInput">Rear left brake pressure. Value between 0 and 1.</param>
        /// <param name="RRBrakeInput">Rear right brake pressure. Value between 0 and 1.</param>
        /// <param name="handbrakeInput">Handbrake pressure. Value between 0 and 1.</param>
        public void ApplyPressure (float FLBrakeInput, float FRBrakeInput, float RLBrakeInput, float RRBrakeInput, float handbrakeInput)
        {

            this.handbrakeInput = Mathf.Clamp01(handbrakeInput);
            this.brakeInput = (FLBrakeInput + FRBrakeInput + RLBrakeInput + RRBrakeInput) / 4f;

            FLBrakeTorque = maxFrontBrakeTorque * FLBrakeInput;
            FRBrakeTorque = maxFrontBrakeTorque * FRBrakeInput;
            RLBrakeTorque = maxRearBrakeTorque * RLBrakeInput;
            RRBrakeTorque = maxRearBrakeTorque * RRBrakeInput;
            handbrakeTorque = data.handbrakePower * this.handbrakeInput;

            frontAxle.leftWheel.collider.brakeTorque = FLBrakeTorque;
            frontAxle.rightWheel.collider.brakeTorque = FRBrakeTorque;
            rearAxle.leftWheel.collider.brakeTorque = RLBrakeTorque + handbrakeTorque;
            rearAxle.rightWheel.collider.brakeTorque = RRBrakeTorque + handbrakeTorque;

        }

        #endregion

        /// <summary>
        /// Recalculate front and rear max brake torque based on current brake bias
        /// </summary>
        private void RefreshBrakeTorqueBiasSplit ()
        {
            maxFrontBrakeTorque = data.frontBrakeTorque * brakeBias;
            maxRearBrakeTorque = data.rearBrakeTorque * (1 - brakeBias);
        }

    }

}
