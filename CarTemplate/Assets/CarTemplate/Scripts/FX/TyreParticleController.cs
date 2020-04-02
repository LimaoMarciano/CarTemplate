using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CarTemplate;

public class TyreParticleController : MonoBehaviour
{

    public Car car;
    [Range(0,1)]
    public float forwardSlipLimit = 0.5f;
    [Range(0, 1)]
    public float sideSlipLimit = 0.99f;
    
    public enum AxleWheel { FrontRight, FrontLeft, RearRight, RearLeft  };

    public AxleWheel axleWheel = AxleWheel.FrontRight;

    private ParticleSystem tyreParticle;
    private Axle selectedAxle;
    private Wheel wheel;

    // Start is called before the first frame update
    void Start()
    {
        tyreParticle = GetComponent<ParticleSystem>();

        switch (axleWheel) {
            case AxleWheel.FrontLeft:
                wheel = car.frontAxle.leftWheel;
                break;
            case AxleWheel.FrontRight:
                wheel = car.frontAxle.rightWheel;
                break;
            case AxleWheel.RearLeft:
                wheel = car.rearAxle.leftWheel;
                break;
            case AxleWheel.RearRight:
                wheel = car.rearAxle.rightWheel;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        var emission = tyreParticle.emission;
        Debug.Log(wheel.collisionInfo.forwardSlip);

        float fSlip = Mathf.Abs(wheel.collisionInfo.forwardSlip);
        float sSlip = Mathf.Abs(wheel.collisionInfo.sidewaySlip);

        if (fSlip >= forwardSlipLimit || sSlip >= sideSlipLimit)
        {
            emission.enabled = true;

        }
        else
        {
            emission.enabled = false;
        }
    }
}
