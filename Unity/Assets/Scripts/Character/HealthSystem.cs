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
	private float health = 0;
	[SerializeField]
	private float healthMax = 100;


    public SpriteRenderer SpriteRenderer;
	#endregion

	#region Properties

	public float Health
	{
		get { return health; }
		set
		{
			health = value;
			if(health > healthMax)
				health = healthMax;
			if(health <= 0)
				Die();

		}
	}

	public float HealthMax
	{
		get { return healthMax; }
		set { healthMax = value; }
	}

	#endregion

	#region Events

	public delegate void EventActionInt (float val);
	public delegate void EventAction ();
	//public delegate void HitAction (NetworkPlayer player, int damage);
	public delegate void HitAction (GameObject source, float damage);

	public event EventAction Death;
	public event HitAction Hit;

	#endregion


    void Awake()
    {
        Health = HealthMax;
        SpriteRenderer = GetComponent<SpriteRenderer>();
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

		float convertedDamage = damage.Damage + UnityEngine.Random.Range(0f,damage.AltDamage);


		//If the even is not null raise the hit event with the converted Damage received
		

		if(convertedDamage > 0)
		{
			Health -= convertedDamage;
			lastDamage = Time.time;
		}

        SetHit();

        if (Hit != null)
            Hit(source, convertedDamage);
	}

	public float RegenDelay = 1;
	private float lastDamage = 0;
	private float lastHealthRegen = 0;

	void Update()
	{
        HitUpdate();
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

    private Color hitHighlight = new Color(1,0,0);
    private Color hitNormal = new Color(1, 1, 1);
    private bool hitFade = false;

    public void SetHit()
    {
        hitFade = true;
    }

    private void HitUpdate()
    {
        if(SpriteRenderer != null)
        {
            if (hitFade)
            {
                SpriteRenderer.color = Color.Lerp(SpriteRenderer.color, hitHighlight, 1);
                if (SpriteRenderer.color == hitHighlight)
                    hitFade = false;
            }
            else if (SpriteRenderer.color != hitNormal)
                SpriteRenderer.color = Color.Lerp(SpriteRenderer.color, hitNormal, Time.deltaTime*3);
        }
    }

}

[System.Serializable]
public class DamageType
{

	public float Damage = 1;
	public float AltDamage = 0;

	public DamageType(float damage, float altDamage)
	{
		Damage = damage;
		AltDamage = altDamage;
	}

	public DamageType()
	{
	}
}
