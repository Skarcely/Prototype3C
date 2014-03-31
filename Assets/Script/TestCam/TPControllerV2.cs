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
	
	private bool isJumping;
	private bool jumpIsPressed;

	[HideInInspector]
	public bool isMoving;
	
	[HideInInspector]
	public bool canMove;
	
	[HideInInspector]
	public bool canBoost;
	[HideInInspector]
	public bool boostIsPressed;
	
	[HideInInspector]
	public bool isGrabbing;
	
	
	//Public
	public float movementSpeed;
	public float smoothRotation;
	
	public float boostCoef;
	
	public Transform refCam;
	public Transform playerGraphics;
	
	public int jumpHeight;
	public int jumpSpeed;
	public float airControl;
	
	// Use this for initialization
	void Start () {
		VarInitialize();
	}
	
	// Update is called once per frame
	void Update () {
		CheckInputs();
		GetExternVar();
			
		if(stickDirection != Vector3.zero && canMove == true)
		{
			isMoving = true;
			
			//Cam Vectors
			Vector3 modifiedDirForward = refCam.forward;
			modifiedDirForward.y = 0.0f;
			modifiedDirForward = modifiedDirForward.normalized;
			Vector3 modifiedDirRight = refCam.right;
			
			// Setting  x and z to translate
			Vector3 xTranslate = modifiedDirRight * XControllerAxis * movementSpeed;
			Vector3 zTranslate = modifiedDirForward * YControllerAxis * movementSpeed;
			
			//Creating the movement vector
			Vector3 composedTranslate = Vector3.Lerp(xTranslate, zTranslate, 0.5f);
			
			if(composedTranslate != Vector3.zero && (isJumping ==false))
			{
				
				if((GameObject.FindObjectOfType(System.Type.GetType ("TPCamera")) as TPCamera).wasStanding == true)
				{
					this.transform.rotation = refCam.rotation;
					(GameObject.FindObjectOfType(System.Type.GetType ("TPCamera")) as TPCamera).wasStanding = false;
				}
				else
				{
					Quaternion newRotation = Quaternion.LookRotation(composedTranslate);
					this.transform.rotation = Quaternion.Slerp(this.transform.rotation, newRotation, Time.deltaTime * smoothRotation);
				}
				
				if(boostIsPressed)
				{
					this.transform.Translate(Vector3.forward*Time.deltaTime*movementSpeed*boostCoef);
				}
				else
				{
					this.transform.Translate(Vector3.forward*Time.deltaTime*movementSpeed);
//					this.transform.Translate(refCam.transform.rotation * Vector3.forward * Time.deltaTime*movementSpeed);
				}
			}			
		}		
		else
		{
			isMoving = false;			
		}
		
		//Debug
		Debug.DrawRay(this.transform.position, this.transform.forward, Color.yellow);
		
	}
	

	void CheckInputs()
	{
		
		//Check jump input
		if((Input.GetButtonDown("A_1")) && (isJumping == false))
		{
			Jump();

		}
		
		if(Input.GetButton("X_1") && isJumping ==false)
		{
			boostIsPressed = true;	
		}
		else
		{
			boostIsPressed = false;
		}
		
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
		isGrabbing = false;
		canMove = true;
	}
	
	
	//Setters
	public void FreezeMovement()
	{
		canMove = false;	
		
	}
	
	public void FreeMovement()
	{
		canMove = true;	
	}
	
	void OnCollisionEnter(Collision collision)
	{
		//Check if caracter is grounded
		if ((collision.gameObject.tag == "Ground") || ( collision.gameObject.tag == "Cube"))
		{			
			isJumping = false;	
			FreeMovement();
		}
		

	}
	
	void OnTriggerEnter(Collider collider)
	{
		if(collider.gameObject.tag == "GrabZone")
		{
			Debug.Log ("In GrabZone");
			
			Ray grabbingRay = new Ray(this.transform.GetChild(2).transform.position, this.transform.forward);
			RaycastHit hitTarget;
			
			if(Physics.Raycast(grabbingRay, out hitTarget,20))
			{
				Debug.Log (hitTarget.transform.tag);
				
				if(hitTarget.transform.tag == "Cube")
				{
					
					
					isGrabbing = true;
					
				}
				else
				{
					isGrabbing = false;
				}
			}
			
		}
		else
		{
			isGrabbing = false;
		}
		
	}

	void Jump()
	{
		jumpIsPressed = false;
		isJumping = true;
		//Jump force up an forward
		this.rigidbody.AddForce(Vector3.up * jumpHeight*Time.deltaTime*1000 + this.transform.forward*jumpSpeed*Time.deltaTime*1000);
	}
	
}
