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

        DriveTrainDebug differential = Instantiate(driveTrainDebugPrefab, gameObject.transform).GetComponent<DriveTrainDebug>();
        differential.driveTrain = car.differential;
        differential.panelLabel = "Differential";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
