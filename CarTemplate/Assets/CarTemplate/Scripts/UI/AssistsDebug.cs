using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CarTemplate;

public class AssistsDebug : MonoBehaviour
{

    public CarController carController;
    public ToggleIndicator abs;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (carController.abs.IsAbsActive)
        {
            abs.IsActive = true;
        }
        else
        {
            abs.IsActive = false;
        }
    }
}
