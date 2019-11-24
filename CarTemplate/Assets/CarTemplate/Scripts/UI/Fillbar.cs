using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Fillbar : MonoBehaviour {

	public Text labelText;
	public Text valueIndicator;
	public Image fill;
	public Color fillColor;

	private float maxValue = 1;
	public float value = 0;
	public bool isFractional = false;

	// Use this for initialization
	void Start () {
		fill.color = fillColor;
	}
	
	// Update is called once per frame
	void Update () {
		float fillAmount = (value / maxValue);

		if (fillAmount > 1) {
			fill.color = Color.red;
			fillAmount = 1;
		} else {
			fill.color = fillColor;
		}

		fill.fillAmount = fillAmount;
		if (isFractional) {
			valueIndicator.text = value.ToString ("F1") + " / " + maxValue.ToString ("F1");
		} else {
			valueIndicator.text = value.ToString ("F0") + " / " + maxValue.ToString ("F0");
		}
	}

	public void SetLabel (string label) {
		labelText.text = label;
	}

	public void SetMaxValue (float max) {
		maxValue = max;
	}
}
