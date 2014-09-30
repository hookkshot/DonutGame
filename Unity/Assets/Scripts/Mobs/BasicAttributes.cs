using UnityEngine;
using System.Collections;

[RequireComponent(typeof(HealthSystem),typeof(Rigidbody2D),typeof(BoxCollider2D))]
public class BasicAttributes : MonoBehaviour {

	public bool WillWander = false;
	public bool CanJump = false;
	public bool Flying = false;

	private Vector3 targetPosition;
	private GameObject targetPlayer;

	private bool isGroundInfront = false;

	public float AttackRange = 1;//How far away from the player until the mob attacks
	public float ViewDistance = 4;//How far the mob has to be until it will lock onto a player
	public DamageType Damage;
	public float MoveSpeed = 10;//Move speed of Mob

	void OnTriggerEnter2D(Collider2D other)
	{
		Trigger (other);
	}

	void OnTriggerStay2D(Collider2D other)
	{
		Trigger (other);
	}

	private void Trigger(Collider2D other)
	{
		if(other.gameObject.layer == LayerMask.NameToLayer("Platforms"))
		{
			isGroundInfront = true;
		}

		if(other.gameObject.layer == LayerMask.NameToLayer("Character"))
		{
			HealthSystem h = other.gameObject.GetComponent<HealthSystem>();

			if(h != null)
			{
				h.TakeDamage(Damage, gameObject);
				other.gameObject.rigidbody2D.AddForce(new Vector2(transform.localScale.x * MoveSpeed * 15,MoveSpeed*10));
			}
		}
	}

	private void Wander(){//Patrol method for a mob, should make it walk back and forward until it detects a player



		transform.position += Vector3.right * MoveSpeed * Time.deltaTime * transform.localScale.x;

		if (!isGroundInfront)
			SwitchDirection ();

		/*
		float waypoint1 = waypoint;
		bool isAttack = true;

		if (isAttack == attackPlayer ()) {//Determining if a player is in sight.
			waypoint1 = playerPosition.x;
				}

		waypoint1 += transform.position.x;
		//transform.position = waypoint;

		Vector3 move = new Vector3 (waypoint - transform.position.x, 0, 0).normalized * Time.deltaTime * moveSpeed;
		transform.position += move;
		*/
	}

	void FixedUpdate()
	{
		if (WillWander && targetPlayer == null) Wander ();

		isGroundInfront = false;
	}

	bool attackPlayer (){ //Detecting if the player is going to be attacked by the mob or not
		/*
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
		*/
		return false;
	}
	
	// Use this for initialization
	void Start () {
		if (Flying)
			rigidbody2D.gravityScale = 0;


	}
	
	// Update is called once per frame
	void Update () {


		/*
		attackPlayer ();

		if(Time.time > moveLast + MoveInterval)
		{
			SwitchDirection();
			moveLast = Time.time;
		}

		Move (moveSpeed);
		*/
	}

	void Move(float direction)
	{
		/*
		transform.position += new Vector3(moveSpeed * Time.deltaTime,0,0);*/
	}

	private float GetPlayerDistance(GameObject player)//How far away the mob is from the player
	{
		float distance = Vector2.Distance (player.transform.position, transform.position);
		return distance;
	}

	private void GetPlayer()//Where the player is
	{
	}

	private void SwitchDirection()
	{
		Vector3 s = transform.localScale;
		s.x = s.x * -1;
		transform.localScale = s;
	}
}
