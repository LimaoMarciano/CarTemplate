using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProtoCar
{
    public class Clutch
    {
        public float grip = 10f;

        private float clutchConnection = 1.0f;

        private float inputRpm = 0.0f;
        private float outputRpm = 0.0f;
        private float inputTorque = 0.0f;
        private float outputTorque = 0.0f;

        public float OutputRpm
        {
            get { return outputRpm; }
        }

        public float OutputTorque
        {
            get { return outputTorque; }
        }

        public float GetClutchRpmOutput()
        {
            float rpmDifference = inputRpm - outputRpm;
            outputRpm += rpmDifference * grip * clutchConnection * Time.deltaTime;

            return outputRpm;
        }

        public float GetClutchTorque()
        {
            outputTorque = inputTorque * clutchConnection;

            return outputTorque;
        }

        public void SetClutchInput(float input)
        {
            clutchConnection = Mathf.Clamp01(input);
        }

        public void SetInputRpm(float rpm)
        {
            inputRpm = rpm;
        }

        public void SetInputTorque(float torque)
        {
            inputTorque = torque;
        }

    }
}

