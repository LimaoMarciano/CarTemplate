using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CarTemplate;

public class CarController : MonoBehaviour
{

    public Car car;
    public float changeGearSpeed = 10f;
    public AnimationCurve acceleratorFilter;
    public AnimationCurve brakesFilter;
    public AnimationCurve steeringFilter;

    public bool useFilteredAccelerator = false;
    public bool useFilteredBrakes = false;
    public bool useFilteredSteering = false;
    bool isAcceleratorEnabled = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
        float handbrakeInput = 0f;

        if (Input.GetButtonDown("Fire1"))
        {
            
            if (car.gearbox.CurrentGear != -1)
            {
                StopCoroutine(ChangeGear());
                StartCoroutine(ChangeGear());
            }
            car.gearbox.IncreaseGear();

        }

        if (Input.GetButtonDown("Fire3"))
        {
            
            if (car.gearbox.CurrentGear != -1)
            {
                StopCoroutine(ChangeGear());
                StartCoroutine(ChangeGear());
            }
            car.gearbox.DecreaseGear();

        }

        if (Input.GetButton("Fire2"))
        {
            handbrakeInput = 1f;
        }

        if (isAcceleratorEnabled)
        {
            if (useFilteredAccelerator)
            {
                car.engine.acceleratorInput = GetFilteredAcceleratorInput(Input.GetAxis("RightTrigger"));
            }
            else
            {
                car.engine.acceleratorInput = Input.GetAxis("RightTrigger");
            }
        }

        if (useFilteredSteering)
        {
            car.steering.SetSteeringInput(GetFilteredSteerInput(Input.GetAxis("Horizontal")));
        }
        else
        {
            car.steering.SetSteeringInput(Input.GetAxis("Horizontal"));
        }

        if (useFilteredBrakes)
        {
            float filteredInput = GetFilteredBrakeInput(Input.GetAxis("LeftTrigger"));
            car.brakes.ApplyPressure(filteredInput, handbrakeInput);
        }
        else
        {
            car.brakes.ApplyPressure(Input.GetAxis("LeftTrigger"), handbrakeInput);
        }
        
    }

    IEnumerator ChangeGear()
    {
        isAcceleratorEnabled = false;
        car.engine.acceleratorInput = 0f;
        for (float clutchInput = 1f; clutchInput >= 0; clutchInput -= changeGearSpeed * Time.deltaTime)
        {
            car.clutch.clutchInput = clutchInput;
            yield return null;
        }

        car.clutch.clutchInput = 0;
        isAcceleratorEnabled = true;

    }

    private float GetFilteredAcceleratorInput (float input)
    {
        return Mathf.Clamp01(acceleratorFilter.Evaluate(input));
    }

    private float GetFilteredBrakeInput (float input)
    {
        return Mathf.Clamp01(brakesFilter.Evaluate(input));
    }

    private float GetFilteredSteerInput (float input)
    {
        return Mathf.Clamp(input, -1, 1);
    }

}
