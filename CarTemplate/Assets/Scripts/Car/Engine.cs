using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engine
{

    //public EngineData data;
    public AnimationCurve powerCurve;
    public float maxRpm = 7000.0f;
    public float minRpm = 1000.0f;
    public float peakPower = 150.0f;

    private float acceleratorInput = 0.0f;
    
    public Engine (EngineData data)
    {
        powerCurve = data.torqueCurve;
        maxRpm = data.maxRpm;
        minRpm = data.minRpm;
        peakPower = data.peakPower;
    }

    public void SetAcceleratorInput(float accelerator)
    {
        acceleratorInput = accelerator;
    }

    public float GetTorqueFromRpm (float rpm)
    {
        
        float t = Mathf.Clamp(rpm, minRpm, maxRpm) / maxRpm;
        
        float power = peakPower * powerCurve.Evaluate(t);
        float torque = (9.5488f * power * 1000) / Mathf.Clamp(rpm,minRpm, maxRpm);

        return torque * acceleratorInput;

    }
}
