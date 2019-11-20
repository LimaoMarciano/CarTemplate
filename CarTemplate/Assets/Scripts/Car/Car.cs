using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CarTemplate
{
    public class Car : MonoBehaviour
    {
        public EngineData engineData;
        public GearboxData gearboxData;
        public BrakesData brakesData;

        [Range(0,1)]
        public float brakeBias = 0.5f;
        public float turnRadius = 10f;
        public float frontAntiRollForce = 5000f;
        public float rearAntiRollForce = 5000f;

        public enum DrivenAxle { front, rear };
        public DrivenAxle drivenAxle;
        public Axle frontAxle;
        public Axle rearAxle;

        public Vector3 centerOfMass;
        
        [HideInInspector]
        public float speed = 0f;

        //Drivetrain parts
        public Engine engine = new Engine();
        public Differential differential = new Differential();
        public Gearbox gearbox = new Gearbox();
        public Clutch clutch = new Clutch();

        //Individual systems
        public Steering steering = new Steering();
        public Brakes brakes = new Brakes();
        public AntiRollBar frontAntiRollBar = new AntiRollBar();
        public AntiRollBar rearAntiRollBar = new AntiRollBar();

        private Rigidbody rb;

        // Start is called before the first frame update
        void OnEnable()
        {
            //Setting up rigidbody
            rb = GetComponent<Rigidbody>();
            rb.centerOfMass = centerOfMass;

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

            //Setting up brakes
            brakes.data = brakesData;
            brakes.brakeBias = brakeBias;
            brakes.frontAxle = frontAxle;
            brakes.rearAxle = rearAxle;

            //Setting up anti roll bars
            frontAntiRollBar.axle = frontAxle;
            frontAntiRollBar.strength = frontAntiRollForce;
            frontAntiRollBar.rb = rb;

            rearAntiRollBar.axle = rearAxle;
            rearAntiRollBar.strength = rearAntiRollForce;
            rearAntiRollBar.rb = rb;

        }

        // Update is called once per frame
        void Update()
        {
            
            speed = rb.velocity.magnitude;
            brakes.brakeBias = brakeBias;
            differential.Update();
            frontAntiRollBar.Update();
            rearAntiRollBar.Update();

        }

        private void OnDrawGizmos()
        {
            drawCenterOfMass();
        }

        // Gizmos drawing methods
        private void drawCenterOfMass()
        {
            Vector3 pos = transform.position;
            pos += transform.right * centerOfMass.x;
            pos += transform.up * centerOfMass.y;
            pos += transform.forward * centerOfMass.z;

            drawGizmoAtPosition(pos, 0.5f, Color.yellow);
        }

        private void drawGizmoAtPosition(Vector3 pos, float size, Color color)
        {
            float halfSize = size * 0.5f;
            Debug.DrawLine(new Vector3(pos.x + halfSize, pos.y, pos.z), new Vector3(pos.x - halfSize, pos.y, pos.z), color);
            Debug.DrawLine(new Vector3(pos.x, pos.y + halfSize, pos.z), new Vector3(pos.x, pos.y - halfSize, pos.z), color);
            Debug.DrawLine(new Vector3(pos.x, pos.y, pos.z + halfSize), new Vector3(pos.x, pos.y, pos.z - halfSize), color);
        }
    }
}

