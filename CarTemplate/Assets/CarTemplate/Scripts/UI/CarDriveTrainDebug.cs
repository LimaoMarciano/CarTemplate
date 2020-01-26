using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CarTemplate;

public class CarDriveTrainDebug : MonoBehaviour
{

    public GameObject driveTrainDebugPrefab;
    public Car car;

    // Start is called before the first frame update
    void Start()
    {
        DriveTrainDebug engine = Instantiate(driveTrainDebugPrefab, gameObject.transform).GetComponent<DriveTrainDebug>();
        engine.driveTrain = car.engine;
        engine.panelLabel = "Engine";

        DriveTrainDebug clutch = Instantiate(driveTrainDebugPrefab, gameObject.transform).GetComponent<DriveTrainDebug>();
        clutch.driveTrain = car.clutch;
        clutch.panelLabel = "Clutch";

        DriveTrainDebug gearbox = Instantiate(driveTrainDebugPrefab, gameObject.transform).GetComponent<DriveTrainDebug>();
        gearbox.driveTrain = car.gearbox;
        gearbox.panelLabel = "Gearbox";

        if (car.drivenAxle == Car.DrivenAxle.front || car.drivenAxle == Car.DrivenAxle.rear)
        {
            DriveTrainDebug differential = Instantiate(driveTrainDebugPrefab, gameObject.transform).GetComponent<DriveTrainDebug>();
            differential.driveTrain = car.Differential;
            differential.panelLabel = "Differential";
        }
        else
        {
            DriveTrainDebug centerDifferential = Instantiate(driveTrainDebugPrefab, gameObject.transform).GetComponent<DriveTrainDebug>();
            centerDifferential.driveTrain = car.frontDifferential;
            centerDifferential.panelLabel = "Center Diff";

            DriveTrainDebug frontDifferential = Instantiate(driveTrainDebugPrefab, gameObject.transform).GetComponent<DriveTrainDebug>();
            frontDifferential.driveTrain = car.frontDifferential;
            frontDifferential.panelLabel = "Front Diff";

            DriveTrainDebug rearDifferential = Instantiate(driveTrainDebugPrefab, gameObject.transform).GetComponent<DriveTrainDebug>();
            rearDifferential.driveTrain = car.rearDifferential;
            rearDifferential.panelLabel = "Rear Diff";
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
