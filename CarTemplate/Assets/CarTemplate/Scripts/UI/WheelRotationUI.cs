using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class WheelRotationUI : MonoBehaviour
{

    [Range(-1,1)]
    public float input;
    public float wheelRotation = 540f;

    Image wheelImage;
    private Vector3 eulerAngle;
    
    // Start is called before the first frame update
    void Start()
    {
        wheelImage = GetComponent<Image>();
        eulerAngle = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        float zRot = Mathf.Lerp(wheelRotation, -wheelRotation, (input + 1) / 2);
        eulerAngle.z = zRot;
        wheelImage.rectTransform.rotation = Quaternion.Euler(eulerAngle);
    }
}
