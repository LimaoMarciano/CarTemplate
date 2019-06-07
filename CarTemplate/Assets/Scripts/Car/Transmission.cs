using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transmission
{

    public List<float> gearRatios;
    public List<WheelCollider> drivenWheels;
    public float finalGear;
    private int currentGear = 0;
    public int CurrentGear
    {
        get { return currentGear; }
    }

    public Transmission (TransmissionData data)
    {
        gearRatios = data.gearRatios;
        finalGear = data.finalGear;
    }

    public void IncreaseGear()
    {
        if (currentGear < gearRatios.Count - 1)
        currentGear += 1;
    }

    public void DecreaseGear()
    {
        if (currentGear > 0)
        {
            currentGear -= 1;
        }
    }

    public float GetTransmissionRpm ()
    {

        float wheelRpm = 0;

        //Hackzinho pra pular simular o diferencial, só pega a média do RPM das rodas
        foreach (WheelCollider wheel in drivenWheels)
        {
            wheelRpm += wheel.rpm;
        }

        if (drivenWheels.Count != 0)
            wheelRpm /= drivenWheels.Count;

        if (gearRatios.Count != 0)
        {
            return wheelRpm * gearRatios[currentGear] * finalGear;
        }
        else
        {
            Debug.LogWarning("Transmission don't have any gears. Returned RPM will be set to zero. Please, set the Transmission component correctly.");
            return 0;
        }

    }

    public void ApplyTorque (float torque)
    {

        if (gearRatios.Count != 0)
        {
            foreach (WheelCollider wheel in drivenWheels)
            {

                wheel.motorTorque = (torque * gearRatios[currentGear] * finalGear) / drivenWheels.Count;
                
            }
        }
        else
        {
            Debug.LogWarning("Transmission don't have any gears. Will not apply torque to wheels. Please, set the Transmission component correctly.");
        }
    }

    public void ApplyEngineBrake (float brakeTorque, float currentRpm, float maxRpm)
    {
        if (currentRpm > maxRpm)
        {
            foreach (WheelCollider wheel in drivenWheels)
            {
                wheel.brakeTorque = brakeTorque;
            }
        }
        else
        {
            foreach (WheelCollider wheel in drivenWheels)
            {
                wheel.brakeTorque = 0;
            }
        }
    }

}
