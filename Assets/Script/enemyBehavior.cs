using UnityEngine;
using System.Collections;

public class enemyBehavior : MonoBehaviour {

	//Public Var
	public int speedEnemy;
	public int paternMax;

	//Private
	private Vector3 pos;
	private Vector3 initPos;
	private bool isMoving;
	private float patternLenght;
	private Vector3 patternLenghtVector;

	// Use this for initialization
	void Start ()
	{
		initVar();
	}
	
	// Update is called once per frame
	void Update () {
		move();
		storePos();
		checkPattern();	
	}

	void lateUpdate()
	{

	}

	void move()
	{
		if (isMoving ==  true)
		{
			this.transform.Translate(Vector3.forward*speedEnemy*Time.deltaTime);
		}
	}
		

	void checkPattern()
	{
		if (patternLenght > paternMax)
		{
			Debug.Log("Stop");
			isMoving = false;
		
		}
	}

	void storePos()
	{
	 	pos = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
	 	patternLenght = Vector3.Distance(initPos, pos);
	}

	void initVar()
	{
		initPos = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
		isMoving = true;
	}
}
