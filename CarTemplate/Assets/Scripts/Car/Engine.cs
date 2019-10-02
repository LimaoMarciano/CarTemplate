using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CarTemplate;

namespace CarTemplate
{

    public class Engine : DriveTrain
    {
        public EngineData engineData;
        [Range (0,1)]
        public float acceleratorInput = 0.0f;

        protected override void ProcessRPM()
        {
            outputTorque = GetTorqueFromRpm(InputRPM) * acceleratorInput;
            SendOutputTorque();
        }

        private float GetTorqueFromRpm(float rpm)
        {
            float t = Mathf.Clamp(rpm, engineData.minRpm, engineData.maxRpm) / engineData.maxRpm;

            float power = engineData.peakPower * engineData.powerCurve.Evaluate(t);
            float torque = (9.5488f * power * 1000) / Mathf.Clamp(rpm, engineData.minRpm, engineData.maxRpm);

            return torque;

        }
    }
}

