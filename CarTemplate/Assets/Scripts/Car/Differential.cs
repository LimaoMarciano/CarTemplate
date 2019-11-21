using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CarTemplate
{
    public class Differential : DriveTrain
    {
        public WheelCollider[] wheels;
        public Axle axle;

        private TransmittedRpm outputRpm = new TransmittedRpm(0f);

        public void Update()
        {

            outputRpm.rpm = (axle.rightWheel.rpm + axle.leftWheel.rpm) / 2f;
            rpmOutputDriveTrain.SetInputRpm(outputRpm);

        }

        protected override void ProcessInputTorque()
        {
            
            float torque = inputTorque.torque / 2f;
            axle.rightWheel.motorTorque = torque;
            axle.leftWheel.motorTorque = torque;

        }

        
    }
}

