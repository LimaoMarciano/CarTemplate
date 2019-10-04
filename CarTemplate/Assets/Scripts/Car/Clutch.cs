using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CarTemplate
{
    public class Clutch : DriveTrain
    {
        public float grip = 10f;
        public float clutchInput = 0f;

        protected override void ProcessRPM()
        {
            float rpmDifference = inputRPM - outputRPM;
            outputRPM += rpmDifference * grip * (1f - clutchInput) * Time.deltaTime;

            SendOutputRPM();
        }

        protected override void ProcessTorque()
        {
            outputTorque = inputTorque * (1f - clutchInput);

            SendOutputTorque();
        }

    }
}

