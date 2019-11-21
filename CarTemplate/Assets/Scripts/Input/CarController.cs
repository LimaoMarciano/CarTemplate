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
        

        float handbrakeInput = 0f;

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

        if (Input.GetButton("Fire2"))
        {
            handbrakeInput = 1f;
        }

        car.engine.acceleratorInput = Input.GetAxis("RightTrigger");
        car.steering.SetSteeringInput(Input.GetAxis("Horizontal"));
        car.brakes.ApplyPressure(Input.GetAxis("LeftTrigger"), handbrakeInput);
    }
}
