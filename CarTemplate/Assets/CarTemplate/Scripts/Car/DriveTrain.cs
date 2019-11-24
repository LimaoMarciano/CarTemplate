using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CarTemplate
{
    /// <summary>
    /// The drive train part is a parent class for all parts that are connected together in the car (engine, clutch, gearbox and differential)
    /// 
    /// <para>The drive train is the pipeline where torque e RPM flows through, each part receiving values, processing them 
    /// and passing the results to next part</para>
    /// 
    /// <para>The wheels start the process by sending its RPM. After a part sends RPM or torque, the next part automatically
    /// calls the method to process the value, starting a chain reaction.</para>
    /// 
    /// <para>As the wheel collider only exposes RPM and not torque, the pipeline sends values on RPM. When the value gets to the engine,
    /// it converts RPM to torque and the torque makes its way back to the wheels. 
    /// That's why every train part has a RPM output, and a Torque output.</para>
    /// 
    /// <para>The RPM and Torque outputs must be set the drive train parts that'll receive the processed values. You can imagine the RPM output as the "in" way, and Torque output as "out".</para>
    /// </summary>
    public class DriveTrain
    {
        public DriveTrain rpmOutputDriveTrain;
        public DriveTrain torqueOutputDriveTrain;

        protected TransmittedRpm inputRpm;
        protected TransmittedTorque inputTorque;

        //Public getters
        //========================================================================================

        public TransmittedRpm InputRpm
        {
            get { return inputRpm; }
        }

        public TransmittedTorque InputTorque
        {
            get { return inputTorque; }
        }


        //Methods
        //========================================================================================


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
