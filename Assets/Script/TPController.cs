using UnityEngine;
using System.Collections;

public class TPController : MonoBehaviour {
	
	//Private
	private float speed = 0.0f;
	private float h = 0.0f;
	private float v = 0.0f;
	private Vector3 movementVector;
	
	[HideInInspector]
	public bool canMove;
	
	// Use this for initialization
	void Start () {
		InitializeVar();
	}
	
	// Update is called once per frame
	void Update () {
		CheckInputs();
		//Debug.Log ("h =" + h);
		//Debug.Log ("v =" + v);
		
		MovementBehaviour();
		
	}
	
	void InitializeVar()
	{
		speed = 5.0f;
		canMove = true;
	}
	
	void MovementBehaviour()
	{	
		//Debug.Log (directionVector);
		
		if(canMove == true)
		{
			this.transform.Translate(movementVector*Time.deltaTime*speed);
		}
	}
	
	void CheckInputs()
	{
		h = Input.GetAxis("L_XAxis_1");
		v = Input.GetAxis("L_YAxis_1");
		
		movementVector = new Vector3(h, 0, v);
	
	}
	
	public void FreezeMovement()
	{
		//Debug.Log("FreezeMovement");
		canMove = false;	
	}
	
	public void FreeMovement()
	{
		canMove = true;	
	}
	
}
