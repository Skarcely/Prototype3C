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
	
	private bool playerIsReprogramming;
	
	[HideInInspector]
	public bool isMoving;
	
	[HideInInspector]
	public bool canMove;
	
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
		GetExternVar();
		
		//Debug
		Debug.DrawRay(this.transform.position, this.transform.forward, Color.magenta);
		
	}
	
	void LateUpdate()
	{
			
		if(stickDirection != Vector3.zero && canMove == true)
		{
			isMoving = true;
			previousAngle = this.transform.eulerAngles;
			
			//Fonctionnel
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
			
				Quaternion newRotation = Quaternion.LookRotation(composedTranslate);
				this.transform.rotation = Quaternion.Slerp(this.transform.rotation, newRotation, Time.deltaTime * smoothRotation);
			
				this.transform.Translate(Vector3.forward*Time.deltaTime*movementSpeed);
				
			}	
	
		}
		else
		{
			isMoving = false;			
		}

	}
	
	void CheckInputs()
	{
	
		XControllerAxis = Input.GetAxis("L_XAxis_1");
		YControllerAxis = Input.GetAxis("L_YAxis_1");
			
		stickDirection = new Vector3(-XControllerAxis, 0 , YControllerAxis);
	}
	
	
	void GetExternVar()
	{
	
		playerIsReprogramming = (GameObject.FindObjectOfType(System.Type.GetType("CrosshairLock")) as CrosshairLock).isModifying;
		
	}
	
	void VarInitialize()
	{
		isMoving = false;
		canMove = true;
	}
	
	public void FreezeMovement()
	{
		canMove = false;	
		
	}
	
	public void FreeMovement()
	{
		canMove = true;	
	}
}
