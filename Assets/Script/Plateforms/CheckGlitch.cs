using UnityEngine;
using System.Collections;

public class CheckGlitch : MonoBehaviour {

	//Private
	private bool isInFrame;
	private Vector3 thisPosition;
	private float randomWait;
	private bool playerIsModifying;
	
	
	//Public
	public Camera refCam;
	public float marginRL = 0.3f;
	public float marginHB = 0.3f;
	public Material glitchMat;
	public Material normalMat;
	public float deltaGlitch = 1.0f;
	
	// Use this for initialization
	void Start () {
		isInFrame = false;
	}
	
	// Update is called once per frame
	void Update () {
		
		thisPosition = refCam.WorldToViewportPoint(this.transform.position);
		CheckInViewPort();
		
		playerIsModifying = (GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).isModifying;
		
		if(playerIsModifying)
		{
			ResetMat();
		}
		else if(isInFrame)
		{
//			Debug.Log ("Is in Frame");
			GlitchTexture();	
		}
		else
		{
			ResetMat();
		}
		
	}
	
	void CheckInViewPort()
	{
	
		if( (thisPosition.x >= marginRL && thisPosition.x <= 1 - marginRL) && (thisPosition.y <= 1 - marginHB && thisPosition.y >= marginHB))
		{
			isInFrame = true;
		}
		else
		{
			isInFrame = false;	
		}
		
	}
	
	void GlitchTexture()
	{
		
		randomWait = Random.Range(0,5) * deltaGlitch;
		StartCoroutine("WaitRandom" ,randomWait);
	}
	
	IEnumerator WaitRandom(float randomTimer)
	{
		this.transform.gameObject.renderer.material = normalMat;
		yield return new WaitForSeconds(randomTimer);
		this.transform.gameObject.renderer.material = glitchMat;
	}
	
	
	void ResetMat(){
		StopCoroutine("WaitRandom");
		this.transform.gameObject.renderer.material = normalMat;
	}
	
}
