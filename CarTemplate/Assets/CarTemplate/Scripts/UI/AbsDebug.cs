using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbsDebug : MonoBehaviour
{
    public CarController carController;
    public Fillbar FLAbsGauge;
    public Fillbar FRAbsGauge;
    public Fillbar RLAbsGauge;
    public Fillbar RRAbsGauge;

    // Start is called before the first frame update
    void Start()
    {
        FLAbsGauge.SetMaxValue(1f);
        FRAbsGauge.SetMaxValue(1f);
        RLAbsGauge.SetMaxValue(1f);
        RRAbsGauge.SetMaxValue(1f);
    }

    // Update is called once per frame
    void Update()
    {
        FLAbsGauge.value = carController.abs.FrontLeftAbs.AllowedBrakePressure;
        FRAbsGauge.value = carController.abs.FrontRightAbs.AllowedBrakePressure;
        RLAbsGauge.value = carController.abs.RearLeftAbs.AllowedBrakePressure;
        RRAbsGauge.value = carController.abs.RearRightAbs.AllowedBrakePressure;
    }
}
