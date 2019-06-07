using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engine
{

    //public EngineData data;
    public AnimationCurve torqueCurve;
    public float maxRpm = 7000.0f;
    public float minRpm = 1000.0f;
    public float peakTorque = 150.0f;

    private float acceleratorInput = 0.0f;
    
    public Engine (EngineData data)
    {
        torqueCurve = data.torqueCurve;
        maxRpm = data.maxRpm;
        minRpm = data.minRpm;
        peakTorque = data.peakTorque;
    }

    public void SetAcceleratorInput(float accelerator)
    {
        acceleratorInput = accelerator;
    }

    public float GetTorqueFromRpm (float rpm)
    {

        float t = Mathf.Clamp(rpm, minRpm, maxRpm) / maxRpm;
        float torque = peakTorque * torqueCurve.Evaluate(t);

        return torque * acceleratorInput;

    }
}
