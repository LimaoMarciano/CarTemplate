using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "EngineData", menuName = "CarTemplate/Engine model", order = 1)]
public class EngineData : ScriptableObject
{
    public AnimationCurve powerCurve;
    public float maxRpm = 7000.0f;
    public float minRpm = 1000.0f;
    public float redline = 5000.0f;
    public float cutoffRpm = 5100.0f;
    public float peakPower = 150.0f;
    public float engineBrakeCoefficient = 1f;
}

