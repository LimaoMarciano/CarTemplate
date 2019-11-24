using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Speedometer : MonoBehaviour {

	public Image needle;
	public Image redline;
	public Text speedText;
	public Text gear;
	public float minNeedle;
	public float maxNeedle;
	public float maxRpm;
	public float redlineRpm;
	public float rpm;
    public float currentSpeed = 0.0f;
    public int currentGear = 0;

	private float range;

	// Use this for initialization
	void Start () {
		range = maxNeedle - minNeedle;
		redline.fillAmount = 0.665f - ((redlineRpm * 0.665f) / maxRpm);
	}
	
	// Update is called once per frame
	void Update () {

        float speedKmh = Mathf.Abs(currentSpeed) * 3.6f;
		speedText.text = speedKmh.ToString("F0") + "<size=24> km/h</size>";

		if (currentGear >= 0) {
			gear.text = (currentGear + 1).ToString ();
		}
        else if (currentGear == -1)
        {
			gear.text = "N";
		} else
        {
            gear.text = "R";
        }

        float t = Mathf.Clamp01(Mathf.Abs(rpm) / maxRpm);

        float needleAngle = (range * t) + minNeedle;
		needle.rectTransform.localRotation = Quaternion.Euler (new Vector3 (0, 0, needleAngle));
	}
}
