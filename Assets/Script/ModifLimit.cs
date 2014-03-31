using UnityEngine;
using System.Collections;

public class ModifLimit : MonoBehaviour {
	
	public float maxTranslateX = 20.0f;
	public float maxTranslateY = 20.0f;
	
	public float maxScaleX = 100.0f;
	public float maxScaleY = 100.0f;
	public float minScaleX = 1.0f;
	public float minScaleY = 1.0f;
	
	[HideInInspector]
	public Vector3 initialPos;
	[HideInInspector]
	public Vector3 initialScale;

	// Use this for initialization
	void Start () {
		
		initialPos = this.transform.position;
		initialScale = this.transform.localScale;
	
	}
	
	// Update is called once per frame
	void Update () {
		
		
	
	}
}
