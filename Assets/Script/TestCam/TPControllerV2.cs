using UnityEngine;
using System.Collections;

public class TPControllerV2 : MonoBehaviour {
	
	private float XControllerAxis;
	private float YControllerAxis;
	private Vector3 movementVector;
	private Vector3 previousAngle;
	
	public float movementSpeed;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		CheckInputs();
		
		//Debug
		Debug.DrawRay(this.transform.position, this.transform.forward, Color.magenta);
	}
	
	void LateUpdate()
	{
		if(movementVector != Vector3.zero)
		{
			this.transform.eulerAngles = new Vector3(this.transform.eulerAngles.x, Mathf.Atan2(XControllerAxis, YControllerAxis) * Mathf.Rad2Deg, this.transform.eulerAngles.z);
			this.transform.Translate(Vector3.forward * Time.deltaTime * movementSpeed);
			previousAngle = this.transform.eulerAngles;
		}
		else
		{
		
			this.transform.eulerAngles = previousAngle;
			
		}

	}
	
	void CheckInputs()
	{
	
		XControllerAxis = Input.GetAxis("L_XAxis_1");
		YControllerAxis = Input.GetAxis("L_YAxis_1");
			
		movementVector = new Vector3(XControllerAxis, 0 , YControllerAxis);
	}
}
