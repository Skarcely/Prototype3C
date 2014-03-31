using UnityEngine;
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
	private float angleZ;
	
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
	private bool playerIsModifying;
	
	[HideInInspector]
	public bool wasMoving;
	[HideInInspector]
	public bool wasStanding;
	
	//NVARS
	private Quaternion lastLookRot;
	private Vector3 lastCamPos;
	
	// Use this for initialization
	void Start () {
		VarInitialize();
	}
	
	void VarInitialize()
	{
//		wasStanding = true;
//		wasMoving = false;
		
		playerCanRotate = true;	
		pivotOffset = followTarget.position;
	}
	
	void Update()
	{
		playerIsMoving = (GameObject.FindObjectOfType(System.Type.GetType ("TPControllerV2")) as TPControllerV2).isMoving;
		playerIsModifying = (GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).isModifying;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		
		if(playerIsModifying)
		{
			this.transform.LookAt((GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify.transform);
		}
		
		if(playerIsMoving == true && playerCanRotate == true)
		{
			
			if (Time.deltaTime == 0 || Time.timeScale == 0 || player == null) 
			return;
		
			CheckRightStickInputs();
			
			//La verticalité ne peut pas dépasser un min et un max
			angleV = Mathf.Clamp(angleV, minVerticalAngle, maxVerticalAngle);
		
//			float prevDist = (aimTarget.position - cam.position).magnitude;
			
			// Set aim rotation
			Quaternion aimRotation = Quaternion.Euler(-angleV, angleH, 0);
			Quaternion camYRotation = Quaternion.Euler(0, angleH, 0);
			this.transform.rotation = aimRotation;
				
//			#region Récupère la position du joueur et détermine la position de la caméra en smoothant les transitions
//			// Find far and close position for the camera
//			smoothPlayerPos = Vector3.Lerp(smoothPlayerPos, player.position, smoothTime * Time.deltaTime);
//			smoothPlayerPos.x = player.position.x;
//			smoothPlayerPos.z = player.position.z;
//			Vector3 farCamPoint = smoothPlayerPos + camYRotation * pivotOffset + aimRotation * camOffset;
//			Vector3 closeCamPoint = player.position + camYRotation * closeOffset;
//			float farDist = Vector3.Distance(farCamPoint, closeCamPoint);
//			// Smoothly increase maxCamDist up to the distance of farDist
//			maxCamDist = Mathf.Lerp(maxCamDist, farDist, 5 * Time.deltaTime);
//			#endregion
//				
//			// Camera setting
//			this.transform.position = closeCamPoint + closeToFarDir * maxCamDist;
//			
//			aimTargetDist = Mathf.Max(5, prevDist);
//			
//			aimTarget.position = this.transform.position + this.transform.forward * aimTargetDist;
			
			targetPosition = followTarget.position + followTarget.up * distanceUp - followTarget.forward * distanceAway;
			this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, Time.deltaTime * smoothTime);
				
//			if(wasStanding)	
//			{				
//				player.transform.LookAt(this.transform.position + this.transform.forward*40);
//					
//				wasStanding = false;
//				wasMoving = true;
//			}
//			else
//			{
//				//Cam rotation
//				rotation = Quaternion.LookRotation(followTarget.position - transform.position);
//				this.transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * dampTime);
//				
//				//Cam position
//				targetPosition = followTarget.position + followTarget.up * distanceUp - followTarget.forward * distanceAway;
//				this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, Time.deltaTime * smoothTime);
//
//				lastCamPos = this.transform.position;
//				lastLookRot = this.transform.rotation;
//				wasMoving = true;
//			}
//		
//			
//		}
//		
//		//Si le personnage ne bouge pas et que le joueur peut bouger la cam. Checker les inputs
//		if(playerIsMoving == false && playerCanRotate == true)
//		{
//			//Si le perso était en train de bouger, prendre les anciennes valeurs de cam pour le 1er placement
//			if(wasMoving)
//			{
//				angleV = lastLookRot.eulerAngles.y;
//				angleH = lastLookRot.eulerAngles.x;
//				
//				wasMoving = false;
//				wasStanding = true;
//			}
//			else
//			{
//				CheckRightStickInputs();
//			}
//			
//			FreeCameraMovement();	
		}
		
	}

	void FreeCameraMovement()
	{
		//Clamp de Angle V entre les angles Y min et max		
		angleH = Mathf.Clamp(angleH, minVerticalAngle, maxVerticalAngle);
		
		aimRotation = Quaternion.Euler(angleH, angleV, 0);
		this.transform.rotation = Quaternion.Slerp(lastLookRot, aimRotation, Time.deltaTime * smoothTime);

		Quaternion camYRotation = Quaternion.identity;
	
		this.transform.position = Vector3.Lerp(lastCamPos, player.position + camYRotation * pivotOffset + aimRotation * new Vector3(0.0f, distanceUp, -distanceAway), Time.deltaTime * smoothTime);

		// Store last position
		lastCamPos = this.transform.position;
		lastLookRot = this.transform.rotation;
	}
		
	void ResetCamera()
	{
		//Cam rotation
		rotation = Quaternion.LookRotation(followTarget.position - transform.position);
		this.transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * dampTime);
		
		//Cam position
		targetPosition = followTarget.position + followTarget.up * distanceUp - followTarget.forward * distanceAway;
		this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, Time.deltaTime * smoothTime);
		
		lastCamPos = this.transform.position;
		lastLookRot = this.transform.rotation;
	}

	void CheckRightStickInputs()
	{
		//Définition de l'horizontalité entre -1 et 1
		angleV += Mathf.Clamp(Input.GetAxis("R_XAxis_1")  , -1, 1) * horizontalAimingSpeed * Time.deltaTime;
									
		//Définition de la verticalité entre -1 et 1
		angleH += Mathf.Clamp(Input.GetAxis("R_YAxis_1")  , -1, 1) * verticalAimingSpeed * Time.deltaTime;
				
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
