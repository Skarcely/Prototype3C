using UnityEngine;
using System.Collections;

public class ReprogrammingStuff : MonoBehaviour {
	
	public float translateStep;
	public float translateSpeed;
	
	public float scaleStep;
	public float scaleSpeed;
	
	[HideInInspector]
	public bool isTranslatingX;
	[HideInInspector]
	public bool isTranslatingY;
<<<<<<< HEAD
=======
	
	//scale
	[HideInInspector]
	public bool isScalingX;
	[HideInInspector]
	public bool isScalingY;
>>>>>>> 9b85f7694ff6d0e1f0b41d26aa1599e96485162e
	
	private Vector3 translateVector;
	
	private Vector3 scaleVector;
	
	private bool translateXMax = false;
	private bool translateXMin = false;
	private bool translateYMax = false;
	private bool translateYMin = false;
	
	private bool scaleXMax = false;
	private bool scaleXMin = false;
	private bool scaleYMax = false;
	private bool scaleYMin = false;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		CheckValidation();
		LimitManager();
	
		if(isTranslatingX)
		{
			Translate ((GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify, 0);
			
		}
		if(isTranslatingY)
		{
			Translate ((GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify, 1);
			
		}
<<<<<<< HEAD
=======
		
		if(isScalingX)
		{
			Scale ((GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify, 0);
			
		}
		if(isScalingY)
		{
			Scale ((GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify, 1);
			
		}
>>>>>>> 9b85f7694ff6d0e1f0b41d26aa1599e96485162e
		
	}
	
	public void Translate(GameObject target, int TranslateDirection )
	{
		
		if( TranslateDirection == 0) // Si Translate X
		{	
			isTranslatingY = false;
<<<<<<< HEAD
			if(Input.GetAxis("L_XAxis_1") >= 0.1)
=======
			isScalingX = false;
			isScalingY = false;
			
			
			if(Input.GetAxis("L_XAxis_1") >= 0.1 && !translateXMax)
>>>>>>> 9b85f7694ff6d0e1f0b41d26aa1599e96485162e
			{
				translateVector = new Vector3(translateStep, 0,0);
				target.transform.Translate(translateVector*Time.deltaTime*translateSpeed);
			}
			else if(Input.GetAxis ("L_XAxis_1")<= -0.1 && !translateXMin)
			{
			
				translateVector = new Vector3(-translateStep, 0,0);
				target.transform.Translate(translateVector*Time.deltaTime*translateSpeed);
				
			}
			
		}
		
<<<<<<< HEAD
		if (TranslateDirection == 1)
		{
			isTranslatingX = false;
			if(Input.GetAxis("L_YAxis_1") >= 0.1)
=======
		if (TranslateDirection == 1) //Si Translate Y
		{
			isTranslatingX = false;
			isScalingX = false;
			isScalingY = false;
			
			if(Input.GetAxis("L_YAxis_1") >= 0.1 && !translateYMax)
>>>>>>> 9b85f7694ff6d0e1f0b41d26aa1599e96485162e
			{
				translateVector = new Vector3(0, translateStep,0);
				target.transform.Translate(translateVector*Time.deltaTime*translateSpeed);
			}
<<<<<<< HEAD
			else if(Input.GetAxis ("L_XAxis_1")<= -0.1)
=======
			else if(Input.GetAxis ("L_YAxis_1") <= -0.1 && !translateYMin)
>>>>>>> 9b85f7694ff6d0e1f0b41d26aa1599e96485162e
			{
			
				translateVector = new Vector3(0, -translateStep,0);
				target.transform.Translate(translateVector*Time.deltaTime*translateSpeed);
				
			}
			
		}
		
	}
	
<<<<<<< HEAD
	private void CheckValidation()
	{
		if(isTranslatingX || isTranslatingY){
=======
	// Scale
	
	public void Scale(GameObject target, int ScaleDirection )
	{		
		
		if(ScaleDirection == 0) // Si Scale X
		{	
			isTranslatingY = false;
			isTranslatingX = false;
			isScalingY = false;
			
			if(Input.GetAxis("L_XAxis_1") >= 0.1 && !scaleXMax)
			{
				scaleVector = new Vector3(scaleStep, 0,0);
				target.transform.localScale += (scaleVector*Time.deltaTime*scaleSpeed);
			}
			else if(Input.GetAxis ("L_XAxis_1")<= -0.1 && !scaleXMin)
			{
			
				scaleVector = new Vector3(-scaleStep, 0,0);
				target.transform.localScale += (scaleVector*Time.deltaTime*translateSpeed);
				
			}
			
		}
		
		if( ScaleDirection == 1) // Si Scale Y
		{	
			isTranslatingY = false;
			isTranslatingX = false;
			isScalingX= false;
			
			if(Input.GetAxis("L_YAxis_1") >= 0.1 && !scaleYMax)
			{
				scaleVector = new Vector3(0, scaleStep,0);
				target.transform.localScale += (scaleVector*Time.deltaTime*scaleSpeed);
			}
			else if(Input.GetAxis ("L_YAxis_1")<= -0.1 && !scaleYMin)
			{
			
				scaleVector = new Vector3(0, -scaleStep,0);
				target.transform.localScale += (scaleVector*Time.deltaTime*scaleSpeed);
				
			}
			
		}
		
	}
	
	private void CheckValidation()
	{
		if(isTranslatingX || isTranslatingY || isScalingX || isScalingY)
		{
>>>>>>> 9b85f7694ff6d0e1f0b41d26aa1599e96485162e
			if(Input.GetButton ("A_1"))
			{
				isTranslatingX = false;
				isTranslatingY = false;
<<<<<<< HEAD
=======
				isScalingX = false;
				isScalingY = false;
>>>>>>> 9b85f7694ff6d0e1f0b41d26aa1599e96485162e
				(GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).isModifying = false;
				(GameObject.FindObjectOfType(System.Type.GetType ("TPCameraV2")) as TPCameraV2).playerCanRotate = false;
				(GameObject.FindObjectOfType(System.Type.GetType ("TPControllerV2")) as TPControllerV2).FreeMovement();
			}
			
			if(Input.GetButton ("B_1"))
			{
				ResetTarget();
			}
			
<<<<<<< HEAD
			if(Input.GetButton ("B_1"))
			{
				ResetTarget();
			}
			
=======
>>>>>>> 9b85f7694ff6d0e1f0b41d26aa1599e96485162e
		}
		
	}
	
	private void ResetTarget()
	{
		
		(GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify.transform.position = (GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetStorePosition;
<<<<<<< HEAD
		isTranslatingX = false;
		(GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).isModifying = false;
		(GameObject.FindObjectOfType(System.Type.GetType ("TPController")) as TPController).FreeMovement();
=======
		(GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify.transform.localScale = (GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetStoreScale;
		isTranslatingX = false;
		isTranslatingY = false;
		isScalingX = false;
		isScalingY = false;
		(GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).isModifying = false;
		(GameObject.FindObjectOfType(System.Type.GetType ("TPControllerV2")) as TPControllerV2).FreeMovement();
	}
	
	
	private void LimitManager()
	{
		// Verifier les limites du translate
		
		if((GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify)
		{
				//verifier max X
			if ( (GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify.transform.position.x >= (((GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify.GetComponent("ModifLimit") as ModifLimit).initialPos.x + ((GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify.GetComponent("ModifLimit") as ModifLimit).maxTranslateX))
			{
				translateXMax = true;
			}
			else
			{
				translateXMax = false;
			}
			
			
			//verifier min X
			if ( (GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify.transform.position.x <= (((GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify.GetComponent("ModifLimit") as ModifLimit).initialPos.x - ((GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify.GetComponent("ModifLimit") as ModifLimit).maxTranslateX))
			{
				translateXMin = true;
			}
			else
			{
				translateXMin = false;
			}
			
			
			//verifier max Y
			if ( (GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify.transform.position.y >= (((GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify.GetComponent("ModifLimit") as ModifLimit).initialPos.y + ((GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify.GetComponent("ModifLimit") as ModifLimit).maxTranslateY))
			{
				translateYMax = true;
			}
			else
			{
				translateYMax = false;
			}
			
			
			//verifier min Y
			if ( (GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify.transform.position.y <= (((GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify.GetComponent("ModifLimit") as ModifLimit).initialPos.y - ((GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify.GetComponent("ModifLimit") as ModifLimit).maxTranslateY))
			{
				translateYMin = true;
			}
			else
			{
				translateYMin = false;
			}
			
			//________________
			
			//Verifier les limites du scale
			
			//verifier max X
			if ( (GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify.transform.localScale.x >= (((GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify.GetComponent("ModifLimit") as ModifLimit).initialScale.x + ((GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify.GetComponent("ModifLimit") as ModifLimit).maxScaleX))
			{
				scaleXMax = true;
			}
			else
			{
				scaleXMax = false;
			}
			
			
			//verifier min X
			if ( (GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify.transform.localScale.x <= /* (((GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify.GetComponent("ModifLimit") as ModifLimit).initialScale.x - */ ((GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify.GetComponent("ModifLimit") as ModifLimit).minScaleX) //)
			{
				scaleXMin = true;
			}
			else
			{
				scaleXMin = false;
			}
			
			//verifier max Y
			if ( (GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify.transform.localScale.y >= (((GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify.GetComponent("ModifLimit") as ModifLimit).initialScale.y + ((GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify.GetComponent("ModifLimit") as ModifLimit).maxScaleY))
			{
				scaleYMax = true;
			}
			else
			{
				scaleYMax = false;
			}
			
			//verifier min Y
			if ( (GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify.transform.localScale.y <= /* (((GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify.GetComponent("ModifLimit") as ModifLimit).initialScale.x - */ ((GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify.GetComponent("ModifLimit") as ModifLimit).minScaleY) //)
			{
				scaleYMin = true;
			}
			else
			{
				scaleYMin = false;
			}
		}
	
>>>>>>> 9b85f7694ff6d0e1f0b41d26aa1599e96485162e
	}
	
}
