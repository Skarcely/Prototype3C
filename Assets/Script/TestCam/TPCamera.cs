using UnityEngine;
using System.Collections;

public class TPCamera : MonoBehaviour {
	
	
	public float distanceUp;
	public float distanceAway;
	public float smoothTime;
	
	public Transform follow;
	
	
	private Vector3 targetPosition;
	
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {
		
		targetPosition = follow.position + follow.up * distanceUp - follow.forward * distanceAway;
		
		this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, Time.deltaTime * smoothTime);
//		this.transform.position = targetPosition;
		
		transform.LookAt(follow);
		
	}
	
}
