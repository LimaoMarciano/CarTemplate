using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CarTemplate
{
    public class Car : MonoBehaviour
    {
        public EngineData engineData;
        public GearboxData gearboxData;
        public float turnRadius = 10f;

        public enum DrivenAxle { front, rear };
        public DrivenAxle drivenAxle;
        public Axle frontAxle;
        public Axle rearAxle;
        
        [HideInInspector]
        public float speed = 0f;

        public Engine engine = new Engine();
        public Differential differential = new Differential();
        public Gearbox gearbox = new Gearbox();
        public Clutch clutch = new Clutch();

        public Steering steering = new Steering();

        private WheelCollider[] drivenWheels;

        // Start is called before the first frame update
        void OnEnable()
        {
            //Get car parts data
            engine.data = engineData;
            gearbox.data = gearboxData;

            //Setting up which axle will be driven by the differential
            switch (drivenAxle)
            {
                case DrivenAxle.front:
                    differential.axle = frontAxle;
                    break;
                case DrivenAxle.rear:
                    differential.axle = rearAxle;
                    break;
            }

            //Setting up drive train parts connections
            engine.torqueOutputDriveTrain = clutch;
            engine.rpmOutputDriveTrain = clutch;

            clutch.torqueOutputDriveTrain = gearbox;
            clutch.rpmOutputDriveTrain = engine;

            gearbox.torqueOutputDriveTrain = differential;
            gearbox.rpmOutputDriveTrain = clutch;

            differential.rpmOutputDriveTrain = gearbox;

            //Setting up steering
            steering.axle = frontAxle;
            steering.turnRadius = turnRadius;
            steering.rearAxleTrack = rearAxle.GetAxleTrack();
            steering.wheelBase = Vector3.Distance(frontAxle.GetAxleMidPoint(), rearAxle.GetAxleMidPoint());
            Debug.Log("Rear track: " + steering.rearAxleTrack + ", Wheel base: " + steering.wheelBase);

        }

        // Update is called once per frame
        void Update()
        {
            
            speed = GetComponent<Rigidbody>().velocity.magnitude;
            
        }
    }
}

