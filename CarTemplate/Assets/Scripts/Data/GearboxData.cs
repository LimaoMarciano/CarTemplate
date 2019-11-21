using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GearboxData", menuName = "CarTemplate/Gearbox model", order = 1)]
public class GearboxData : ScriptableObject
{
    public List<float> gearRatios;
    public float reverseGearRatio;
    public float finalGear;
}

