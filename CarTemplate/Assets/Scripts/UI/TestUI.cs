using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestUI : MonoBehaviour
{

    public Car car;
    public Speedometer speedometer;

    // Start is called before the first frame update
    void Start()
    {
        speedometer.maxRpm = car.engineData.maxRpm;
    }

    // Update is called once per frame
    void Update()
    {
        speedometer.currentSpeed = car.Speed;
        speedometer.currentGear = car.CurrentGear;
        speedometer.rpm = car.EngineRpm;
    }
}
