using System.Collections;
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
    private Clutch clutch;
    private float engineTorque;
    private float engineRpm;
    private float speed;
    private float tyreSlip;
    private int currentGear;

    private IEnumerator changeGearCoroutine;

    public Engine Engine
    {
        get { return engine; }
    }

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

    public float TyreSlip
    {
        get { return tyreSlip; }
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
        clutch = new Clutch();

        transmission.drivenWheels = drivenWheels;

        rb.centerOfMass = centerOfMass;

        changeGearCoroutine = ChangeGear();
    }

    // Update is called once per frame
    void Update()
    {
        float acceleratorInput = Input.GetAxis("Vertical");
        engine.SetAcceleratorInput(acceleratorInput);

        //speed = CalculateWheelSpeed(drivenWheels[0]);
        speed = rb.velocity.magnitude;

        
        

        if (Input.GetButtonDown("Fire1"))
        {
            transmission.IncreaseGear();
            StopCoroutine(changeGearCoroutine);    
            StartCoroutine(changeGearCoroutine);
        }

        if (Input.GetButtonDown("Fire3"))
        {
            transmission.DecreaseGear();
            StopCoroutine(changeGearCoroutine);
            StartCoroutine(changeGearCoroutine);

        }

        currentGear = transmission.CurrentGear;

        if (currentGear == 0)
        {
            engine.isEngaged = false;
        }
        else
        {
            engine.isEngaged = true;
        }

        clutch.SetInputRpm(transmission.GetTransmissionRpm());
        engine.SetInputRpm(clutch.GetClutchRpmOutput());
        engine.EngineUpdate();
        clutch.SetInputTorque(engine.OutputTorque);
        transmission.ApplyTorque(clutch.GetClutchTorque());
        //engineRpm = transmission.GetTransmissionRpm();
        //engineTorque = engine.GetTorqueFromRpm(engineRpm);
        //transmission.ApplyTorque(engineTorque);
        //transmission.ApplyEngineBrake(1000.0f, engine.EngineRpm, engine.maxRpm);

        //tyreSlip = (CalculateWheelSpeed(drivenWheels[0]) - speed) * 3.6f;

    }

    private void FixedUpdate()
    {
        tyreSlip = getTyreSlip(drivenWheels[0]);
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

    float getTyreSlip (WheelCollider wheel)
    {
        WheelHit hit = new WheelHit();
        if (wheel.GetGroundHit(out hit))
        {
            return hit.forwardSlip;
        }
        else
        {
            return 0f;
        }
    }

    float getTyreForce (WheelCollider wheel)
    {
        WheelHit hit = new WheelHit();
        if (wheel.GetGroundHit(out hit))
        {
            return hit.force;
        }
        else
        {
            return 0f;
        }
    }

    IEnumerator ChangeGear ()
    {

        for (float clutchInput = 0f; clutchInput < 1; clutchInput += 1 * Time.deltaTime )
        {
            engine.transmissionConnection = clutchInput;
            clutch.SetClutchInput(clutchInput);
            yield return null;
        }
        
    }
}
