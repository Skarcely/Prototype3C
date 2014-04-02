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
	
	//scale
	[HideInInspector]
	public bool isScalingX;
	[HideInInspector]
	public bool isScalingY;
	
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
	
	private float lightRange;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		
		LimitManager();

		if(isTranslatingX)
		{
			Translate ((GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify, 0);
			
		}
		if(isTranslatingY)
		{
			Translate ((GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify, 1);
			
		}
		
		if(isScalingX)
		{
			Scale ((GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify, 0);
			
		}
		if(isScalingY)
		{
			Scale ((GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify, 1);
			
		}
		
		CheckValidation();
		
	}
	
	
	public void Translate(GameObject target, int TranslateDirection )
	{		
		if(!translateXMax && !translateXMin && !translateYMax && !translateYMin)
		{
			target.transform.GetChild(0).light.color = Color.blue;
		}
		
		if( TranslateDirection == 0) // Si Translate X
		{	
			isTranslatingY = false;
			isScalingX = false;
			isScalingY = false;
			
			
			if(Input.GetAxis("L_XAxis_1") >= 0.1 && !translateXMax)
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
		
		if (TranslateDirection == 1) //Si Translate Y
		{
			isTranslatingX = false;
			isScalingX = false;
			isScalingY = false;
			
			if(Input.GetAxis("L_YAxis_1") >= 0.1 && !translateYMax)
			{
				translateVector = new Vector3(0, translateStep,0);
				target.transform.Translate(translateVector*Time.deltaTime*translateSpeed);
			}
			else if(Input.GetAxis ("L_YAxis_1") <= -0.1 && !translateYMin)
			{
			
				translateVector = new Vector3(0, -translateStep,0);
				target.transform.Translate(translateVector*Time.deltaTime*translateSpeed);
				
			}
			
		}
		
	}
	
	// Scale
	
	public void Scale(GameObject target, int ScaleDirection )
	{		
		if(!scaleXMax && !scaleXMin && !scaleYMin && !scaleYMax)
		{
			target.transform.GetChild(0).light.color = Color.blue;
		}
			
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
			if(Input.GetButton ("A_1"))
			{	
				
				translateXMax = false;
//				Debug.Log("After validation : " + translateXMax);
				
				isTranslatingX = false;
				isTranslatingY = false;
				isScalingX = false;
				isScalingY = false;
				
				(GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify.transform.GetChild(0).light.color = Color.white;
				
				(GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).isModifying = false;
				(GameObject.FindObjectOfType(System.Type.GetType ("TPCameraV2")) as TPCameraV2).playerCanRotate = false;
				(GameObject.FindObjectOfType(System.Type.GetType ("TPControllerV2")) as TPControllerV2).FreeMovement();
			}
			
			if(Input.GetButton ("B_1"))
			{
				ResetTarget();
			}
			
		}
		
	}
	
	private void ResetTarget()
	{
		(GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify.transform.position = (GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetStorePosition;
		(GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify.transform.localScale = (GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetStoreScale;
		
		translateXMax = false;
		
		isTranslatingX = false;
		isTranslatingY = false;
		isScalingX = false;
		isScalingY = false;

		(GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify.transform.GetChild(0).light.color = Color.white;
		
		(GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).isModifying = false;
		(GameObject.FindObjectOfType(System.Type.GetType ("TPControllerV2")) as TPControllerV2).FreeMovement();
	}
	
	
	private void LimitManager()
	{
		// Verifier les limites du translate
		
		if((GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify)
		{
			//verifier max X
			if ( isTranslatingX && (GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify.transform.position.x >= (((GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify.GetComponent("ModifLimit") as ModifLimit).initialPos.x + ((GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify.GetComponent("ModifLimit") as ModifLimit).maxTranslateX))
			{
				
				translateXMax = true;
				(GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify.transform.GetChild(0).light.color = Color.red;
			}
			else
			{
				
				translateXMax = false;
			}
			
			
			//verifier min X
			if ( isTranslatingX && (GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify.transform.position.x <= (((GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify.GetComponent("ModifLimit") as ModifLimit).initialPos.x - ((GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify.GetComponent("ModifLimit") as ModifLimit).maxTranslateX))
			{
				(GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify.transform.GetChild(0).light.color = Color.red;
				translateXMin = true;
			}
			else
			{
				translateXMin = false;
			}
			
			
			//verifier max Y
			if ( isTranslatingY && (GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify.transform.position.y >= (((GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify.GetComponent("ModifLimit") as ModifLimit).initialPos.y + ((GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify.GetComponent("ModifLimit") as ModifLimit).maxTranslateY))
			{
				(GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify.transform.GetChild(0).light.color = Color.red;
				translateYMax = true;
			}
			else
			{
				translateYMax = false;
			}
			
			
			//verifier min Y
			if ( isTranslatingY && (GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify.transform.position.y <= (((GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify.GetComponent("ModifLimit") as ModifLimit).initialPos.y - ((GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify.GetComponent("ModifLimit") as ModifLimit).maxTranslateY))
			{
				(GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify.transform.GetChild(0).light.color = Color.red;
				translateYMin = true;
			}
			else
			{
				translateYMin = false;
			}
			
			//________________
			
			//Verifier les limites du scale
			
			//verifier max X
			if ( isScalingX && (GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify.transform.localScale.x >= (((GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify.GetComponent("ModifLimit") as ModifLimit).initialScale.x + ((GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify.GetComponent("ModifLimit") as ModifLimit).maxScaleX))
			{
				(GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify.transform.GetChild(0).light.color = Color.red;
				scaleXMax = true;
			}
			else
			{
				scaleXMax = false;
			}
			
			
			//verifier min X
			if ( isScalingX && (GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify.transform.localScale.x <= /* (((GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify.GetComponent("ModifLimit") as ModifLimit).initialScale.x - */ ((GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify.GetComponent("ModifLimit") as ModifLimit).minScaleX) //)
			{
				(GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify.transform.GetChild(0).light.color = Color.red;
				scaleXMin = true;
			}
			else
			{
				scaleXMin = false;
			}
			
			//verifier max Y
			if ( isScalingY && (GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify.transform.localScale.y >= (((GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify.GetComponent("ModifLimit") as ModifLimit).initialScale.y + ((GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify.GetComponent("ModifLimit") as ModifLimit).maxScaleY))
			{
				(GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify.transform.GetChild(0).light.color = Color.red;
				scaleYMax = true;
			}
			else
			{
				scaleYMax = false;
			}
			
			//verifier min Y
			if ( isScalingY &&(GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify.transform.localScale.y <= /* (((GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify.GetComponent("ModifLimit") as ModifLimit).initialScale.x - */ ((GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify.GetComponent("ModifLimit") as ModifLimit).minScaleY) //)
			{
				(GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify.transform.GetChild(0).light.color = Color.red;
				scaleYMin = true;
			}
			else
			{
				scaleYMin = false;
			}
		}
	
	}
	
}
