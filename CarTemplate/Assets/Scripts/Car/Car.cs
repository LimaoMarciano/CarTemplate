using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CarTemplate
{
    public class Car : MonoBehaviour
    {
        public EngineData engineData;
        public GearboxData gearboxData;
        public WheelCollider[] drivenWheels;
        public float speed = 0f;

        public Engine engine = new Engine();
        public Differential differential = new Differential();
        public Gearbox gearbox = new Gearbox();
        public Clutch clutch = new Clutch();


        // Start is called before the first frame update
        void OnEnable()
        {
            engine.data = engineData;
            gearbox.data = gearboxData;
            differential.wheels = drivenWheels;

            engine.torqueOutputDriveTrain = clutch;
            engine.rpmOutputDriveTrain = clutch;

            clutch.torqueOutputDriveTrain = gearbox;
            clutch.rpmOutputDriveTrain = engine;

            gearbox.torqueOutputDriveTrain = differential;
            gearbox.rpmOutputDriveTrain = clutch;

            //differential.torqueOutput = gearbox;
            differential.rpmOutputDriveTrain = gearbox;

            //engine.torqueOutput = differential;
            //engine.rpmOutput = differential;

            //differential.torqueOutput = engine;
            //differential.rpmOutput = engine;
        }

        // Update is called once per frame
        void Update()
        {
            engine.acceleratorInput = Input.GetAxis("Vertical");

            if (Input.GetButtonDown("Fire1"))
            {
                gearbox.IncreaseGear();
                //StopCoroutine(changeGearCoroutine);
                //StartCoroutine(changeGearCoroutine);
            }

            if (Input.GetButtonDown("Fire3"))
            {
                gearbox.DecreaseGear();
                //StopCoroutine(changeGearCoroutine);
                //StartCoroutine(changeGearCoroutine);

            }

            float triggerTest = Input.GetAxis("RightTrigger");
            clutch.clutchInput = triggerTest;

            differential.Update();

            speed = GetComponent<Rigidbody>().velocity.magnitude;
            
        }
    }
}

