using UnityEngine;
using System.Collections;

public class TPController : MonoBehaviour {
	
	//Private
	private float speed = 0.0f;
	private float h = 0.0f;
	private float v = 0.0f;
	private Vector3 movementVector;
	
	private Vector3 cameraDirection;
	private bool jumpIsPressed;
	private bool isJumping;
	private bool isMoving;
	
	
	//Public
	public Camera camera;
	public float jumpSpeed;
	
	
	[HideInInspector]
	public bool canMove;

	void Start () {
		InitializeVar();
	}
	
	void Update () {
		
		CheckInputs();
		MovementBehaviour();
		
	}
	
	void LateUpdate()
	{
		
		if(isMoving || (GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).showCadran == true)
		{
			cameraDirection = camera.transform.forward;
			cameraDirection.y=0;
			
			if(cameraDirection.sqrMagnitude != 0.0f)
			{
				
				cameraDirection.Normalize();
				this.transform.LookAt(this.transform.position + cameraDirection);
					
			}
		}
		
	}
	
	void InitializeVar()
	{
		speed = 5.0f;
		canMove = true;
		isJumping = false;
		isMoving = false;
	}
	
	void MovementBehaviour()
	{	
		if(canMove == true)
		{	
			if( jumpIsPressed == true && isJumping == false )
			{
				Jump();	
			}
			
			this.transform.Translate(movementVector*Time.deltaTime*speed);
	
		}
	}
	
	void CheckInputs()
	{
		h = Input.GetAxis("L_XAxis_1");
		v = Input.GetAxis("L_YAxis_1");
		
		movementVector = new Vector3(h, 0, v);
		
		if( h!=0 || v != 0)
		{
			isMoving = true;
		}
		else
		{
			
			isMoving = false;
		}
		
		if((GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).isModifying == false)
		{
			if(Input.GetButton("A_1"))
			{
				jumpIsPressed = true;
				
			}
		}
	
	}
	
	void Jump()
	{
		jumpIsPressed = false;
		isJumping = true;
		
		this.rigidbody.AddForce(Vector3.up * jumpSpeed*Time.deltaTime);	

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
