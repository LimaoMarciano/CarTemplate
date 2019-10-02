using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CarTemplate;

public class DriveTrainDebug : MonoBehaviour
{
    public string panelLabel;
    public DriveTrain driveTrain;
    public Text label;
    public Text inputRPM;
    public Text inputTorque;
    public Text outputRPM;
    public Text outputTorque;

    // Start is called before the first frame update
    void Start()
    {
        label.text = panelLabel;
    }

    // Update is called once per frame
    void Update()
    {
        inputRPM.text = driveTrain.InputRPM.ToString("F1");
        inputTorque.text = driveTrain.InputTorque.ToString("F1");
        outputRPM.text = driveTrain.OutputRPM.ToString("F1");
        outputTorque.text = driveTrain.OutputTorque.ToString("F1");
    }
}
