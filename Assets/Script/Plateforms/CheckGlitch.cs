using UnityEngine;
using System.Collections;

public class CheckGlitch : MonoBehaviour {

	//Private
	[HideInInspector]
	public bool isInFrame;
	private Vector3 thisPosition;
	private float randomWait;
	private bool playerIsModifying;
	
	
	//Public
	public Camera refCam;
	public float marginRL = 0.3f;
	public float marginHB = 0.3f;
	public float distanceZ = 20.0f;
	
	public float aimMarginRL = 0.05f;
	public float aimMarginHB = 0.05f;
	public float aimDistanceZ = 15.0f;
	
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
//		Debug.Log (thisPosition);
		CheckInViewPort();
		
		playerIsModifying = (GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).isModifying;
		
		if(playerIsModifying)
		{
			ResetMat();
		}
		else if(isInFrame && (GameObject.FindObjectOfType(System.Type.GetType ("PauseMenu")) as PauseMenu).isPausing == false )
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
	
		if((GameObject.FindObjectOfType(System.Type.GetType("TPControllerV2")) as TPControllerV2).isAiming == false)
		{
			if( (thisPosition.x >= marginRL && thisPosition.x <= 1 - marginRL) && (thisPosition.y <= 1 - marginHB && thisPosition.y >= marginHB) && (thisPosition.z <= distanceZ))
			{
				isInFrame = true;
			}
			else
			{
				isInFrame = false;	
			}
		}
		
		if((GameObject.FindObjectOfType(System.Type.GetType("TPControllerV2")) as TPControllerV2).isAiming == true)
		{
			if( (thisPosition.x >= aimMarginRL && thisPosition.x <= 1 - aimMarginRL) && (thisPosition.y <= 1 - aimMarginHB && thisPosition.y >= aimMarginHB) && (thisPosition.z <= aimDistanceZ))
			{
				isInFrame = true;
			}
			else
			{
				isInFrame = false;	
			}
		}
		
	}
	
	void GlitchTexture()
	{
		
		randomWait = Random.Range(0,20) * deltaGlitch;
//		Debug.Log (randomWait);
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
