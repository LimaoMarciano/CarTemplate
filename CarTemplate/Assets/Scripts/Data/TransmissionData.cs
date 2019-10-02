using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TransmissionData", menuName = "CarTemplate/Transmission model", order = 1)]
public class TransmissionData : ScriptableObject
{
    public List<float> gearRatios;
    public float finalGear;
}

