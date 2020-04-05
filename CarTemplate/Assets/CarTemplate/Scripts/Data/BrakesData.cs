using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BrakesData", menuName = "CarTemplate/Brakes model", order = 1)]
public class BrakesData : ScriptableObject
{
    public float frontBrakeTorque = 1000f;
    public float rearBrakeTorque = 1000f;
    public float handbrakePower = 1000f;
}
