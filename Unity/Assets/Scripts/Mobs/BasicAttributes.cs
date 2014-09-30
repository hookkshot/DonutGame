using UnityEngine;
using System.Collections;

[RequireComponent(typeof(HealthSystem),typeof(Rigidbody2D),typeof(BoxCollider2D))]
public class BasicAttributes : MonoBehaviour {

	public int attackRange = 1;//How far away from the player until the mob attacks
	public int viewDistance = 4;//How far the mob has to be until it will lock onto a player
	public int attackStr = 1000000;//Int for determining damage
	public float moveSpeed = 10;//Move speed of Mob
	public Vector3 playerPosition;//Position of the player
	public float waypoint = 4;//Where the mob is moving towards
	public string mobName;

	public float MoveInterval = 10;//How often it changes direction
	float moveLast = 0;

	public void Wander(){//Patrol method for a mob, should make it walk back and forward until it detects a player
		float waypoint1 = waypoint;
		bool isAttack = true;

		if (isAttack == attackPlayer ()) {//Determining if a player is in sight.
			waypoint1 = playerPosition.x;
				}

		waypoint1 += transform.position.x;
		//transform.position = waypoint;

		Vector3 move = new Vector3 (waypoint - transform.position.x, 0, 0).normalized * Time.deltaTime * moveSpeed;
		transform.position += move;

	}

	bool attackPlayer (){ //Detecting if the player is going to be attacked by the mob or not
		bool attRet = false;
		float distance = GetPlayerDistance();

		if(viewDistance > distance){
			attRet = true;
			return attRet;
		}
		
		else{
			//Wander ();
			return attRet;
		}

	}
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

		attackPlayer ();

		if(Time.time > moveLast + MoveInterval)
		{
			SwitchDirection();
			moveLast = Time.time;
		}

		Move (moveSpeed);
	}

	void Move(float direction)
	{
		transform.position += new Vector3(moveSpeed * Time.deltaTime,0,0);
	}

	private float GetPlayerDistance()//How far away the mob is from the player
	{
		playerPosition = EnemyManager.Instance.Player.transform.position;
		float distance = Vector2.Distance (playerPosition, transform.position);
		return distance;
	}

	private Vector3 GetPlayerPosition()//Where the player is
	{
		playerPosition = EnemyManager.Instance.Player.transform.position;
		return playerPosition;
	}

	private void SwitchDirection()
	{
		moveSpeed *= -1;
	}
}
