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

        protected override void ProcessRPM()
        {
            outputTorque = GetTorqueFromRpm(InputRPM) * acceleratorInput;
            SendOutputTorque();
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

