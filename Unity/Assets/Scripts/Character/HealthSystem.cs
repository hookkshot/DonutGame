/* Name: Health System
 * Desc: Stores and manipulates vitals for a game object
 * Author: Keirron Stach
 * Version: 1.0
 * Created: 1/04/2014
 * Edited: 22/04/2014
 */ 

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[AddComponentMenu("Health System/Health Attributes")]
public class HealthSystem : MonoBehaviour {

	#region Fields

	[SerializeField]
	private int health = 0;
	[SerializeField]
	private int healthMax = 100;

	#endregion

	#region Properties

	public int Health
	{
		get { return health; }
		set
		{
			health = value;
			if(health > healthMax)
				health = healthMax;
			if(Network.isServer)
			{
				if(networkView != null)
					networkView.RPC("ChangeHealth", RPCMode.Others, health);
				if(health <= 0)
					Die();
			}
		}
	}

	public int HealthMax
	{
		get { return healthMax; }
		set { healthMax = value; }
	}

	#endregion

	#region Events

	public delegate void EventActionInt (int val);
	public delegate void EventAction ();
	//public delegate void HitAction (NetworkPlayer player, int damage);
	public delegate void HitAction (GameObject source, int damage);

	public event EventAction Death;
	public event HitAction Hit;

	#endregion


    void Awake()
    {
        Health = HealthMax;
    }

	void Start()
	{
		
	}

    public void Respawn()
    {
        Health = HealthMax;
    }


	public void TakeDamage(DamageType damage, GameObject source)
	{

		int convertedDamage = damage.Damage + UnityEngine.Random.Range(0,damage.AltDamage);


		//If the even is not null raise the hit event with the converted Damage received
		

		if(convertedDamage > 0)
		{
			Health -= convertedDamage;
			lastDamage = Time.time;
		}

        if (Hit != null)
            Hit(source, convertedDamage);
	}

	public float RegenDelay = 1;
	private float lastDamage = 0;
	private float lastHealthRegen = 0;

	void Update()
	{

		if(Health < HealthMax)
		{
			if(Time.time - lastDamage > RegenDelay && Time.time - lastHealthRegen > 0.5f)
			{
				Health += 1;
				lastHealthRegen = Time.time;
			}
		}

	}

	void Die()
	{
		if(Network.isServer)
		{
			//Debug.Log("Player " + networkView.owner.ToString() + " died.");

			if(Death == null)
			{
                Destroy(gameObject);
			}
			else
			{
				Death();
			}
		}
	}

}

[System.Serializable]
public class DamageType
{

	public int Damage = 1;
	public int AltDamage = 0;

	public DamageType(int damage, int altDamage)
	{
		Damage = damage;
		AltDamage = altDamage;
	}

	public DamageType()
	{
	}
}
