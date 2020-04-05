using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ToggleIndicator : MonoBehaviour
{

    public Color activeColor = Color.red;
    public Color inactiveColor = Color.white;

    private bool isActive = false;
    private Image indicator;

    public bool IsActive
    {
        get { return isActive; }
        set
        {
            isActive = value;
            UpdateColor();
        }
    }

    private void Start()
    {
        indicator = GetComponent<Image>();
        UpdateColor();
    }

    private void UpdateColor ()
    {
        if (isActive)
        {
            indicator.color = activeColor;
        }
        else
        {
            indicator.color = inactiveColor;
        }
    }

}
