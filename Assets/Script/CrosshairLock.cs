﻿using UnityEngine;
using System.Collections;

public class CrosshairLock : MonoBehaviour {
	
	
	//Public
	public Texture2D crosshairNormal;
	public Texture2D crosshairLock;
	public Texture2D cadran;
	public Texture2D cadran_TranslateX_Normal;
	public Texture2D cadran_TranslateX_Selected;
	public Texture2D cadran_TranslateY_Normal;
	public Texture2D cadran_TranslateY_Selected;
	public Texture2D cadran_ScaleX_Normal;
	public Texture2D cadran_ScaleX_Selected;
	public Texture2D cadran_ScaleY_Normal;
	public Texture2D cadran_ScaleY_Selected;
	
	//Private
	private int xRayTarget;
	private int yRayTarget;
	
	private Ray rayCamToTarget;
	
	[HideInInspector]
	public bool isLocking;
	[HideInInspector]
	public bool showCadran;
	
	[HideInInspector]
	public bool isModifying;
	
	//TranslateX
	private bool translateXAvailable;
	private bool translateXActivated;
	
	//TranslateY
	private bool translateYAvailable;
	private bool translateYActivated;
	
	//ScaleX
	private bool scaleXAvailable;
	private bool scaleXActivated;
	
	//ScaleY
	private bool scaleYAvailable;
	private bool scaleYActivated;
	
	
	private int activeMode = 0; // Langage actif
	private int nbModes = 2; // Nombre de langages possedes
	
	private RaycastHit hitTarget;
	
	[HideInInspector]
	public GameObject targetToModify;
	[HideInInspector]
	public Vector3 targetStorePosition;
	
	// Use this for initialization
	void Start () 
	{
	
		VarInitialize();
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Raycast
		rayCamToTarget = camera.ScreenPointToRay(new Vector3(xRayTarget,yRayTarget));
		
		//Check hit Method
		CheckRayHit();
		
		//Check player's input
		CheckInputs();
			
	}
	
	void OnGUI () 
	{
		// Affiche le crosshair de base
		if(isLocking == false )
		{
			GUI.DrawTexture(new Rect(Screen.width/2 - (crosshairNormal.width/2), Screen.height/2 - (crosshairNormal.height/2), 32,32), crosshairNormal);
		}
		
		//Si passe sur un cube
		if(isLocking == true && showCadran == false)
		{
			GUI.DrawTexture(new Rect(Screen.width/2 - (crosshairLock.width/2), Screen.height/2 - (crosshairLock.height/2), 32,32), crosshairLock);	
		}
		
		//Si Appuie sur LT
		if(showCadran == true)
		{
			GUI.DrawTexture(new Rect(Screen.width/2 - (cadran.width/2), Screen.height/2 - (cadran.height/2), 256,256), cadran);
			
			#region Translate X
			//Si le translate X est dispo
			if(translateXAvailable == true)
			{
				
				GUI.DrawTexture(new Rect(Screen.width/2 - (cadran.width/2), Screen.height/2 - (cadran.height/2), 128,128), cadran_TranslateX_Normal);
				
			}
			
			//Si le joystick droit pointe sur translateX
			if(translateXActivated == true)
			{
			
				GUI.DrawTexture(new Rect(Screen.width/2 - (cadran.width/2), Screen.height/2 - (cadran.height/2), 128,128), cadran_TranslateX_Selected);
				
			}
			#endregion
			
			#region TranslateY
			
			if(translateYAvailable == true)
			{
				
				GUI.DrawTexture(new Rect(Screen.width/2, Screen.height/2 - (cadran.height/2), 128,128), cadran_TranslateY_Normal);
				
			}
			if(translateYActivated == true)
			{
			
				GUI.DrawTexture(new Rect(Screen.width/2, Screen.height/2 - (cadran.height/2), 128,128), cadran_TranslateY_Selected);
				
			}
			
			//Si le scale X est dispo
			if(scaleXAvailable == true)
			{
				
				GUI.DrawTexture(new Rect(Screen.width/2 - (cadran.width/2), Screen.height/2 - (cadran.height/2), 128,128), cadran_ScaleX_Normal);
				
			}
			if(scaleXActivated == true)
			{
				
				GUI.DrawTexture(new Rect(Screen.width/2 - (cadran.width/2), Screen.height/2 - (cadran.height/2), 128,128), cadran_ScaleX_Selected);
				
			}
			
			// Si le scale Y est dispo
			if(scaleYAvailable == true)
			{
				
				GUI.DrawTexture(new Rect(Screen.width/2, Screen.height/2 - (cadran.height/2), 128,128), cadran_ScaleY_Normal);
				
			}
			if(scaleYActivated == true)
			{
				
				GUI.DrawTexture(new Rect(Screen.width/2, Screen.height/2 - (cadran.height/2), 128,128), cadran_ScaleY_Selected);
				
			}
			
			
			if (activeMode == 0)
			{
				// premier cadran a afficher ici
				
				translateXAvailable = true;
				translateYAvailable = true;
				scaleXAvailable = false;
				scaleYAvailable = false;
			}
			
			if (activeMode == 1)
			{
				// deuxieme cadran a afficher ici
				
				translateXAvailable = false;
				translateYAvailable = false;
				scaleXAvailable = true;
				scaleYAvailable = true;
			}
			
			#endregion*/
			
		}
		
		
	}
	
	void CheckRayHit()
	{
		
		
		if(Physics.Raycast(rayCamToTarget, out hitTarget) == true)
		{
			if(hitTarget.transform.gameObject.tag == "Cube")
			{
				isLocking = true;
			}
			else{
				isLocking = false;	
			}
		}
		
	}
	
	void CheckInputs()
	{
		
		// Si on vise un cube
		if(isLocking == true )
		{
			
			if( Input.GetAxis("TriggersL_1") >= 0.9)
			{
								
				showCadran = true;
				
				(GameObject.FindObjectOfType(System.Type.GetType ("TPCamera")) as TPCamera).playerCanRotate = false;
				(GameObject.FindObjectOfType(System.Type.GetType ("TPControllerV2")) as TPControllerV2).canMove = false;
				
				
				//Si le joystick droit est entre 0 et -x et 0 et -y 
				if( translateXAvailable && ((Input.GetAxis("R_XAxis_1") <= -0.1) && (Input.GetAxis("R_YAxis_1") <= -0.1)) ){
				
					//Juste visuel
					translateXActivated = true;
					
					
					/*
					//Si le joueur sélectionn translate X
					if(Input.GetButton("RB_1"))
					{
						
						showCadran = false;
						isModifying = true;						
						//translateXActivated = true;
						
						targetToModify = hitTarget.transform.gameObject;
						targetStorePosition = targetToModify.transform.position;
						// Debug.Log(targetToModify.tag);
						
						(GameObject.FindObjectOfType(System.Type.GetType ("ReprogrammingStuff")) as ReprogrammingStuff).isTranslatingX = true;

					} */
				
				}
				else if( ((Input.GetAxis ("R_XAxis_1") >= 0.1 && Input.GetAxis ("R_XAxis_1") >= -0.1 )) && translateYAvailable)
				{
					translateYActivated = true;
					
					/*
					//Si le joueur sélectionne translate Y
					if(Input.GetButton("RB_1"))
					{
						
						showCadran = false;
						isModifying = true;						
						
						targetToModify = hitTarget.transform.gameObject;
						targetStorePosition = targetToModify.transform.position;
						
						(GameObject.FindObjectOfType(System.Type.GetType ("ReprogrammingStuff")) as ReprogrammingStuff).isTranslatingY = true;

					} */
					
					
				}
				
				else if( scaleXAvailable && ((Input.GetAxis("R_XAxis_1") <= -0.1) && (Input.GetAxis("R_YAxis_1") <= -0.1)) )
				{
				
					//Juste visuel
					scaleXActivated = true;
					
				
				} 
				
				else if( ((Input.GetAxis ("R_XAxis_1") >= 0.1 && Input.GetAxis ("R_XAxis_1") >= -0.1 )) && scaleYAvailable)
				{
					scaleYActivated = true;
					
					
				}
				
				// Pour l'instant juste un ELSE, mais après ajout d'autres fonctions, les tests devront etre complets
				else
				{
					
					translateXActivated = false;
					translateYActivated = false;
					scaleXActivated = false;
					scaleYActivated = false;
					
				}
					
 				
			}
			
			// ajoute par Elias : si le bouton est lache sur translateX ou translateY, on lance la modification. A terme on va avoir un "else if"
			// pour chaque action possible, pas tres propre :s
			
			else if(translateXActivated)
			{
				
				showCadran = false;
				isModifying = true;						
				translateXActivated = false;
						
				targetToModify = hitTarget.transform.gameObject;
				targetStorePosition = targetToModify.transform.position;
				// Debug.Log(targetToModify.tag);
						
				(GameObject.FindObjectOfType(System.Type.GetType ("ReprogrammingStuff")) as ReprogrammingStuff).isTranslatingX = true;
			}
			
			
			else if(translateYActivated)
			{
				showCadran = false;
				isModifying = true;						
				translateYActivated = false;
						
				targetToModify = hitTarget.transform.gameObject;
				targetStorePosition = targetToModify.transform.position;
				// Debug.Log(targetToModify.tag);
						
				(GameObject.FindObjectOfType(System.Type.GetType ("ReprogrammingStuff")) as ReprogrammingStuff).isTranslatingY = true;
			}
			
			else if(scaleXActivated)
			{
				
				showCadran = false;
				isModifying = true;						
				scaleXActivated = false;
						
				targetToModify = hitTarget.transform.gameObject;
				targetStorePosition = targetToModify.transform.position;
				// Debug.Log(targetToModify.tag);
						
				(GameObject.FindObjectOfType(System.Type.GetType ("ReprogrammingStuff")) as ReprogrammingStuff).isScalingX = true;
			}
			
			else if(scaleYActivated)
			{
				showCadran = false;
				isModifying = true;						
				scaleYActivated = false;
						
				targetToModify = hitTarget.transform.gameObject;
				targetStorePosition = targetToModify.transform.position;
				// Debug.Log(targetToModify.tag);
						
				(GameObject.FindObjectOfType(System.Type.GetType ("ReprogrammingStuff")) as ReprogrammingStuff).isScalingY = true;
			}
			
			
			else if( isModifying == true)
			{
				showCadran = false;
				
				(GameObject.FindObjectOfType(System.Type.GetType ("TPCamera")) as TPCamera).playerCanRotate = false;
				(GameObject.FindObjectOfType(System.Type.GetType ("TPControllerV2")) as TPControllerV2).canMove = false;
			}
			
			
			else
			{
				
				showCadran = false;
				
				(GameObject.FindObjectOfType(System.Type.GetType ("TPCamera")) as TPCamera).playerCanRotate = true;
				(GameObject.FindObjectOfType(System.Type.GetType ("TPControllerV2")) as TPControllerV2).canMove = true;
			}
			
		}
		
		else if(isModifying == true)
		{
			
			(GameObject.FindObjectOfType(System.Type.GetType ("TPCamera")) as TPCamera).playerCanRotate = false;
			(GameObject.FindObjectOfType(System.Type.GetType ("TPControllerV2")) as TPControllerV2).FreezeMovement();
			
		}
		
		
		// Changement de cadran
		if(showCadran && Input.GetAxis ("DPad_YAxis_1") >= 0.5)
		{
			Debug.Log("chgt cadran");
			
			activeMode +=1;
			
			if (activeMode == nbModes)
			{
				activeMode = 0;
			}
		}
		else if(showCadran && Input.GetAxis ("DPad_YAxis_1") <= -0.5)
		{
			Debug.Log("chgt cadran");
			
			activeMode -=1;
			
			if (activeMode < 0)
			{
				activeMode = nbModes;
			}
		}
		
	
	}
	
	
	void VarInitialize()
	{
		
		xRayTarget = Screen.width/2;
		yRayTarget = Screen.height/2;
		
		isLocking = false;
		showCadran = false;
		translateXAvailable = true;
		translateXActivated = false;
		translateYAvailable = true;
			
	}
	
}

