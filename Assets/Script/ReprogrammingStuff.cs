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
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		CheckValidation();
	
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
		
	}
	
	public void Translate(GameObject target, int TranslateDirection ){
		
		
		
		if( TranslateDirection == 0) // Si Translate X
		{	
			isTranslatingY = false;
			isScalingX = false;
			isScalingY = false;
			
			
			if(Input.GetAxis("L_XAxis_1") >= 0.1)
			{
				translateVector = new Vector3(translateStep, 0,0);
				target.transform.Translate(translateVector*Time.deltaTime*translateSpeed);
			}
			else if(Input.GetAxis ("L_XAxis_1")<= -0.1)
			{
			
				translateVector = new Vector3(-translateStep, 0,0);
				target.transform.Translate(translateVector*Time.deltaTime*translateSpeed);
				
			}
			
		}
		
		if (TranslateDirection == 1)
		{
			isTranslatingX = false;
			isScalingX = false;
			isScalingY = false;
			if(Input.GetAxis("L_YAxis_1") >= 0.1)
			{
				translateVector = new Vector3(0, translateStep,0);
				target.transform.Translate(translateVector*Time.deltaTime*translateSpeed);
			}
			else if(Input.GetAxis ("L_YAxis_1") <= -0.1)
			{
			
				translateVector = new Vector3(0, -translateStep,0);
				target.transform.Translate(translateVector*Time.deltaTime*translateSpeed);
				
			}
			
		}
		
	}
	
	// Scale
	
	public void Scale(GameObject target, int ScaleDirection ){
		
		
		
		if( ScaleDirection == 0) // Si Scale X
		{	
			isTranslatingY = false;
			isTranslatingX = false;
			isScalingY = false;
			
			if(Input.GetAxis("L_XAxis_1") >= 0.1)
			{
				scaleVector = new Vector3(scaleStep, 0,0);
				target.transform.localScale += (scaleVector*Time.deltaTime*scaleSpeed);
			}
			else if(Input.GetAxis ("L_XAxis_1")<= -0.1)
			{
			
				translateVector = new Vector3(-scaleStep, 0,0);
				target.transform.localScale += (scaleVector*Time.deltaTime*translateSpeed);
				
			}
			
		}
		
		if( ScaleDirection == 0) // Si Scale Y
		{	
			isTranslatingY = false;
			isTranslatingX = false;
			isScalingX= false;
			
			if(Input.GetAxis("L_XAxis_1") >= 0.1)
			{
				scaleVector = new Vector3(0, scaleStep,0);
				target.transform.localScale += (scaleVector*Time.deltaTime*scaleSpeed);
			}
			else if(Input.GetAxis ("L_XAxis_1")<= -0.1)
			{
			
				translateVector = new Vector3(0, -scaleStep,0);
				target.transform.localScale += (scaleVector*Time.deltaTime*translateSpeed);
				
			}
			
		}
		
	}
	
	private void CheckValidation()
	{
		if(isTranslatingX || isTranslatingY || isScalingX || isScalingY){
			if(Input.GetButton ("A_1"))
			{
				isTranslatingX = false;
				isTranslatingY = false;
				isScalingX = false;
				isScalingY = false;
				(GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).isModifying = false;
				(GameObject.FindObjectOfType(System.Type.GetType ("TPCamera")) as TPCamera).playerCanRotate = false;
				(GameObject.FindObjectOfType(System.Type.GetType ("TPController")) as TPControllerV2).FreeMovement();
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
		isTranslatingX = false;
		isTranslatingY = false;
		(GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).isModifying = false;
		(GameObject.FindObjectOfType(System.Type.GetType ("TPController")) as TPController).FreeMovement();
	}
	
}
