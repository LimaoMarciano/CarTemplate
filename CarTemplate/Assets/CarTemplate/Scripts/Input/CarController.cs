using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CarTemplate;

public class CarController : MonoBehaviour
{

    public Car car;
    public float changeGearSpeed = 5f;
    public float neutralChangeGearSpeed = 0.2f;
    public AnimationCurve acceleratorFilter;
    public AnimationCurve brakesFilter;
    public AnimationCurve steeringFilter;

    public bool useFilteredAccelerator = false;
    public bool useFilteredBrakes = false;
    public bool useFilteredSteering = false;
    bool isAcceleratorEnabled = true;

    private float clutchDownSpeed;
    private float clutchUpSpeed;

    private IEnumerator gearCO;

    // Start is called before the first frame update
    void Start()
    {
        gearCO = UpChangeGear();

        clutchDownSpeed = changeGearSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        
        float handbrakeInput = 0f;

        if (Input.GetButtonDown("Fire1"))
        {
            
            StopCoroutine(gearCO);

            //If it's parting from neutral, use different coroutine
            //that's more efficient from a stand still
            if (car.gearbox.CurrentGear == -1)
            {
                isAcceleratorEnabled = true;
                gearCO = ChangeGearFromNeutral();
            }
            else
            {
                isAcceleratorEnabled = false;
                gearCO = UpChangeGear();
            }
            
            StartCoroutine(gearCO);

        }

        if (Input.GetButtonDown("Fire3"))
        {

            StopCoroutine(gearCO);
            isAcceleratorEnabled = false;

            gearCO = DownChangeGear();
            StartCoroutine(gearCO);

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

    IEnumerator UpChangeGear()
    {
        car.engine.acceleratorInput = 0f;
        car.clutch.clutchInput = 0f;

        while (car.clutch.clutchInput < 1f)
        {
            car.clutch.clutchInput += changeGearSpeed * Time.deltaTime;
            yield return null;
        }

        car.clutch.clutchInput = 1;
        car.gearbox.IncreaseGear();

        while (car.clutch.clutchInput > 0)
        {
            car.clutch.clutchInput -= changeGearSpeed * Time.deltaTime;
            yield return null;
        }

        car.clutch.clutchInput = 0f;
        isAcceleratorEnabled = true;

    }

    IEnumerator DownChangeGear()
    {
        car.engine.acceleratorInput = 0f;
        car.clutch.clutchInput = 0f;

        while (car.clutch.clutchInput < 1f)
        {
            car.clutch.clutchInput += changeGearSpeed * Time.deltaTime;
            yield return null;
        }

        car.clutch.clutchInput = 1;
        car.gearbox.DecreaseGear();

        while (car.clutch.clutchInput > 0)
        {
            car.clutch.clutchInput -= changeGearSpeed * Time.deltaTime;
            yield return null;
        }

        car.clutch.clutchInput = 0f;
        isAcceleratorEnabled = true;

    }

    IEnumerator ChangeGearFromNeutral()
    {
        car.clutch.clutchInput = 0f;

        while (car.clutch.clutchInput < 1f)
        {
            car.clutch.clutchInput += changeGearSpeed * Time.deltaTime;
            yield return null;
        }

        car.clutch.clutchInput = 1;
        car.gearbox.IncreaseGear();

        while (car.clutch.clutchInput > 0.5)
        {
            car.clutch.clutchInput -= changeGearSpeed * Time.deltaTime;
            yield return null;
        }

        while (car.clutch.clutchInput > 0)
        {
            car.clutch.clutchInput -= neutralChangeGearSpeed * Time.deltaTime;
            yield return null;
        }

        car.clutch.clutchInput = 0f;

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
