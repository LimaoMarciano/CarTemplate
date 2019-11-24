using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SuspensionModel", menuName = "CarTemplate/Suspension Model", order = 1)]
public class SuspensionModel : ScriptableObject
{
    public float suspensionDistance = 0.2f;
    public float targetPosition = 0.5f;
    public float springForce = 15000f;
    public float damper = 1700f;
    public float ForceAppPointDistance = 0.27f;
}
