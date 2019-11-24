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

        /// <summary>
        /// Defines how brake pressure will be split between front and rear wheels. Range between 0 and 1.
        /// <para>0 = 100% rear, 1 = 100% front</para>
        /// </summary>
        public float brakeBias = 0.5f;

        private float brakeInput = 0f;
        private float handbrakeInput = 0f;

        /// <summary>
        /// Current brake input
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
        /// Apply brake and handbrake pressure on the wheels, respecting the configured brake bias
        /// </summary>
        /// <param name="brakeValue">Brake input. Value between 0 and 1.</param>
        /// <param name="handbrakeValue">Handbrake input. Value between 0 and 1</param>
        public void ApplyPressure (float brakeValue, float handbrakeValue)
        {

            brakeInput = Mathf.Clamp01(brakeValue);
            handbrakeInput = Mathf.Clamp01(handbrakeValue);

            brakeBias = Mathf.Clamp01(brakeBias);

            float frontPower = (data.frontBrakePower * brakeBias * brakeInput);
            float rearPower = (data.rearBrakePower * (1 - brakeBias) * brakeInput) + (data.handbrakePower * handbrakeInput);

            frontAxle.leftWheel.brakeTorque = frontPower;
            frontAxle.rightWheel.brakeTorque = frontPower;
            rearAxle.leftWheel.brakeTorque = rearPower;
            rearAxle.rightWheel.brakeTorque = rearPower;
        }

    }


}
