using UnityEngine;
using System.Collections;

public class TPController : MonoBehaviour {
	
	//Private
	private float speed = 0.0f;
	private float h = 0.0f;
	private float v = 0.0f;
	private Vector3 movementVector;
	
	private Vector3 cameraDirection;
	private bool isMoving;	
	
	//Public
	public Camera camera;
	public float jumpSpeed;
	
	[HideInInspector]
	public bool canMove;
	
	#region Methods
	void Start () {
		InitializeVar();
	}
	
	void Update () {
		
		CheckInputs();
		MovementBehaviour();
		
	}
	
	void LateUpdate()
	{
		// Si le personnage est en mouvement ou qu'il bouge et que le joueur dirige la caméra
		// Ou que le cadran est ouvert, le personnage regarde dans la direction de la caméra
		if( isMoving ||
			(isMoving && (GameObject.FindObjectOfType(System.Type.GetType ("ThirdPersonShooterGameCamera")) as ThirdPersonShooterGameCamera).playerIsRotatingCamera == true) ||
			(GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).showCadran == true || 
			(GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).isModifying == true )
		{
			cameraDirection = camera.transform.forward;
			cameraDirection.y=0;
			
			if(cameraDirection.sqrMagnitude != 0.0f)
			{
				cameraDirection.Normalize();
				this.transform.LookAt(this.transform.position + cameraDirection);
				this.transform.GetChild(0).transform.forward = camera.transform.forward;
			}
		}
		
		
	}
	
	void InitializeVar()
	{
		speed = 5.0f;
		canMove = true;
		isMoving = false;
	}
	
	void MovementBehaviour()
	{	
		
		if(canMove == true)
		{	
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
		
	
	}
	
	
	public void FreezeMovement()
	{
		
		canMove = false;	
	}
	
	public void FreeMovement()
	{
		canMove = true;	
	}
	
	#endregion
	
}
