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

        public Engine engine = new Engine();
        public Differential differential = new Differential();
        public Gearbox gearbox = new Gearbox();


        // Start is called before the first frame update
        void OnEnable()
        {
            engine.data = engineData;
            gearbox.data = gearboxData;
            differential.wheels = drivenWheels;

            engine.torqueOutput = gearbox;
            engine.rpmOutput = gearbox;

            gearbox.torqueOutput = differential;
            gearbox.rpmOutput = engine;

            //differential.torqueOutput = gearbox;
            differential.rpmOutput = gearbox;

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

            differential.Update();
            
            
        }
    }
}

