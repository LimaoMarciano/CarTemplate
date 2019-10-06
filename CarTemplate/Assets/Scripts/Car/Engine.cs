using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CarTemplate
{

    public class Engine : DriveTrain
    {
        public EngineData data;
        [Range (0,1)]
        public float acceleratorInput = 0.0f;

        private TransmittedTorque outputTorque = new TransmittedTorque(0f);
        private float engineRpm = 0f;
        private float engineResponse = 0.1f;

        private float smoothDampVelocity = 0f;


        protected override void ProcessInputRpm()
        {

            float targetRpm;

            targetRpm = Mathf.Lerp(inputRpm.rpm, data.maxRpm * acceleratorInput, inputRpm.connectionSlip);
            engineRpm = Mathf.SmoothDamp(engineRpm, targetRpm, ref smoothDampVelocity, engineResponse);
            Debug.Log(engineRpm);

            outputTorque.torque = GetTorqueFromRpm(engineRpm) * acceleratorInput;

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

