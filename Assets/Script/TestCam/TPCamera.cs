using UnityEngine;
using System.Collections;

public class TPCamera : MonoBehaviour {
	
	
	private bool wasStanding;
	private bool wasMoving;
	
	//Camera autoplacement
	public float distanceUp;
	public float distanceAway;
	public float smoothTime;
	public float dampTime;
	private Quaternion lastMovementCameraRotation;
	private Quaternion rotation;
	
	public Transform followTarget;

	
	//Camera Free
	public Transform player;
	public float horizontalAimingSpeed;
	public float verticalAimingSpeed;
	public float minVerticalAngle;
	public float maxVerticalAngle;
	public Vector3 pivotOffset = new Vector3(0.2f, 0.7f,  0.0f); // offset of point from player transform (?) ben0bi
	public Vector3 camOffset   = new Vector3(0.0f, 0.7f, -3.4f); // offset of camera from pivotOffset (?) ben0bi
	public Vector3 closeOffset = new Vector3(0.35f, 1.7f, 0.0f); // close offset of camera from pivotOffset (?) ben0bi
	private float maxCamDistance;
	protected Transform aimTarget;
	private Quaternion aimRotation;
	
	
	[HideInInspector]
	public bool playerCanRotate;
	[HideInInspector]
	public bool playerIsRotatingCamera;
	
	
	private Vector3 targetPosition;
	private bool playerIsMoving;
	
	private float angleH;
	private float angleV;
	
	
	// Use this for initialization
	void Start () {
		VarInitialize();
	}
	
	void VarInitialize()
	{
		playerCanRotate = true;	
		GameObject g=new GameObject();
		aimTarget=g.transform;
		wasMoving = false;
		wasStanding = false;
		
	}
	
	void Update(){
		
		playerIsMoving = (GameObject.FindObjectOfType(System.Type.GetType ("TPControllerV2")) as TPControllerV2).isMoving;
		
		//Debug.Log
//		Debug.Log ("playerIsMoving = " + playerIsMoving);
//		Debug.Log ("playerIsRotatingCamera = " + playerIsRotatingCamera);
		Debug.DrawRay(this.transform.position, this.transform.forward, Color.blue);
	}
	
	// Update is called once per frame
	void LateUpdate () {
		
		if(playerIsMoving == true)
		{
			
			rotation = Quaternion.LookRotation(followTarget.position - transform.position);
			this.transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * dampTime);
			
			targetPosition = followTarget.position + followTarget.up * distanceUp - followTarget.forward * distanceAway;
			this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, Time.deltaTime * smoothTime);
			this.transform.position = targetPosition;
		
			rotation = Quaternion.LookRotation(followTarget.position - transform.position);
			this.transform.rotation = Quaternion.Slerp(this.transform.rotation, rotation, Time.deltaTime * dampTime);
			Debug.Log ("Constraint rotation = "+ rotation);

		}
		
		if(playerIsMoving == false && playerCanRotate == true)
		{
			if(wasMoving = true)
			{
				wasMoving = false;
				wasStanding = true;	
			}
			
			CheckRightStickInputs();
			FreeCameraMovement();	
		}
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
	
	void FreeCameraMovement()
	{
			//Clamp de Angle V entre les angles Y min et max		
			angleV = Mathf.Clamp(angleV, minVerticalAngle, maxVerticalAngle);
			
			// Before changing camera, store the prev aiming distance.
			// If we're aiming at nothing (the sky), we'll keep this distance.
			float prevDist = (followTarget.position - this.transform.position).magnitude;
			
			aimRotation = Quaternion.Euler(-angleV, angleH, 0);
			Quaternion camYRotation = Quaternion.Euler(0, angleH, 0);
			
			this.transform.rotation = aimRotation;
			Debug.Log ("Free rotation = "+ aimRotation);
			
			#region Récupère la position du joueur et détermine la position de la caméra
			// Find far and close position for the camera
			Vector3 smoothPlayerPos = Vector3.Lerp(player.position, player.position, smoothTime * Time.deltaTime);
			smoothPlayerPos.x = player.position.x;
			smoothPlayerPos.z = player.position.z;
			
			Vector3 farCamPoint = smoothPlayerPos + camYRotation * pivotOffset + aimRotation * camOffset;
			Vector3 closeCamPoint = player.position + camYRotation * closeOffset;
			
			//Distance entre far et close. 
			//Distance = (Vecteur1-Vecteur2).magnitude
			float farDist = Vector3.Distance(farCamPoint, closeCamPoint);
			
			// Smoothly increase maxCamDist up to the distance of farDist
			maxCamDistance = Mathf.Lerp(maxCamDistance, farDist, 50 * Time.deltaTime);
			#endregion
					
			Vector3 closeToFarDir = (farCamPoint - closeCamPoint) / farDist;
			
			this.transform.position = closeCamPoint + closeToFarDir * maxCamDistance;
		
		
			float aimTargetDist;
			aimTargetDist = Mathf.Max(5, prevDist);

			// Set the aimTarget position according to the distance calculated.
			// Make the movement slightly smooth.
			aimTarget.forward = player.forward;
			aimTarget.position = (this.transform.position + this.transform.forward * aimTargetDist);
		
	}
	
}
