using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelGraphicBehaviour : MonoBehaviour
{

    private WheelCollider wheelCollider;
    
    // Start is called before the first frame update
    void Start()
    {
        wheelCollider = GetComponent<WheelCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        ApplyLocalPositionToVisuals(wheelCollider);
    }

    public void ApplyLocalPositionToVisuals(WheelCollider collider)
    {
        if (collider.transform.childCount == 0)
        {
            return;
        }

        Transform visualWheel = collider.transform.GetChild(0);

        Vector3 position;
        Quaternion rotation;
        collider.GetWorldPose(out position, out rotation);

        visualWheel.transform.position = position;
        visualWheel.transform.rotation = rotation;
    }

    void GetWheelSpeed ()
    {
        float circunference = wheelCollider.radius * 2 * Mathf.PI;
    }
}
