using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EngineData", menuName = "CarTemplate/Engine model", order = 1)]
public class EngineData : ScriptableObject
{
    public AnimationCurve torqueCurve;
    public float maxRpm = 7000.0f;
    public float minRpm = 1000.0f;
    public float peakTorque = 150.0f;
}
