using UnityEngine;
using System.Collections;

public class enemyBehavior : MonoBehaviour {

	//Public Var
	public int speedEnemy;
	public int paternMax;
	public GameObject perso;
	public GameObject startPoint;

	//Private
	private Vector3 pos;
	private Vector3 initPos;
	private Vector3 firstPos;
	private Vector3 tempPos;
	private Quaternion firstRotation;  
	private bool isMoving;
	private float patternLenght;
	private float tempPatternLenght;
	private Vector3 patternLenghtVector;
	private bool hasAggro;

	// Use this for initialization
	void Start ()
	{
		initVar();
		isMoving = true;
		hasAggro = false;
	}
	
	// Update is called once per frame
	void Update () {
		move();
		storePos();
		checkPattern();	
		testAggro();
	}

	void lateUpdate()
	{

	}

	//Enemy is moving every frame in front of him
	void move()
	{
		if (isMoving ==  true)
		{
			this.transform.Translate(Vector3.forward*speedEnemy*Time.deltaTime);
		}
	}
		

	//Check if enemy has turn
	void checkPattern()
	{
		if (patternLenght > paternMax)
		{
			this.transform.forward = -this.transform.forward;
			resetPattern();
		
		}
	}

	//Check Pattern lenght every frame
	void storePos()
	{
	 	pos = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
	 	patternLenght = Vector3.Distance(initPos, pos);
	}

	void initVar()
	{
		initPos = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
		patternLenght = Vector3.Distance(initPos, pos);
		firstPos = initPos;
		firstRotation = this.transform.rotation;
	}

	//Turn back when pattern is over
	void resetPattern()
	{
		initPos = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
	}

	void testAggro()
	{
		if (((GameObject.FindObjectOfType(System.Type.GetType ("aggroZone")) as aggroZone).aggro == true) && (hasAggro == false))
		{
			Debug.Log("WTF");
			//store temp var for the Caract to get back to previous Pos
			tempPos = this.transform.position;
			tempPatternLenght = Vector3.Distance(initPos, pos);
			//enemy is following the caract
			this.transform.rotation = Quaternion.LookRotation(perso.transform.position - this.transform.position);
			hasAggro = true;
		}

		if (((GameObject.FindObjectOfType(System.Type.GetType ("aggroZone")) as aggroZone).aggro == false) && (hasAggro == true))
		{
			returnToPattern();
		}
	}

	void returnToPattern()
	{
		Debug.Log("ok");
		this.transform.LookAt(firstPos);
		if (this.transform.position == firstPos)
		{
			Debug.Log("Ok colis bien arrivé");
			this.transform.rotation = firstRotation;
			hasAggro = false;
		}
		
	}
}
