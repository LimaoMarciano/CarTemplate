using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CarTemplate;

public class CarController : MonoBehaviour
{

    public Car car;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        car.engine.acceleratorInput = Input.GetAxis("Vertical");
        car.steering.SetSteeringInput(Input.GetAxis("Horizontal"));

        if (Input.GetButtonDown("Fire1"))
        {
            car.gearbox.IncreaseGear();
            //StopCoroutine(changeGearCoroutine);
            //StartCoroutine(changeGearCoroutine);
        }

        if (Input.GetButtonDown("Fire3"))
        {
            car.gearbox.DecreaseGear();
            //StopCoroutine(changeGearCoroutine);
            //StartCoroutine(changeGearCoroutine);

        }

        float triggerTest = Input.GetAxis("RightTrigger");
        car.clutch.clutchInput = triggerTest;

        car.differential.Update();
    }
}
