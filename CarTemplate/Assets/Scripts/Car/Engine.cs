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
        private float engineResponse = 0.3f;

        private float engineProtectionCutoff = 0.02f;
        private float engineProtectionTimer = 0.0f;
        private bool isProtectionOn = false;

        private float smoothDampVelocity = 0f;

        public float EngineRpm
        {
            get { return engineRpm; }
        }

        protected override void ProcessInputRpm()
        {

            float targetRpm;

            if (engineRpm > (data.maxRpm - 100))
            {
                isProtectionOn = true;
                engineProtectionTimer = 0f;
            }

            if (isProtectionOn)
            {
                engineProtectionTimer += Time.deltaTime;
                acceleratorInput = 0f;
                if (engineProtectionTimer >= engineProtectionCutoff)
                {
                    isProtectionOn = false;
                }
            }

            targetRpm = Mathf.Lerp(inputRpm.rpm, data.maxRpm * acceleratorInput, inputRpm.connectionSlip);
            engineRpm = Mathf.SmoothDamp(engineRpm, targetRpm, ref smoothDampVelocity, engineResponse);

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

