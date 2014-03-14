﻿using UnityEngine;
using System.Collections;

public class ReprogrammingStuff : MonoBehaviour {
	
	public float translateStep;
	public float translateSpeed;
	
	[HideInInspector]
	public bool isTranslatingX;
	
	private Vector3 translateVector;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if(isTranslatingX)
		{
			
			Translate ((GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify, 0);
			
		}
		
	}
	
	public void Translate(GameObject target, int TranslateDirection ){
		
		
		if( TranslateDirection == 0) // Si Translate X
		{	
		
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
		
	}
	
}
