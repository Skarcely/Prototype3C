using UnityEngine;
using System.Collections;

public class CrosshairLock : MonoBehaviour {
	
	
	//Public
	public Texture2D crosshairNormal;
	public Texture2D crosshairLock;
	public Texture2D cadran;
	public Texture2D cadran2;
	public Texture2D cadran_TranslateX_Normal;
	public Texture2D cadran_TranslateX_Selected;
	public Texture2D cadran_TranslateY_Normal;
	public Texture2D cadran_TranslateY_Selected;
<<<<<<< HEAD
=======
	public Texture2D cadran_ScaleX_Normal;
	public Texture2D cadran_ScaleX_Selected;
	public Texture2D cadran_ScaleY_Normal;
	public Texture2D cadran_ScaleY_Selected;
	public Texture2D Lang1;
	public Texture2D Lang2;
	
>>>>>>> 9b85f7694ff6d0e1f0b41d26aa1599e96485162e
	
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
	
<<<<<<< HEAD
	#endregion
=======
	//ScaleX
	private bool scaleXAvailable;
	private bool scaleXActivated;
	
	//ScaleY
	private bool scaleYAvailable;
	private bool scaleYActivated;
	
	
	private int activeMode = 0; // Langage actif
	private int nbModes = 2; // Nombre de langages
	private int cooldownCadran; // pour pas que l'appui sur le D-pad trigger le changement plusieurs fois
	
>>>>>>> 9b85f7694ff6d0e1f0b41d26aa1599e96485162e
	
	private RaycastHit hitTarget;
	
	[HideInInspector]
	public GameObject targetToModify;
	
	[HideInInspector]
	public Vector3 targetStorePosition;
<<<<<<< HEAD
	
=======
	[HideInInspector]
	public Vector3 targetStoreScale;
		
>>>>>>> 9b85f7694ff6d0e1f0b41d26aa1599e96485162e
	// Use this for initialization
	void Start () 
	{
	
		VarInitialize();
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		xRayTarget = Screen.width/2;
		yRayTarget = Screen.height/2;
		
<<<<<<< HEAD
		//Debug
		//Debug.DrawRay(rayCam.origin, rayCam.direction, Color.magenta);
		Debug.Log("X : " + (Input.GetAxis("R_XAxis_1") + " ; Y = " + (Input.GetAxis("R_YAxis_1"))));
=======
		//Raycast
		rayCamToTarget = camera.ScreenPointToRay(new Vector3(xRayTarget,yRayTarget));
>>>>>>> 9b85f7694ff6d0e1f0b41d26aa1599e96485162e
		
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
<<<<<<< HEAD
			GUI.DrawTexture(new Rect(Screen.width/2 - (crosshairNormal.width/2), Screen.height/2 - (crosshairNormal.height/2), 32,32), crosshairNormal);
=======
			GUI.DrawTexture(new Rect( Screen.width/2 - 16, Screen.height/2 - 16 , 32,32), crosshairNormal);
>>>>>>> 9b85f7694ff6d0e1f0b41d26aa1599e96485162e
		}
		
		//Si passe sur un cube
		if(isLocking == true && showCadran == false)
		{
<<<<<<< HEAD
			GUI.DrawTexture(new Rect(Screen.width/2 - (crosshairLock.width/2), Screen.height/2 - (crosshairLock.height/2), 32,32), crosshairLock);	
=======
			GUI.DrawTexture(new Rect(Screen.width/2 - 16, Screen.height/2 - 16, 32,32), crosshairLock);	
>>>>>>> 9b85f7694ff6d0e1f0b41d26aa1599e96485162e
		}
		
		//Si Appuie sur LT
		if(showCadran == true)
		{
<<<<<<< HEAD
			GUI.DrawTexture(new Rect(Screen.width/2 - (cadran.width/2), Screen.height/2 - (cadran.height/2), 256,256), cadran);
			
=======
			
			if (activeMode == 0)
			{
				// premier cadran a afficher ici
				GUI.DrawTexture(new Rect(Screen.width/2 - (cadran.width/2), Screen.height/2 - (cadran.height/2), 256,256), cadran);
				
			
				GUI.DrawTexture(new Rect(Screen.width/2 - 256, Screen.height/2 - 32, 128,64), Lang1);
				
				translateXAvailable = true;
				translateYAvailable = true;
				scaleXAvailable = false;
				scaleYAvailable = false;
			}
			
			if (activeMode == 1)
			{
				// deuxieme cadran a afficher ici
				
				GUI.DrawTexture(new Rect(Screen.width/2 - (cadran2.width/2), Screen.height/2 - (cadran2.height/2), 256,256), cadran2);
				
				GUI.DrawTexture(new Rect(Screen.width/2 - 256, Screen.height/2 - 32, 128,64), Lang2);
				GUI.contentColor = Color.green;
				
				translateXAvailable = false;
				translateYAvailable = false;
				scaleXAvailable = true;
				scaleYAvailable = true;
			}
			
			
			
			
>>>>>>> 9b85f7694ff6d0e1f0b41d26aa1599e96485162e
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
<<<<<<< HEAD
				
			}
=======
				
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
			
>>>>>>> 9b85f7694ff6d0e1f0b41d26aa1599e96485162e
			
			
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
<<<<<<< HEAD
		{
=======
		{			
>>>>>>> 9b85f7694ff6d0e1f0b41d26aa1599e96485162e
			
			if(Input.GetAxis("TriggersL_1") >= 0.9)
			{
								
				showCadran = true;
				
<<<<<<< HEAD
				(GameObject.FindObjectOfType(System.Type.GetType ("ThirdPersonShooterGameCamera")) as ThirdPersonShooterGameCamera).playerCanRotate = false;
				(GameObject.FindObjectOfType(System.Type.GetType ("TPController")) as TPController).canMove = false;
				
				
				//Si le joystick droit est entre 0 et -x et 0 et -y 
				if( translateXAvailable && ((Input.GetAxis("R_XAxis_1") <= -0.1) && (Input.GetAxis("R_YAxis_1") <= -0.1)) ){
				
					Debug.Log ("Passe ici !");
=======
				(GameObject.FindObjectOfType(System.Type.GetType ("TPCameraV2")) as TPCameraV2).playerCanRotate = false;
				(GameObject.FindObjectOfType(System.Type.GetType ("TPControllerV2")) as TPControllerV2).canMove = false;
				
				
				//Si le joystick droit est entre 0 et -x et 0 et -y 
				if( translateXAvailable && ((Input.GetAxis("R_XAxis_1") <= -0.1) && (Input.GetAxis("R_YAxis_1") <= -0.1)) )
				{
				
					//Juste visuel (plus maintenant)
>>>>>>> 9b85f7694ff6d0e1f0b41d26aa1599e96485162e
					translateXActivated = true;
					scaleXActivated = false;
					scaleYActivated = false;
					translateYActivated = false;
					
				
				}
				else if( ((Input.GetAxis ("R_XAxis_1") >= 0.1 && Input.GetAxis ("R_XAxis_1") >= -0.1 )) && translateYAvailable)
				{
					translateYActivated = true;
					scaleXActivated = false;
					scaleYActivated = false;
					translateXActivated = false;
					
					
				}
				
				else if( scaleXAvailable && ((Input.GetAxis("R_XAxis_1") <= -0.1) && (Input.GetAxis("R_YAxis_1") <= -0.1)) )
				{
				
					scaleXActivated = true;
					scaleYActivated = false;
					translateXActivated = false;
					translateYActivated = false;
					
<<<<<<< HEAD
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

					}
=======
>>>>>>> 9b85f7694ff6d0e1f0b41d26aa1599e96485162e
				
				} 
				
				else if( ((Input.GetAxis ("R_XAxis_1") >= 0.1 && Input.GetAxis ("R_XAxis_1") >= -0.1 )) && scaleYAvailable)
				{
					scaleYActivated = true;
					scaleXActivated = false;
					translateXActivated = false;
					translateYActivated = false;
					
				}
<<<<<<< HEAD
				else if( ((Input.GetAxis ("R_XAxis_1") >= 0.1 && Input.GetAxis ("R_XAxis_1") >= -0.1 )) && translateYAvailable)
				{
					translateYActivated = true;
					
					//Si le joueur sélectionn translate Y
					if(Input.GetButton("RB_1"))
					{
						
						showCadran = false;
						isModifying = true;						
						//translateXActivated = true;
						
						targetToModify = hitTarget.transform.gameObject;
						// Debug.Log(targetToModify.tag);
						
						(GameObject.FindObjectOfType(System.Type.GetType ("ReprogrammingStuff")) as ReprogrammingStuff).isTranslatingY = true;

					}
					
					
				}
=======
>>>>>>> 9b85f7694ff6d0e1f0b41d26aa1599e96485162e
				
				// Pour l'instant juste un ELSE, mais après ajout d'autres fonctions, les tests devront etre complets
				else
				{
					
					translateXActivated = false;
					translateYActivated = false;
<<<<<<< HEAD
=======
					scaleXActivated = false;
					scaleYActivated = false;
>>>>>>> 9b85f7694ff6d0e1f0b41d26aa1599e96485162e
					
				}
					
 				
			}
<<<<<<< HEAD
			else if( isModifying == true)
			{
				showCadran = false;
				
				(GameObject.FindObjectOfType(System.Type.GetType ("ThirdPersonShooterGameCamera")) as ThirdPersonShooterGameCamera).playerCanRotate = false;
				(GameObject.FindObjectOfType(System.Type.GetType ("TPController")) as TPController).canMove = false;
			}
			else
=======
			
			// Si le bouton est lache sur translateX ou translateY, on lance la modification. A terme on va avoir un "else if"
			// pour chaque action possible, pas tres propre :s
			
			else if(translateXActivated)
			{
				
				showCadran = false;
				isModifying = true;						
				translateXActivated = false;
						
				targetToModify = hitTarget.transform.gameObject;
				targetStorePosition = targetToModify.transform.position;
				targetStoreScale = targetToModify.transform.localScale;
				// Debug.Log(targetToModify.tag);
						
				(GameObject.FindObjectOfType(System.Type.GetType ("ReprogrammingStuff")) as ReprogrammingStuff).isTranslatingX = true;
				
				(GameObject.FindObjectOfType(System.Type.GetType ("ReprogrammingStuff")) as ReprogrammingStuff).isTranslatingY = false;
				(GameObject.FindObjectOfType(System.Type.GetType ("ReprogrammingStuff")) as ReprogrammingStuff).isScalingX = false;
				(GameObject.FindObjectOfType(System.Type.GetType ("ReprogrammingStuff")) as ReprogrammingStuff).isScalingY = false;
			}
			
			
			else if(translateYActivated)
			{
				showCadran = false;
				isModifying = true;						
				translateYActivated = false;
						
				targetToModify = hitTarget.transform.gameObject;
				targetStorePosition = targetToModify.transform.position;
				targetStoreScale = targetToModify.transform.localScale;
				// Debug.Log(targetToModify.tag);
						
				(GameObject.FindObjectOfType(System.Type.GetType ("ReprogrammingStuff")) as ReprogrammingStuff).isTranslatingY = true;
				
				(GameObject.FindObjectOfType(System.Type.GetType ("ReprogrammingStuff")) as ReprogrammingStuff).isTranslatingX = false;
				(GameObject.FindObjectOfType(System.Type.GetType ("ReprogrammingStuff")) as ReprogrammingStuff).isScalingX = false;
				(GameObject.FindObjectOfType(System.Type.GetType ("ReprogrammingStuff")) as ReprogrammingStuff).isScalingY = false;
			}
			
			else if(scaleXActivated)
>>>>>>> 9b85f7694ff6d0e1f0b41d26aa1599e96485162e
			{
				
				showCadran = false;
				isModifying = true;						
				scaleXActivated = false;
						
				targetToModify = hitTarget.transform.gameObject;
				targetStorePosition = targetToModify.transform.position;
				targetStoreScale = targetToModify.transform.localScale;
				// Debug.Log(targetToModify.tag);
						
				(GameObject.FindObjectOfType(System.Type.GetType ("ReprogrammingStuff")) as ReprogrammingStuff).isScalingX = true;
				
				(GameObject.FindObjectOfType(System.Type.GetType ("ReprogrammingStuff")) as ReprogrammingStuff).isTranslatingY = false;
				(GameObject.FindObjectOfType(System.Type.GetType ("ReprogrammingStuff")) as ReprogrammingStuff).isTranslatingX = false;
				(GameObject.FindObjectOfType(System.Type.GetType ("ReprogrammingStuff")) as ReprogrammingStuff).isScalingY = false;
			}
			
			else if(scaleYActivated)
			{
				showCadran = false;
				isModifying = true;						
				scaleYActivated = false;
						
				targetToModify = hitTarget.transform.gameObject;
				targetStorePosition = targetToModify.transform.position;
				targetStoreScale = targetToModify.transform.localScale;
						
				(GameObject.FindObjectOfType(System.Type.GetType ("ReprogrammingStuff")) as ReprogrammingStuff).isScalingY = true;
				
				(GameObject.FindObjectOfType(System.Type.GetType ("ReprogrammingStuff")) as ReprogrammingStuff).isTranslatingY = false;
				(GameObject.FindObjectOfType(System.Type.GetType ("ReprogrammingStuff")) as ReprogrammingStuff).isScalingX = false;
				(GameObject.FindObjectOfType(System.Type.GetType ("ReprogrammingStuff")) as ReprogrammingStuff).isTranslatingX = false;
			}
			
			
			else if(isModifying == true)
			{
				showCadran = false;
				
				(GameObject.FindObjectOfType(System.Type.GetType ("TPCameraV2")) as TPCameraV2).playerCanRotate = false;
				(GameObject.FindObjectOfType(System.Type.GetType ("TPControllerV2")) as TPControllerV2).canMove = false;
			}
			
			
			else
			{
				
<<<<<<< HEAD
				(GameObject.FindObjectOfType(System.Type.GetType ("ThirdPersonShooterGameCamera")) as ThirdPersonShooterGameCamera).playerCanRotate = true;
				(GameObject.FindObjectOfType(System.Type.GetType ("TPController")) as TPController).canMove = true;
=======
				showCadran = false;
				
				(GameObject.FindObjectOfType(System.Type.GetType ("TPCameraV2")) as TPCameraV2).playerCanRotate = true;
				(GameObject.FindObjectOfType(System.Type.GetType ("TPControllerV2")) as TPControllerV2).canMove = true;
>>>>>>> 9b85f7694ff6d0e1f0b41d26aa1599e96485162e
			}
			
		}
		
		else if(isModifying == true)
		{
			
			(GameObject.FindObjectOfType(System.Type.GetType ("TPCameraV2")) as TPCameraV2).playerCanRotate = false;
			(GameObject.FindObjectOfType(System.Type.GetType ("TPControllerV2")) as TPControllerV2).FreezeMovement();
			
		}
		else
		{
			
			(GameObject.FindObjectOfType(System.Type.GetType ("TPCameraV2")) as TPCameraV2).playerCanRotate = true;
			(GameObject.FindObjectOfType(System.Type.GetType ("TPControllerV2")) as TPControllerV2).FreeMovement();
		}
		
		
		
		
		
		
		// Changement de cadran
		if (cooldownCadran >= 15)
		{
				
				
			if(showCadran && Input.GetAxis ("DPad_YAxis_1") >= 0.5)
			{
//				Debug.Log("chgt cadran");
				
				activeMode +=1;
				
				if (activeMode == nbModes)
				{
					activeMode = 0;
				}
				cooldownCadran = 0;
			}
			else if(showCadran && Input.GetAxis ("DPad_YAxis_1") <= -0.5)
			{
//				Debug.Log("chgt cadran");
				
				activeMode -=1;
				
				if (activeMode < 0)
				{
					activeMode = nbModes-1;
				}
				cooldownCadran = 0;
			}
		}
		else
		{
			cooldownCadran +=1;
		}
		
	
	}
	
	
	
	
	
	void VarInitialize()
	{
	
		isLocking = false;
		showCadran = false;
		translateXAvailable = true;
		translateXActivated = false;
		translateYAvailable = true;
<<<<<<< HEAD
=======
		translateYActivated = false;
		
		scaleXAvailable = false;
		scaleXActivated = false;
		scaleYAvailable = false;
		scaleYActivated = false;
		
		cooldownCadran = 15;
>>>>>>> 9b85f7694ff6d0e1f0b41d26aa1599e96485162e
			
	}
	
}

