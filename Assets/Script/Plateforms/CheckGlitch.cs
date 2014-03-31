using UnityEngine;
using System.Collections;

public class CheckGlitch : MonoBehaviour {

	
	private bool isvisible;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		isvisible = renderer.isVisible;
		Debug.Log (isvisible);
	}
	
	void OnBecameVisible() {
     	Debug.Log("OnBecameVisible");  
	}
	void OnBecameInvisible() {
     	Debug.Log("Plus visible");  
	}
}
