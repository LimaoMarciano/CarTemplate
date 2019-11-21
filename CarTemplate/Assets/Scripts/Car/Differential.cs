using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CarTemplate
{
    public class Differential : DriveTrain
    {
        public WheelCollider[] wheels;
        public Axle axle;

        private float speed;

        public float Speed
        {
            get { return speed; }
        }

        private TransmittedRpm outputRpm = new TransmittedRpm(0f);

        public void Update()
        {

            outputRpm.rpm = (axle.rightWheel.rpm + axle.leftWheel.rpm) / 2f;
            rpmOutputDriveTrain.SetInputRpm(outputRpm);

            speed = CalculateSpeed(outputRpm.rpm, axle.rightWheel.radius);

        }

        protected override void ProcessInputTorque()
        {
            
            float torque = inputTorque.torque / 2f;
            axle.rightWheel.motorTorque = torque;
            axle.leftWheel.motorTorque = torque;

        }

        private float CalculateSpeed(float rpm, float wheelRadius)
        {
            float wheelCircunference = wheelRadius * 2.0f * Mathf.PI;
            float speed = (rpm * wheelCircunference) / 60.0f;

            return speed;
        }

    }
}

