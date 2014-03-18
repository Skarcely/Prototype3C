using UnityEngine;
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
	
	//Private
	private int xRayTarget;
	private int yRayTarget;
	
	private Ray rayCamToTarget;
	
	[HideInInspector]
	public bool isLocking;
	
	private bool showCadran;
	
	[HideInInspector]
	public bool isModifying;
	
	#region TranslateChecker
	//TranslateX
	private bool translateXAvailable;
	private bool translateXActivated;
	
	//TranslateY
	private bool translateYAvailable;
	private bool translateYActivated;
	
	#endregion
	
	private RaycastHit hitTarget;
	
	[HideInInspector]
	public GameObject targetToModify;
	
	// Use this for initialization
	void Start () 
	{
	
		VarInitialize();
		
	}
	
	// Update is called once per frame
	void Update () 
	{
	
		rayCamToTarget = camera.ScreenPointToRay(new Vector3(xRayTarget,yRayTarget));
		
		//Debug
		//Debug.DrawRay(rayCam.origin, rayCam.direction, Color.magenta);
		
		CheckRayHit();
		
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
			
			/*#region TranslateY
			
			if(translateYAvailable == true)
			{
				
				GUI.DrawTexture(new Rect(Screen.width/2 + (cadran.width/2), Screen.height/2 - (cadran.height/2), 128,128), cadran_TranslateY_Normal);
				
			}
			if(translateYActivated == true)
			{
			
				GUI.DrawTexture(new Rect(Screen.width/2 - (cadran.width/2), Screen.height/2 - (cadran.height/2), 128,128), cadran_TranslateY_Selected);
				
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
				//Debug.Log("Cube hitted");	
			}
			else{
				isLocking = false;	
			}
		}
		
	}
	
	void CheckInputs()
	{
		
		// Si on vise un cube et qu'on n'est pas déjà en train de modifier un objet
		if(isLocking == true )
		{
			
			if( Input.GetAxis("TriggersL_1") >= 0.9)
			{

				showCadran = true;
				
				(GameObject.FindObjectOfType(System.Type.GetType ("ThirdPersonShooterGameCamera")) as ThirdPersonShooterGameCamera).playerCanRotate = false;
				(GameObject.FindObjectOfType(System.Type.GetType ("TPController")) as TPController).canMove = false;
				
				
				//Si le joystick droit est entre 0 et -x et 0 et -y 
				if(((Input.GetAxis("R_XAxis_1") <= -0.1 && Input.GetAxis("R_YAxis_1") >= -0.9) || (Input.GetAxis("R_XAxis_1") >= -0.9 && Input.GetAxis("R_YAxis_1") <= -0.1)) && translateXAvailable){
				
					translateXActivated = true;
					
					//Si le joueur sélectionn translate X
					if(Input.GetButton("RB_1"))
					{
						
						showCadran = false;
						isModifying = true;						
						//translateXActivated = true;
						
						targetToModify = hitTarget.transform.gameObject;
						// Debug.Log(targetToModify.tag);
						
						(GameObject.FindObjectOfType(System.Type.GetType ("ReprogrammingStuff")) as ReprogrammingStuff).isTranslatingX = true;

					}
				
				}
			/*	else if ( ((Input.GetAxis ("R_XAxis_1") >= 0.1 && Input.GetAxis ("R_XAxis_1") >= 0.1 )) && translateYAvailable)
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
					
					
				}*/
				
				// Pour l'instant juste un ELSE, mais après ajout d'autres fonctions, les tests devront etre complets
				else
				{
					
					translateXActivated = false;
					
				}
					
 				
			}
			else
			{
				
				showCadran = false;
				
				(GameObject.FindObjectOfType(System.Type.GetType ("ThirdPersonShooterGameCamera")) as ThirdPersonShooterGameCamera).playerCanRotate = true;
				
			}
			
		}
		
		else if(isModifying == true)
		{
			
			(GameObject.FindObjectOfType(System.Type.GetType ("ThirdPersonShooterGameCamera")) as ThirdPersonShooterGameCamera).playerCanRotate = false;
			(GameObject.FindObjectOfType(System.Type.GetType ("TPController")) as TPController).FreezeMovement();
			
		}
		
	
	}
	
	
	void VarInitialize()
	{
		
		xRayTarget = Screen.width/2;
		yRayTarget = Screen.height/2;
		
		isLocking = false;
		showCadran = false;
		translateXAvailable = true;
		translateYAvailable = true;
			
	}
	
}
