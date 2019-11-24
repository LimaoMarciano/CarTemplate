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
        [Range (0,1)]
        public float acceleratorInput = 0.0f;

        private TransmittedTorque outputTorque = new TransmittedTorque(0f);
        private float engineRpm = 0f;
        private float engineResponse = 0.3f;

        private float smoothDampVelocity = 0f;

        public float EngineRpm
        {
            get { return engineRpm; }
        }

        protected override void ProcessInputRpm()
        {

            float targetRpm;

            if (engineRpm > (data.maxRpm))
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
            

            torqueOutputDriveTrain.SetInputTorque(outputTorque);

        }

        private float GetTorqueFromRpm(float rpm)
        {
            float t = Mathf.Clamp(rpm, data.minRpm, data.maxRpm) / data.maxRpm;

            float power = data.peakPower * data.powerCurve.Evaluate(t);
            float torque = (9.5488f * power * 1000) / Mathf.Clamp(rpm, data.minRpm, data.maxRpm);

            return torque;

        }
    }
}

