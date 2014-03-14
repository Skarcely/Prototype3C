using UnityEngine;
using System.Collections;

public class TPCamera : MonoBehaviour {
	
	//Public
	public float distanceAway;
	public float distanceUp;
	public float smooth;
	public Transform pivot;
	public float speedRotate;
	
	//Private
	
	private Vector3 targetPosition;
	private float xRot;
	private float yRot;
	
	// Use this for initialization
	void Start () {
		InitializeVar();
	}
	
	void InitializeVar()
	{
		pivot = GameObject.FindWithTag("Player").transform;	
	}
	
	// Update is called once per frame
	void Update () {
	
		CheckInputs();
		
		
		if(xRot != 0 || yRot !=0){ 
			RotateBehavior();
		}
		else
		{
			CustomUpdate();	
		}
		
		//transform.LookAt(pivot);
	}
	
	void CustomUpdate()
	{
		
		//Debug.Log("0 ? xRot = " + xRot + " yRot = " + yRot);
		
		targetPosition = pivot.position + pivot.up * distanceUp - pivot.forward * distanceAway;
		
		//Debug.DrawRay(pivot.position, Vector3.up * distanceUp, Color.red);
		//Debug.DrawRay(pivot.position, -1f * pivot.forward * distanceAway, Color.blue);
		//Debug.DrawLine(pivot.position, targetPosition, Color.magenta);
		
		transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime*smooth);			
		
		transform.LookAt(pivot);
		
	}
	
	void CheckInputs(){
		
		xRot = speedRotate * Input.GetAxis("RightV");
		yRot = speedRotate * Input.GetAxis("RightH");
		
		
	}
	
	private void RotateBehavior()
	{
		Debug.Log ("Rotation manuelle");
		transform.Rotate(xRot, yRot, 0.0f);
			
	}
	
}
