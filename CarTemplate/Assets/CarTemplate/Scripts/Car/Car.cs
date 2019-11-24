using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CarTemplate
{
    /// <summary>
    /// Main car class, responsible for setting up a vehicle.
    /// It has references to all car parts, allowing to read info from them.
    /// <para>You may use this class as a template for creating other kinds of car</para>
    /// </summary>
    public class Car : MonoBehaviour
    {
        [Header("Engine and drivetrain")]
        public EngineData engineData;
        public GearboxData gearboxData;

        [Header("Brakes")]
        public BrakesData brakesData;
        [Range(0,1)]
        public float brakeBias = 0.5f;

        [Header("Tires")]
        public TyreModel frontTyreModel;
        public TyreModel rearTyreModel;

        [Header("Suspension")]
        public SuspensionModel frontSuspensionModel;
        public SuspensionModel rearSuspensionModel;

        [Header("Anti-roll bars")]
        public float frontAntiRollForce = 5000f;
        public float rearAntiRollForce = 5000f;

        public enum DrivenAxle { front, rear };

        [Header("Axles")]
        [Range(-1,1)]
        public float differentialLock = 0;
        public DrivenAxle drivenAxle;
        public Axle frontAxle;
        public Axle rearAxle;

        [Header("General")]
        public float turnRadius = 10f;
        public Vector3 centerOfMass;

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

        void OnEnable()
        {

            
            //Here's all steps to configure a car!
            
            //How many substeps per fixed update the vehicles will use.
            //This is a global value that affects all vehicles on scene
            frontAxle.leftWheel.ConfigureVehicleSubsteps(5, 5, 5);
            
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

            //Setting up if the differential will behave as a open or locked diff.
            differential.lockStrenght = differentialLock;

            //Setting up tyres
            frontAxle.SetTyreModel(frontTyreModel);
            rearAxle.SetTyreModel(rearTyreModel);

            //Setting up suspension
            frontAxle.SetSuspensionModel(frontSuspensionModel);
            rearAxle.SetSuspensionModel(rearSuspensionModel);

            //Setting up steering
            steering.axle = frontAxle;
            steering.turnRadius = turnRadius;
            steering.rearAxleTrack = rearAxle.GetAxleTrack();
            steering.wheelBase = Vector3.Distance(frontAxle.GetAxleMidPoint(), rearAxle.GetAxleMidPoint());

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

            //Setting up drive train parts connections
            // RPM path:    differential ==> gearbox ==> clutch ==> engine
            // Torque path: differential <== gearbox <== clutch <== engine

            engine.torqueOutputDriveTrain = clutch;
            engine.rpmOutputDriveTrain = clutch;

            clutch.torqueOutputDriveTrain = gearbox;
            clutch.rpmOutputDriveTrain = engine;

            gearbox.torqueOutputDriveTrain = differential;
            gearbox.rpmOutputDriveTrain = clutch;

            differential.rpmOutputDriveTrain = gearbox;
        }

        // Update is called once per frame
        void Update()
        {

            //Some parts needs to be updated every frame to gather info.
            //(Wheel rpm, suspension compression...)
            differential.Update();
            frontAntiRollBar.Update();
            rearAntiRollBar.Update();

        }

        private void OnDrawGizmos()
        {
            drawCenterOfMass();
        }

        // Gizmos drawing methods
        //========================================================================

        /// <summary>
        /// Draws the vehicle center of mass on Scene view
        /// </summary>
        private void drawCenterOfMass()
        {
            Vector3 pos = transform.position;
            pos += transform.right * centerOfMass.x;
            pos += transform.up * centerOfMass.y;
            pos += transform.forward * centerOfMass.z;

            drawGizmoAtPosition(pos, 0.5f, Color.yellow);
        }


        /// <summary>
        /// Draws a 3D cross gizmo at position
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="size"></param>
        /// <param name="color"></param>
        private void drawGizmoAtPosition(Vector3 pos, float size, Color color)
        {
            float halfSize = size * 0.5f;
            Debug.DrawLine(new Vector3(pos.x + halfSize, pos.y, pos.z), new Vector3(pos.x - halfSize, pos.y, pos.z), color);
            Debug.DrawLine(new Vector3(pos.x, pos.y + halfSize, pos.z), new Vector3(pos.x, pos.y - halfSize, pos.z), color);
            Debug.DrawLine(new Vector3(pos.x, pos.y, pos.z + halfSize), new Vector3(pos.x, pos.y, pos.z - halfSize), color);
        }
    }
}

