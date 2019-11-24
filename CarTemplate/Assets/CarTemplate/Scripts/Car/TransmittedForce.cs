using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CarTemplate
{
    /// <summary>
    /// Struct the holds the transmitted RPM values between drive train parts.
    /// <para>The connection slip variable represents if the connection between parts is solid, or if it's a slipping connection.
    /// This information is necessary to represent the connection between engine and gearbox through the clutch, which can be solid connected
    /// or slipping on different levels.</para>
    /// 
    /// <para>The value doesn't do anything on it's own, but can be used to create custom behaviour on the process methods.</para>
    /// </summary>
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


    /// <summary>
    /// Struct the holds the transmitted torque values between drive train parts.
    /// <para>The connection slip variable represents if the connection between parts is solid, or if it's a slipping connection.
    /// This information is necessary to represent the connection between engine and gearbox through the clutch, which can be solid connected
    /// or slipping on different levels.</para>
    /// 
    /// <para>The value doesn't do anything on it's own, but can be used to create custom behaviour on the process methods.</para>
    /// </summary>
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

