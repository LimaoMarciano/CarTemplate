﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public EngineData engineData;
    public TransmissionData transmissionData;
    public List<WheelCollider> drivenWheels;
    public Vector3 centerOfMass;
    public Rigidbody rb;

    private Engine engine;
    private Transmission transmission;
    private float engineTorque;
    private float engineRpm;
    private float speed;
    private int currentGear;

    public float EngineTorque
    {
        get { return engineTorque; }
    }

    public float EngineRpm
    {
        get { return engineRpm; }
    }

    public float Speed
    {
        get { return speed; }
    }

    public int CurrentGear
    {
        get { return currentGear; }
    }

    // Start is called before the first frame update
    void Start()
    {
        engine = new Engine(engineData);
        transmission = new Transmission(transmissionData);

        transmission.drivenWheels = drivenWheels;

        rb.centerOfMass = centerOfMass;
    }

    // Update is called once per frame
    void Update()
    {
        float acceleratorInput = Input.GetAxis("Vertical");
        engine.SetAcceleratorInput(acceleratorInput);

        //speed = CalculateWheelSpeed(drivenWheels[0]);
        speed = rb.velocity.magnitude;

        currentGear = transmission.CurrentGear;

        if (Input.GetButtonDown("Fire1"))
        {
            transmission.IncreaseGear();
        }

        if (Input.GetButtonDown("Fire3"))
        {
            transmission.DecreaseGear();
        }

        engineRpm = transmission.GetTransmissionRpm();
        engineTorque = engine.GetTorqueFromRpm(engineRpm);
        transmission.ApplyTorque(engineTorque);
        transmission.ApplyEngineBrake(1000.0f, engineRpm, engine.maxRpm);

        Debug.Log((speed - CalculateWheelSpeed(drivenWheels[0])) * 3.6f);

    }

    private void OnDrawGizmos()
    {
        drawCenterOfMass();
    }

    float CalculateWheelSpeed(WheelCollider wheel)
    {
        float wheelRpm = wheel.rpm;
        float wheelCircunference = wheel.radius * 2.0f * Mathf.PI;
        float speed = (wheelRpm * wheelCircunference) / 60.0f;

        return speed;
    }


    void drawCenterOfMass()
    {
        Vector3 pos = transform.position;
        pos += transform.right * centerOfMass.x;
        pos += transform.up * centerOfMass.y;
        pos += transform.forward * centerOfMass.z;

        drawGizmoAtPosition(pos, 0.5f, Color.yellow);
    }

    void drawGizmoAtPosition(Vector3 pos, float size, Color color)
    {
        float halfSize = size * 0.5f;
        Debug.DrawLine(new Vector3(pos.x + halfSize, pos.y, pos.z), new Vector3(pos.x - halfSize, pos.y, pos.z), color);
        Debug.DrawLine(new Vector3(pos.x, pos.y + halfSize, pos.z), new Vector3(pos.x, pos.y - halfSize, pos.z), color);
        Debug.DrawLine(new Vector3(pos.x, pos.y, pos.z + halfSize), new Vector3(pos.x, pos.y, pos.z - halfSize), color);
    }
}