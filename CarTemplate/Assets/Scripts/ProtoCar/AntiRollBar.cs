using UnityEngine;
using System.Collections;


public class AntiRollBar : MonoBehaviour {
	public WheelCollider wheelL;
	public WheelCollider wheelR;
	public float antiRollVal = 5000f;

	private Rigidbody rBody;

	// Use this for initialization
	void Start () {
		rBody = GetComponent<Rigidbody> ();
	}

	// Update is called once per frame
	void Update () {
		WheelHit hit;
		float travelL=1.0f;
		float travelR=1.0f;
		bool groundedL = wheelL.GetGroundHit(out hit);
		if (groundedL){
			travelL = (-wheelL.transform.InverseTransformPoint(hit.point).y - wheelL.radius) / wheelL.suspensionDistance;
		}
		bool groundedR = wheelR.GetGroundHit(out hit);
		if (groundedR){
			travelR = (-wheelR.transform.InverseTransformPoint(hit.point).y - wheelR.radius) / wheelR.suspensionDistance;


		}

		float antiRollForce = (travelL - travelR) * antiRollVal;



		if (groundedL)

			rBody.AddForceAtPosition(wheelL.transform.up * -antiRollForce,

				wheelL.transform.position);  

		if (groundedR)

			rBody.AddForceAtPosition(wheelR.transform.up * antiRollForce,

				wheelR.transform.position);  
	}
}
