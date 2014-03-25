using UnityEngine;
using System.Collections;

public class ThirdPersonShooterGameCamera : MonoBehaviour {
	
	public Transform player;
	
	protected Transform aimTarget; // that was public and a gameobject had to be dragged on it. - ben0bi
	
	public float smoothingTime = 10.0f; // it should follow it faster by jumping (y-axis) (previous: 0.1 or so) ben0bi
	public Vector3 pivotOffset = new Vector3(0.2f, 0.7f,  0.0f); // offset of point from player transform (?) ben0bi
	public Vector3 camOffset   = new Vector3(0.0f, 0.7f, -3.4f); // offset of camera from pivotOffset (?) ben0bi
	public Vector3 closeOffset = new Vector3(0.35f, 1.7f, 0.0f); // close offset of camera from pivotOffset (?) ben0bi
	
	public float horizontalAimingSpeed = 800f; // was way to lame for me (270) ben0bi
	public float verticalAimingSpeed = 800f;   // --"-- (270) ben0bi
	public float maxVerticalAngle = 80f;
	public float minVerticalAngle = -80f;
	
	public float mouseSensitivity = 0.3f;
		
	private float angleH = 0;
	private float angleV = 0;
	private Transform cam;
	private float maxCamDist = 1;
	private LayerMask mask;
	private Vector3 smoothPlayerPos;
	
	[HideInInspector]
	public bool playerCanRotate;
	
	[HideInInspector]
	public bool playerIsRotatingCamera;
	
	// Use this for initialization
	void Start () 
	{
		VarInitialize();	
	}
	
	// Update is called once per frame
	void LateUpdate () {
				
		if (Time.deltaTime == 0 || Time.timeScale == 0 || player == null) 
			return;
		
		// Si le joueur peut diriger la caméra
		if( playerCanRotate == true)	
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
		
		//Si on est en train de modifier un objet : l'Objet devient la target de la caméra
		if((GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).isModifying == true)
		{
			cam.LookAt((GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify.transform);
			
		}
		// Sinon, on calcule la position de la cam
		else
		{
			//Y ne peut pas dépasser un min et un max définis dans l'inspector
			angleV = Mathf.Clamp(angleV, minVerticalAngle, maxVerticalAngle);
			
			// Before changing camera, store the prev aiming distance.
			// If we're aiming at nothing (the sky), we'll keep this distance.
			float prevDist = (aimTarget.position - cam.position).magnitude;
			
			// Set aim rotation
			Quaternion aimRotation = Quaternion.Euler(-angleV, angleH, 0);
			Quaternion camYRotation = Quaternion.Euler(0, angleH, 0);
			
			cam.rotation = aimRotation;
			
			#region Récupère la position du joueur et détermine la position de la caméra
			// Find far and close position for the camera
			smoothPlayerPos = Vector3.Lerp(player.position, player.position, smoothingTime * Time.deltaTime);
			smoothPlayerPos.x = player.position.x;
			smoothPlayerPos.z = player.position.z;
			
			Vector3 farCamPoint = smoothPlayerPos + camYRotation * pivotOffset + aimRotation * camOffset;
			Vector3 closeCamPoint = player.position + camYRotation * closeOffset;
			
			//Distance entre far et close. 
			//Distance = (Vecteur1-Vecteur2).magnitude
			float farDist = Vector3.Distance(farCamPoint, closeCamPoint);
			
			
			// Smoothly increase maxCamDist up to the distance of farDist
			maxCamDist = Mathf.Lerp(maxCamDist, farDist, 50 * Time.deltaTime);
			
			
			#endregion
			
						
			Vector3 closeToFarDir = (farCamPoint - closeCamPoint) / farDist;
			
			cam.position = closeCamPoint + closeToFarDir * maxCamDist;
			
			
			float aimTargetDist;
			aimTargetDist = Mathf.Max(5, prevDist);

			// Set the aimTarget position according to the distance we found.
			// Make the movement slightly smooth.
			aimTarget.position = (cam.position + cam.forward * aimTargetDist);
		
		}
		
	}
	
	void VarInitialize()
	{
	
		playerCanRotate = true;
	
		// [edit] no aimtarget gameobject needs to be placed anymore - ben0bi
		GameObject g=new GameObject();
		aimTarget=g.transform;
		
		// Add player's own layer to mask
		mask = 1 << player.gameObject.layer;
		
		// Add Igbore Raycast layer to mask
		mask |= 1 << LayerMask.NameToLayer("Ignore Raycast");
		
		// Invert mask
		mask = ~mask;
		
		cam = this.transform;
		smoothPlayerPos = player.position;
		
		maxCamDist = 3;
		
		
	}
	

}
