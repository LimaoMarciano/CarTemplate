﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CarTemplate;

public class InputsDebug : MonoBehaviour
{
    public Car car;
    public Fillbar accelerator;
    public Fillbar brakes;
    public Fillbar clutch;
    
    // Start is called before the first frame update
    void Start()
    {
        accelerator.SetMaxValue(1f);
        brakes.SetMaxValue(1f);
        clutch.SetMaxValue(1f);
    }

    // Update is called once per frame
    void Update()
    {
        accelerator.value = car.engine.acceleratorInput;
        brakes.value = car.brakes.BrakeInput;
        clutch.value = car.clutch.clutchInput;
    }
}
