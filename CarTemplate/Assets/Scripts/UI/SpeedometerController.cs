using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CarTemplate;

public class SpeedometerController : MonoBehaviour
{

    public Car car;
    private Speedometer speedometer;

    // Start is called before the first frame update
    void Start()
    {
        speedometer = GetComponent<Speedometer>();
        speedometer.maxRpm = car.engine.data.maxRpm;
    }

    // Update is called once per frame
    void Update()
    {
        //speedometer.currentSpeed = car.Speed;
        speedometer.currentGear = car.gearbox.CurrentGear;
        speedometer.rpm = car.engine.InputRPM;
    }
}
