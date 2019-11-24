using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CarTemplate
{

    /// <summary>
    /// <para>The engine is the heart of the car. It produces torque based on it's current RPM.</para>
    /// This class receives RPM from the connected drive train and sends back corresponding torque
    /// </summary>
    public class Engine : DriveTrain
    {
        public EngineData data;

        /// <summary>
        /// Engine accelerator input. Control the percentage of current torque sent to drive train.
        /// </summary>
        [Range (0,1)]
        public float acceleratorInput = 0.0f;

        private TransmittedTorque outputTorque = new TransmittedTorque(0f);
        
        /// <summary>
        /// How fast the engine respond to changes. Used to give a quick or sluggish feel to engine.
        /// </summary>
        private float engineResponse = 0.3f;

        private float smoothDampVelocity = 0f;

        private float engineRpm = 0f;

        //Public getters
        //=======================================================================================================

        /// <summary>
        /// Current engine RPM
        /// </summary>
        public float EngineRpm
        {
            get { return engineRpm; }
        }

        //Methods
        //========================================================================================================

        protected override void ProcessInputRpm()
        {

            float targetRpm;

            if (engineRpm > (data.maxRpm - 100))
            {
                acceleratorInput = 0f;
            }
            
            targetRpm = Mathf.Lerp(inputRpm.rpm, data.maxRpm * acceleratorInput, inputRpm.connectionSlip);
            engineRpm = Mathf.SmoothDamp(engineRpm, targetRpm, ref smoothDampVelocity, engineResponse);

            if (acceleratorInput > 0f)
            {
                outputTorque.torque = GetTorqueFromRpm(engineRpm) * acceleratorInput;
            } else
            {
                outputTorque.torque = -data.engineBrakeCoefficient * Mathf.Clamp(engineRpm, -data.maxRpm, data.maxRpm) / 60;
            }
            
            if (torqueOutputDriveTrain != null)
            {
                torqueOutputDriveTrain.SetInputTorque(outputTorque);
            }
            else
            {
                Debug.LogWarning("Engine doesn't have a torque output. Won't transmit torque.");
            }

        }

        /// <summary>
        /// Calculates engine torque based on current RPM, considering the engine power curve
        /// </summary>
        /// <param name="rpm">Engine RPM</param>
        /// <returns>Torque from current RPM</returns>
        private float GetTorqueFromRpm(float rpm)
        {
            float t = Mathf.Clamp(rpm, data.minRpm, data.maxRpm) / data.maxRpm;

            float power = data.peakPower * data.powerCurve.Evaluate(t);
            float torque = (9.5488f * power * 1000) / Mathf.Clamp(rpm, data.minRpm, data.maxRpm);

            return torque;

        }
    }
}

