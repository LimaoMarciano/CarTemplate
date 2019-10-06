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
        public DriveTrain rpmOutputDriveTrain;
        public DriveTrain torqueOutputDriveTrain;

        protected TransmittedRpm inputRpm;
        protected TransmittedTorque inputTorque;

        public TransmittedRpm InputRpm
        {
            get { return inputRpm; }
        }

        public TransmittedTorque InputTorque
        {
            get { return inputTorque; }
        }


        //Methods
        /*=======================================================================================*/


        /// <summary>
        /// Set rpm input and calls ProcessInputRpm method
        /// </summary>
        public void SetInputRpm (TransmittedRpm rpm)
        {
            inputRpm = rpm;
            ProcessInputRpm();
        }

        /// <summary>
        /// Set torque input and calls ProcessTorqueRpm method
        /// </summary>
        public void SetInputTorque (TransmittedTorque torque)
        {
            inputTorque = torque;
            ProcessInputTorque();
        }

        /// <summary>
        /// Method called when rpm input is set. This method can be overriden to create the drive train part behaviour when receiving RPM from another part.
        /// Default behaviour is to just bypass RPM to the rpmOutputDriveTrain.
        /// </summary>
        protected virtual void ProcessInputRpm()
        {
            if (rpmOutputDriveTrain != null)
            {
                rpmOutputDriveTrain.SetInputRpm(inputRpm);
            }
            else
            {
                Debug.LogWarning("No drive train part connected to rpm output");
            }
        }

        /// <summary>
        /// Method called when torque input is set. This method can be overriden to create the drive train part behaviour when receiving torque from another part.
        /// Default behaviour is to just bypass torque to the torqueOutputDriveTrain.
        /// </summary>
        protected virtual void ProcessInputTorque()
        {
            if (rpmOutputDriveTrain != null)
            {
                rpmOutputDriveTrain.SetInputTorque(inputTorque);
            }
            else
            {
                Debug.LogWarning("No drive train part connected to torque output");
            }
        }

    }
}
