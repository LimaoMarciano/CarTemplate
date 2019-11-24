using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WheelInfoDebug : MonoBehaviour
{
    [Header("Contact Force")]
    public Image forceImage;
    public Color minForceColor;
    public Color maxForceColor;
    public Text forceText;
    public float maxForce = 5000f;

    [Header("Slip")]
    public Image slipImage;
    public Color minSlipColor;
    public Color maxSlipColor;
    public Text sideSlipText;
    public Text forwardSlipText;
    public float maxSlip = 1f;

    public float force = 0f;
    public float sideSlip = 0f;
    public float forwardSlip = 0f;

    // Update is called once per frame
    void Update()
    {
        float forceT = Mathf.Clamp01(force / maxForce);
        float slipT = Mathf.Clamp01(Mathf.Max(Mathf.Abs(sideSlip), Mathf.Abs(forwardSlip)) / maxSlip);

        forceText.text = force.ToString("F0");
        sideSlipText.text = sideSlip.ToString("F2");
        forwardSlipText.text = forwardSlip.ToString("F2");

        forceImage.color = Color.Lerp(minForceColor, maxForceColor, forceT);
        slipImage.color = Color.Lerp(minSlipColor, maxSlipColor, slipT);
    }
}
