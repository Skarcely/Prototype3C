using UnityEngine;
using System.Collections;

public class TPControllerV2 : MonoBehaviour {
	
	
	//Private
	private float XControllerAxis;
	private float YControllerAxis;
	private Vector3 stickDirection;
	private Vector3 movementDirection;
	
	private Vector3 previousAngle;
	private Vector3 cameraForward;
	private Vector3 cameraRight;
	
	[HideInInspector]
	public bool isMoving;
	
	//Public
	public float movementSpeed;
	public float smoothRotation;
	
	public Transform refCam;
	public Transform playerGraphics;
	
	// Use this for initialization
	void Start () {
		VarInitialize();
	}
	
	// Update is called once per frame
	void Update () {
		CheckInputs();
		
		//Debug
		Debug.DrawRay(this.transform.position, this.transform.forward, Color.magenta);
		
	}
	
	void LateUpdate()
	{
			
		if(stickDirection != Vector3.zero)
		{
			
//			Fonctionnel mais mouvements perso pas en ref à la cam
			
			//Rotate le player en fonction de l'input stick
//			this.transform.eulerAngles = new Vector3(this.transform.eulerAngles.x, Mathf.Atan2(XControllerAxis, YControllerAxis) * Mathf.Rad2Deg, this.transform.eulerAngles.z);
//			
//			this.transform.Translate(Vector3.forward * Time.deltaTime * movementSpeed);
			previousAngle = this.transform.eulerAngles;
			//Fonctionnel
			
			
			//Test 1
	
			//Get Forward and Right from Camera
			Vector3 modifiedDirForward = refCam.TransformDirection(Vector3.forward);
			modifiedDirForward.y = 0.0f;
			modifiedDirForward = modifiedDirForward.normalized;
			
			Vector3 modifiedDirRight = refCam.right;
			
			// Setting  x aand z to translate
			Vector3 xTranslate = modifiedDirRight * XControllerAxis * movementSpeed;
			Vector3 zTranslate = modifiedDirForward * YControllerAxis * movementSpeed;
			
			//Creating the movement vector
			Vector3 composedTranslate = Vector3.Lerp(xTranslate, zTranslate, 0.5f);
			
			if(composedTranslate != Vector3.zero)
			{
				isMoving = true;
				
				Debug.DrawRay(this.transform.position, composedTranslate, Color.red);
			
				Quaternion newRotation = Quaternion.LookRotation(composedTranslate);
				this.transform.rotation = Quaternion.Slerp(this.transform.rotation, newRotation, Time.deltaTime * smoothRotation);
			
				this.transform.Translate(Vector3.forward*Time.deltaTime*movementSpeed);
				
			}
			else
			{
				isMoving = false;
			}
			
	
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
			
		stickDirection = new Vector3(-XControllerAxis, 0 , YControllerAxis);
	}
	
	void VarInitialize()
	{
		isMoving = false;	
	}
}
