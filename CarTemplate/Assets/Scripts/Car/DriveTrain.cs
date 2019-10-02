using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CarTemplate
{
    /// <summary>
    /// 
    /// </summary>
    public class DriveTrain
    {
        public DriveTrain input;
        public DriveTrain output;

        protected float inputTorque = 0;
        protected float outputTorque = 0;
        protected float outputRPM = 0;
        protected float inputRPM = 0;

        /// <summary>
        /// 
        /// </summary>
        public float InputTorque
        {
            set {
                inputTorque = value;
                ProcessTorque();
            }
            get { return inputTorque; }
        }

        public float InputRPM
        {
            set {
                inputRPM = value;
                ProcessRPM();
            }
            get { return inputRPM; }
        }

        public float OutputTorque
        {
            get { return outputTorque; }
        }

        public float OutputRPM
        {
            get { return outputRPM; }
        }

        //Methods
        /*=======================================================================================*/

        /// <summary>
        /// Force receiving processed RPM from input drive train part
        /// </summary>
        protected void GetRPMInput ()
        {
            if (input != null)
            {
                InputRPM = input.OutputRPM;
            }
            else
            {
                Debug.LogWarning("No output connected");
            }
        }

        /// <summary>
        /// Sends processed RPM to connected output drive train part
        /// </summary>
        protected void SendRPMOutput ()
        {
            if (output != null)
            {
                output.InputRPM = OutputRPM;
            }
            else
            {
                Debug.LogWarning("No input connected");
            }
        }

        /// <summary>
        /// Force receiving processed torque from connected input drive train part
        /// </summary>
        protected void GetTorqueInput ()
        {
            if (input != null)
            {
                InputTorque = input.OutputTorque;
            }
            else
            {
                Debug.LogWarning("No output connected");
            }
        }

        /// <summary>
        /// Sends processed torque to connected output drive train part
        /// </summary>
        protected void SendOutputTorque ()
        {
            if (output != null)
            {
                output.InputTorque = OutputTorque;
            }
            else
            {
                Debug.LogWarning("No input connected");
            }
        }

        protected virtual void ProcessRPM()
        {
            outputRPM = inputRPM;
            SendOutputTorque();
        }

        protected virtual void ProcessTorque()
        {
            outputTorque = inputTorque;
            SendOutputTorque();
        }

    }
}
