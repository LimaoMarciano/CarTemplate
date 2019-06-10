using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestUI : MonoBehaviour
{

    public Car car;
    public Speedometer speedometer;
    public Fillbar powerMeter;
    public Fillbar tyreSlipMeter;

    // Start is called before the first frame update
    void Start()
    {
        speedometer.maxRpm = car.engineData.maxRpm;

        powerMeter.SetMaxValue(1000.0f);
        powerMeter.SetLabel("Engine Torque");

        tyreSlipMeter.SetMaxValue(1f);
        tyreSlipMeter.SetLabel("Tyre slip");
    }

    // Update is called once per frame
    void Update()
    {
        speedometer.currentSpeed = car.Speed;
        speedometer.currentGear = car.CurrentGear;
        speedometer.rpm = car.Engine.EngineRpm;

        powerMeter.value = car.EngineTorque;

        tyreSlipMeter.value = car.TyreSlip;

    }
}
