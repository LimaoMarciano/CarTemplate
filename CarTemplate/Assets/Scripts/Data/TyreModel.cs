using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TyreModel", menuName = "CarTemplate/Tyre Model", order = 1)]
public class TyreModel : ScriptableObject
{
    public string modelName = "Tyre";
    public float radius = 0.29f;
    public float mass = 20f;

    [System.Serializable]
    public struct FrictionCurveData
    {
        public float extremumSlip;
        public float extremumValue;
        public float asymptoteSlip;
        public float asymptoteValue;
        public float stiffness;
    }

    [Header("Friction curves")]
    public FrictionCurveData forwardFriction = new FrictionCurveData()
    {
        extremumSlip = 0.2f,
        extremumValue = 1f,
        asymptoteSlip = 0.4f,
        asymptoteValue = 0.5f,
        stiffness = 1f,
    };

    public FrictionCurveData sidewaysFriction = new FrictionCurveData()
    {
        extremumSlip = 0.2f,
        extremumValue = 1f,
        asymptoteSlip = 0.5f,
        asymptoteValue = 0.75f,
        stiffness = 1f,
    };

}
