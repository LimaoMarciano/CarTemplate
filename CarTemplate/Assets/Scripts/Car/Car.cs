using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CarTemplate
{
    public class Car : MonoBehaviour
    {
        public EngineData engineData;
        public WheelCollider[] drivenWheels;

        public Engine engine = new Engine();
        public Differential differential = new Differential();
        public Gearbox gearbox = new Gearbox();


        // Start is called before the first frame update
        void OnEnable()
        {
            engine.engineData = engineData;
            differential.wheels = drivenWheels;

            engine.output = differential;
            engine.input = differential;

            differential.output = engine;
            differential.input = engine;
        }

        // Update is called once per frame
        void Update()
        {
            engine.acceleratorInput = Input.GetAxis("Vertical");
            
            differential.Update();
            
            
        }
    }
}

