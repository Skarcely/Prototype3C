using UnityEngine;
using System.Collections;

public class TPCamera : MonoBehaviour {
	
	
	public float distanceUp;
	public float distanceAway;
	public float smoothTime;
	public float dampTime;
	
	public Transform follow;
	
	
	private Vector3 targetPosition;
	private bool playerIsMoving;
	
	
	// Use this for initialization
	void Start () {
		
	}
	
	void Update(){
		playerIsMoving = (GameObject.FindObjectOfType(System.Type.GetType ("TPControllerV2")) as TPControllerV2).isMoving;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		
		if(playerIsMoving = true)
		{
			targetPosition = follow.position + follow.up * distanceUp - follow.forward * distanceAway;
			this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, Time.deltaTime * smoothTime);
			this.transform.position = targetPosition;
		
		var rotation = Quaternion.LookRotation(follow.position - transform.position);
		transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * dampTime);
		}
		else
		{
			
			
		}
	}
	
}
