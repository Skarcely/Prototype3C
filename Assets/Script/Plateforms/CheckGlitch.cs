using UnityEngine;
using System.Collections;

public class CheckGlitch : MonoBehaviour {

	//Private
	private bool isInFrame;
	private Vector3 thisPosition;
	private float randomWait;
	
	
	//Public
	public Camera refCam;
	public float marginRL = 0.1f;
	public float marginHB = 0.1f;
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
		
		if(isInFrame)
		{
			Debug.Log ("Is in Frame");
			GlitchTexture();	
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
//		Debug.Log("Passe dans GlitchTexture");
		this.transform.gameObject.renderer.material = glitchMat;
		Debug.Log ("Before wait = " + this.transform.gameObject.renderer.material);
		
		randomWait = Random.Range(0,5) * deltaGlitch;
		
		StartCoroutine(WaitRandom(randomWait));
		this.transform.gameObject.renderer.material = normalMat;
		Debug.Log ("Before wait = " + this.transform.gameObject.renderer.material);
		
	}
	
	IEnumerator WaitRandom(float randomTimer)
	{
//		Debug.Log("Passe dans WaitRandom");
		Debug.Log(randomTimer);
		yield return new WaitForSeconds(randomTimer);
	}
	
}
