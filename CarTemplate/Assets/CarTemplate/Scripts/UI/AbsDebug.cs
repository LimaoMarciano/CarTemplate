using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CarTemplate;

public class AbsDebug : MonoBehaviour
{
    public Car car;
    public CarController carController;
    public DoubleFillbar FLAbsGauge;
    public DoubleFillbar FRAbsGauge;
    public DoubleFillbar RLAbsGauge;
    public DoubleFillbar RRAbsGauge;

    // Start is called before the first frame update
    void Start()
    {
        FLAbsGauge.SetLabel("FL");
        FRAbsGauge.SetLabel("FR");
        RLAbsGauge.SetLabel("RL");
        RRAbsGauge.SetLabel("RR");

        FLAbsGauge.SetMaxValue(1f);
        FRAbsGauge.SetMaxValue(1f);
        RLAbsGauge.SetMaxValue(1f);
        RRAbsGauge.SetMaxValue(1f);
    }

    // Update is called once per frame
    void Update()
    {
        float FLPressure = car.brakes.FrontLeftBrakeTorque / car.brakes.data.frontBrakeTorque;
        float FRPressure = car.brakes.FrontRightBrakeTorque / car.brakes.data.frontBrakeTorque;
        float RLPressure = car.brakes.RearLeftBrakeTorque / car.brakes.data.rearBrakeTorque;
        float RRPressure = car.brakes.RearRightBrakeTorque / car.brakes.data.rearBrakeTorque;

        FLAbsGauge.value01 = FLPressure;
        FRAbsGauge.value01 = FRPressure;
        RLAbsGauge.value01 = RLPressure;
        RRAbsGauge.value01 = RRPressure;

        FLAbsGauge.value02 = GetAbsValue(carController.abs.FrontLeftAbs) * FLPressure;
        FRAbsGauge.value02 = GetAbsValue(carController.abs.FrontRightAbs) * FRPressure;
        RLAbsGauge.value02 = GetAbsValue(carController.abs.RearLeftAbs) * RLPressure;
        RRAbsGauge.value02 = GetAbsValue(carController.abs.RearRightAbs) * RRPressure;
    }

    private float GetAbsValue (AntiLockBrakeSensor absSensor)
    {
        float value;

        if (absSensor.IsActive)
        {
            value = absSensor.AllowedBrakePressure;
        }
        else
        {
            value = 0;
        }

        return value;
    }

}
