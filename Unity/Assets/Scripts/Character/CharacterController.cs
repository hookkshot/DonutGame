﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(HealthSystem))]
public class CharacterController : MonoBehaviour {

    CameraController cameraController;
	public Transform GroundCheck;
    public Transform CameraTarget;
	public LayerMask GroundMask;
	public float speed = 20;
	public float jumpForce = 120;
	public Vector2 maxSpeed = new Vector2(15, 80);
	public GameObject JumpPrefab;

    private bool canJump = false;

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    public GameObject CharModel;
    private HealthSystem health;
    private HUD hud;

    public Transform AttackPosition;
    public GameObject GunPrefab;
    private float fireDelay = 0.05f;
    private float fireLast = 0;
    public GameObject FireSound;
    public float fireSoundDelay = 0.1f;
    private float fireSoundLast = 0;

    public GameObject ReloadSound;

    //Ammo Stuff
    public int AmmoCurrent = 1;
    public int AmmoMax = 1;

    public int PlayerNum = 0;
    private InputControl control = null;

    public List<PlayerPower> Powers = new List<PlayerPower>();

    void Awake()
    {
        cameraController = Camera.main.GetComponent<CameraController>();

        if (Game.ActivePlayerCount() > 1)
            cameraController.AddTarget(transform);
        else
            cameraController.AddTarget(CameraTarget.transform);
        

        animator = CharModel.GetComponent<Animator>();
        spriteRenderer = CharModel.GetComponent<SpriteRenderer>();
        health = GetComponent<HealthSystem>();
        hud = GetComponent<HUD>();
    }

	// Use this for initialization
	void Start () {
        health.Death += Death;
        health.Hit += Hit;
        health.SpriteRenderer = spriteRenderer;

        if (Game.Players.Length > PlayerNum)
        {
            if(Game.Players[PlayerNum] != null)
                control = Game.Players[PlayerNum];
        }

        if (control == null)
            control = new InputControl(ControlType.Keyboard);
	}
	
	// Update is called once per frame
    void Update()
    {
        UpdatePowers();

        //Debug stuff
        {
            if (Input.GetKeyDown(KeyCode.Q))
                health.TakeDamage(new DamageType(1,1), gameObject);
        }

        if (control.GetButton(ControlButton.Jump))
        {
            if (canJump)
            {
                normalJump();
            }
        }

        if (control.GetButton(ControlButton.Shoot))
        {
            Fire(new Color(0, 1, 0));
        }

        if (control.GetButton(ControlButton.ShootAlt))
        {
            Fire(new Color(1, 0, 0));
        }

        if(control.GetButton(ControlButton.Reload))
        {
            AmmoCurrent = AmmoMax;

            if(ReloadSound != null)
            {
                GameObject.Instantiate(ReloadSound, transform.position, Quaternion.identity);
            }
        }
    }

    private void Fire(Color c)
    {
        if (Time.time > fireLast + fireDelay && AmmoCurrent > 0)
        {
            AmmoCurrent--;

            float dir = transform.localScale.x;

            GameObject p = (GameObject)GameObject.Instantiate(GunPrefab, AttackPosition.position, Quaternion.identity);

            p.rigidbody2D.velocity = new Vector2(dir * Random.Range(4f, 5f) + transform.rigidbody2D.velocity.x, Random.Range(0.2f, 1.8f));
            SpriteRenderer r = p.GetComponent<SpriteRenderer>();
            Projectile pro = p.GetComponent<Projectile>();

            if(pro != null)
            {
                pro.Damage = new DamageType(0.2f, 0.2f);
                pro.Owner = gameObject;
            }

            if (r != null)
                r.color = c;

            if (FireSound != null && fireSoundLast + fireSoundDelay < Time.time)
            {
                GameObject.Instantiate(FireSound, AttackPosition.position, Quaternion.identity);
                fireSoundLast = Time.time;
            }

            fireLast = Time.time;
        }

    }

    private void GetInput()
    {
    }

    void FixedUpdate()
    {

        //This gets a float that is either negative or positive based on the buttons the plyaer is pressing
        float hori = control.GetAxis();

        animator.SetBool("Walking", hori != 0);
        animator.SetFloat("JumpForce", rigidbody2D.velocity.y);

        hori *= speed;
        if (HasPower(PowerType.Speed))
            hori *= 2;

        //this adds force to the player
        rigidbody2D.AddForce(new Vector2(hori, 0));


        if (hori > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if(hori<0)
            transform.localScale = new Vector3(-1, 1, 1);
        
    }

    public Color GetColor()
    {
        return spriteRenderer.color;
    }

	private void normalJump(){
		if (JumpPrefab != null) {
			GameObject.Instantiate(JumpPrefab, GroundCheck.position, Quaternion.identity);
		}
		rigidbody2D.AddForce(new Vector2(0, jumpForce));
	}

    private void Hit(GameObject source, float damage)
    {
        cameraController.Shake(1);
        HUD.Instance.UpdateHealth(PlayerNum, health.Health, health.HealthMax);
    }

    private void Death()
    {
        Destroy(gameObject);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "MovingPlatform")
            transform.parent = other.transform;
        if(other.gameObject.layer == LayerMask.NameToLayer("Platforms"))
            canJump = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "MovingPlatform")
            transform.parent = null;
        if (other.gameObject.layer == LayerMask.NameToLayer("Platforms"))
            canJump = false;
    }

    #region Powers

    public void AddPower(PlayerPower p)
    {
        for(int i = 0; i < Powers.Count; i++)
        {
            if (p.Type == Powers[i].Type)
            {
                p.Reset(p.Length);
                return;
            }
        }
        p.Reset(p.Length);
        Powers.Add(p);
    }

    public bool HasPower(PowerType t)
    {
        for (int i = 0; i < Powers.Count; i++)
        {
            if (t == Powers[i].Type)
            {
                return true;
            }
        }
        return false;
    }

    public void UpdatePowers()
    {
        for(int i = Powers.Count-1; i >= 0; i++)
        {
            Powers[i].Update();
            if (!Powers[i].Active)
                Powers.RemoveAt(i);
        }
    }

    #endregion
}

public class PlayerPower
{
    public PowerType Type = PowerType.Damage;
    public bool Active = true;
    public float Value = 2;
    public float Length = 10;

    private float until = 0;


    public PlayerPower(float value, float time, PowerType type)
    {
        Type = type;
        Length = time;
        Value = value;

        until = Time.time + time;
    }

    public void Update()
    {
        if (Time.time > until)
            Active = false;
    }

    public void Reset(float time)
    {
        until = Time.time + time;
    }
}

public enum PowerType
{
    Speed,
    Damage
}