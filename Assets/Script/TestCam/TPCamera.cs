﻿using UnityEngine;
using System.Collections;

public class TPCamera : MonoBehaviour {
	

	//Camera autoplacement
	public float distanceUp;
	public float distanceAway;
	public float smoothTime;
	public float dampTime;
	private Quaternion lastMovementCameraRotation;
	private Quaternion rotation;
	private Vector3 targetPosition;
	public Transform followTarget;

	//Camera Free
	private float angleH;
	private float angleV;
	public Transform player;
	public float horizontalAimingSpeed;
	public float verticalAimingSpeed;
	public float minVerticalAngle;
	public float maxVerticalAngle;
	private Vector3 pivotOffset;

	private Quaternion aimRotation;
	
	// Player behaviour variable
	[HideInInspector]
	public bool playerCanRotate;
	[HideInInspector]
	public bool playerIsRotatingCamera;
	private bool playerIsMoving;
	private bool wasMoving;
	private bool wasStanding;
	
	// Use this for initialization
	void Start () {
		VarInitialize();
	}
	
	void VarInitialize()
	{
		playerCanRotate = true;	
		pivotOffset = followTarget.position;
	}
	
	void Update(){
		
		playerIsMoving = (GameObject.FindObjectOfType(System.Type.GetType ("TPControllerV2")) as TPControllerV2).isMoving;
		
		//Debug.Log
//		Debug.DrawRay(followTarget.position, followTarget.forward, Color.blue);
//		Debug.Log ("wasStanding = " + wasStanding);
//		Debug.Log ("wasMoving = " + wasMoving);
	}
	
	// Update is called once per frame
	void LateUpdate () {
		
		if(playerIsMoving == true)
		{
			//Cam rotation
			rotation = Quaternion.LookRotation(followTarget.position - transform.position);
			this.transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * dampTime);
			
			//Cam position
			targetPosition = followTarget.position + followTarget.up * distanceUp - followTarget.forward * distanceAway;
			this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, Time.deltaTime * smoothTime);
		}
		
		if(playerIsMoving == false && playerCanRotate == true)
		{
			
			CheckRightStickInputs();
			FreeCameraMovement();	
		}
	}

	void FreeCameraMovement()
	{
			//Clamp de Angle V entre les angles Y min et max		
			angleV = Mathf.Clamp(angleV, minVerticalAngle, maxVerticalAngle);
			
			//Cam rotation
			aimRotation = Quaternion.Euler(-angleV, angleH, 0);
			Quaternion camYRotation = Quaternion.Euler(0, angleH, 0);
			this.transform.rotation = aimRotation;
		
			//Cam position
			this.transform.position = player.position + camYRotation * pivotOffset + aimRotation * new Vector3(0.0f, distanceUp, -distanceAway);;

	}
	
		

	void CheckRightStickInputs()
	{
		//Définition de l'horizontalité entre -1 et 1
		angleH += Mathf.Clamp(Input.GetAxis("R_XAxis_1")  , -1, 1) * horizontalAimingSpeed * Time.deltaTime;
									
		//Définition de la verticalité entre -1 et 1
		angleV += Mathf.Clamp(Input.GetAxis("R_YAxis_1")  , -1, 1) * verticalAimingSpeed * Time.deltaTime;
				
		if (Input.GetAxis("R_XAxis_1") !=0 || Input.GetAxis("R_YAxis_1") != 0 )
		{
			playerIsRotatingCamera = true;				
		}
		else
		{
			playerIsRotatingCamera = false;	
		}
	}
	
	
}
