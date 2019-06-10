using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engine
{

    //Engine settings
    public AnimationCurve powerCurve;
    public float maxRpm = 7000.0f;
    public float minRpm = 1000.0f;
    public float peakPower = 150.0f;
    public float engineDelay = 0.3f;

    private float engineRpm = 0.0f;
    private float targetRpm = 0.0f;

    //Inputs
    private float transmissionConnection = 1.0f;
    private float acceleratorInput = 0.0f;
    private float inputRpm = 0.0f;

    //Outputs
    private float outputTorque = 0.0f;
    

    private float smoothDampVelocity = 0.0f;
    
    public float EngineRpm
    {
        get { return engineRpm; }
    }

    public float OutputTorque
    {
        get { return outputTorque; }
    }

    public Engine (EngineData data)
    {
        powerCurve = data.torqueCurve;
        maxRpm = data.maxRpm;
        minRpm = data.minRpm;
        peakPower = data.peakPower;
    }

    public void EngineUpdate ()
    {

        targetRpm = Mathf.Lerp(maxRpm * acceleratorInput, inputRpm, transmissionConnection);
        engineRpm = Mathf.SmoothDamp(engineRpm, targetRpm, ref smoothDampVelocity, engineDelay);
        outputTorque = GetTorqueFromRpm(engineRpm);

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

    public void InputRpm (float rpm)
    {
        inputRpm = rpm;
    }
}
