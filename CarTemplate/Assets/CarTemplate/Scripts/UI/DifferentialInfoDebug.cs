using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CarTemplate;

public class DifferentialInfoDebug : MonoBehaviour
{
    public enum TargetDifferential { front, rear, center }

    public Car car;
    public Image leftFillBar;
    public Image rightFillBar;
    public Text leftValue;
    public Text rightValue;
    public Text rpmDiffValue;
    public Car.DrivenAxle targetAxle;
    
    private Differential differential;

    void Start()
    {
        leftValue.text = "";
        rightValue.text = "";
        rpmDiffValue.text = "";
        leftFillBar.fillAmount = 0;
        rightFillBar.fillAmount = 0;

        switch (targetAxle)
        {
            case Car.DrivenAxle.front:
                differential = car.frontDifferential;
                break;

            case Car.DrivenAxle.rear:
                differential = car.rearDifferential;
                break;

            case Car.DrivenAxle.all:
                differential = car.centerDifferential;
                break;
        }

    }

    void Update()
    {

        if (car.drivenAxle == targetAxle || car.drivenAxle == Car.DrivenAxle.all)
        {
            float torqueSplit = differential.TorqueSplit;
            float left = Mathf.Clamp01((torqueSplit - 0.5f) / 0.5f);
            float right = Mathf.Clamp01((0.5f - torqueSplit) / 0.5f);

            leftValue.text = (torqueSplit * 100f).ToString("F0") + "%";
            rightValue.text = ((1 - torqueSplit) * 100f).ToString("F0") + "%";
            rpmDiffValue.text = (differential.RpmDifferenceRatio * 100).ToString("F0") + "%";

            leftFillBar.fillAmount = left;
            rightFillBar.fillAmount = right;
        }

    }

}
