using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CarTemplate
{
    public struct TransmittedRpm
    {
        public float rpm;
        public float connectionSlip;

        public TransmittedRpm(float setRpm)
        {
            rpm = setRpm;
            connectionSlip = 0f;
        }

        public TransmittedRpm(float setRpm, float slip)
        {
            rpm = setRpm;
            connectionSlip = slip;
        }
    }

    

    public struct TransmittedTorque
    {
        public float torque;
        public float connectionSlip;

        public TransmittedTorque(float setTorque)
        {
            torque = setTorque;
            connectionSlip = 0f;
        }

        public TransmittedTorque(float setTorque, float slip)
        {
            torque = setTorque;
            connectionSlip = slip;
        }
    }
}

