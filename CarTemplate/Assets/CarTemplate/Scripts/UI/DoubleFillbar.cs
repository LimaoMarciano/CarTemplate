using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoubleFillbar : MonoBehaviour
{
    public Text labelText;
    public Image fill01;
    public Image fill02;
    public Color fillColor01 = Color.blue;
    public Color fillColor02 = Color.cyan;
    public Color outOfRangeColor = Color.red;

    private float maxValue = 1f;
    public float value01 = 0f;
    public float value02 = 0f;

    // Use this for initialization
    void Start()
    {
        fill01.color = fillColor01;
        fill02.color = fillColor02;
    }

    // Update is called once per frame
    void Update()
    {
        float fillAmount = (value01 / maxValue);

        if (fillAmount > 1)
        {
            fill01.color = outOfRangeColor;
            fillAmount = 1;
        }
        else
        {
            fill01.color = fillColor01;
        }

        fill01.fillAmount = fillAmount;

        fillAmount = (value02 / maxValue);

        if (fillAmount > 1)
        {
            fill02.color = outOfRangeColor;
            fillAmount = 1;
        }
        else
        {
            fill02.color = fillColor02;
        }

        fill02.fillAmount = fillAmount;

    }

    public void SetLabel(string label)
    {
        labelText.text = label;
    }

    public void SetMaxValue(float max)
    {
        maxValue = max;
    }
}
