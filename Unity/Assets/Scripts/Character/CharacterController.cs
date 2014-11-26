using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(HealthSystem))]
public class CharacterController : MonoBehaviour {

    //Attached Systems
    private CameraController cameraController;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    public GameObject CharModel;
    public GameObject InteractLight;
    private HealthSystem health;
    private InputControl control = null;
    private Color playerColor = new Color(0, 1, 0);

    //Movement Modifiers
	public float Speed = 20;
	public float JumpForce = 120;

    //State Check
    private bool canJump = false;
   
    //Targets
    public Transform AttackPosition;
    public Transform CameraTarget;
    public Transform GroundCheck;
    private InteractObject interactObject;
    
    //Projectiles Modifiers
    private float fireDelay = 0.05f;
    private float fireLast = 0;
    public float fireSoundDelay = 0.1f;
    private float fireSoundLast = 0;

    public Dictionary<PowerType, Power> Powers = new Dictionary<PowerType, Power>();
    private PowerType currentPower = PowerType.Speed;

    //Prefabs
    public GameObject JumpPrefab;
    public GameObject ReloadSound;
    public GameObject FireSound;
    public GameObject GunPrefab;

    //Ammo Stuff
    public int AmmoCurrent = 1;
    public int AmmoMax = 1;

    public int PlayerNum = 0;

    

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

        //Make the Ammo defaults
        Powers.Add(PowerType.Speed, new Power(1.5f, 2, new Color(0, 1, 0)));
        Powers.Add(PowerType.Jump, new Power(1.5f, 2, new Color(0.3f, 0.3f, 1)));
        Powers.Add(PowerType.Solid, new Power(2, 2, new Color(0, 1, 0)));
        Powers.Add(PowerType.Sticky, new Power(2, 2, new Color(0, 1, 0)));
        Powers.Add(PowerType.Weight, new Power(2, 2, new Color(0, 1, 0)));

        SetPower(PowerType.Speed);
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
            if (Input.GetKeyDown(KeyCode.Alpha1))
                currentPower = PowerType.Speed;
            if (Input.GetKeyDown(KeyCode.Alpha2))
                currentPower = PowerType.Jump;
        }

        if (control.GetButton(ControlButton.Jump))
        {
            if (canJump)
            {
                Jump();
            }
        }

        if (control.GetButton(ControlButton.Shoot))
        {
            Fire();
        }

        if (control.GetButton(ControlButton.ShootAlt))
        {
            Fire();
        }

        if(control.GetButton(ControlButton.Interact))
        {
            if(interactObject!= null)
                interactObject.Interact(this);
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

    private void Fire()
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
                pro.IcingType = currentPower;
            }

            if (r != null)
                r.color = GetColor();

            if (FireSound != null && fireSoundLast + fireSoundDelay < Time.time)
            {
                GameObject.Instantiate(FireSound, AttackPosition.position, Quaternion.identity);
                fireSoundLast = Time.time;
            }

            fireLast = Time.time;
        }

    }

    void Move()
    {
        //This gets a float that is either negative or positive based on the buttons the plyaer is pressing
        float hori = control.GetAxis();

        animator.SetBool("Walking", hori != 0);
        animator.SetFloat("JumpForce", rigidbody2D.velocity.y);

        hori *= Speed;
        if (PowerActive(PowerType.Speed))
            hori *= PowerValue(PowerType.Speed);



        //this adds force to the player
        rigidbody2D.AddForce(new Vector2(hori, 0));

        float x = transform.localScale.x;

        if (hori > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (hori < 0)
            transform.localScale = new Vector3(-1, 1, 1);

        if (x != transform.localScale.x)
            animator.SetTrigger("Switch");
    }

    void FixedUpdate()
    {

        Move();
        
    }

    public Color GetColor()
    {
        return Powers[currentPower].Color();
    }

	private void Jump(){
		if (JumpPrefab != null) {
			GameObject.Instantiate(JumpPrefab, GroundCheck.position, Quaternion.identity);
		}
        float force = JumpForce;
        if (PowerActive(PowerType.Jump))
            force *= PowerValue(PowerType.Jump);
		rigidbody2D.AddForce(new Vector2(0, force));
	}

    private void Hit(GameObject source, float damage)
    {
        cameraController.Shake(1);
        HUD.Instance.UpdateHealth(PlayerNum, health.Health, health.HealthMax);
    }

    public void SetPower(PowerType t)
    {
        currentPower = t;
        AmmoCurrent = AmmoMax;
    }

    #region Collider Methods

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Dispenser"))
        {
            interactObject = other.gameObject.GetComponent<InteractObject>();
            if(interactObject != null)
                InteractLight.SetActive(true);
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "MovingPlatform")
            transform.parent = other.transform;
        if(other.gameObject.layer == LayerMask.NameToLayer("Platforms"))
            canJump = true;
        if (other.gameObject.layer == LayerMask.NameToLayer("Icing"))
        {
            IcingTop icing = other.GetComponent<IcingTop>();
            if(icing != null)
            {
                PowerType p = icing.GetPower();
                if(p != PowerType.None)
                    ActivatePower(p);
            }
            
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "MovingPlatform")
            transform.parent = null;
        if (other.gameObject.layer == LayerMask.NameToLayer("Platforms"))
            canJump = false;
        if (other.gameObject.layer == LayerMask.NameToLayer("Dispenser"))
        {
            interactObject = null;
            InteractLight.SetActive(false);
        }

    }

    #endregion

    #region Death and Respawn

    /// <summary>
    /// Respawns the player at either the center of the screen of the start of the map.
    /// </summary>
    public void Respawn()
    {
        health.Health -= 10;
        if (Game.ActivePlayerCount() > 1)
            transform.position = Camera.main.transform.position;
        else
            transform.position = Game.Instance.StartPosition.position;
    }

    private void Death()
    {
        gameObject.SetActive(false);
    }

    #endregion

    #region Powers


    public bool PowerActive(PowerType t)
    {
        return Powers[t].IsActive();
    }

    public float PowerValue(PowerType t)
    {
        return Powers[t].Value();
    }

    public void ActivatePower(PowerType t)
    {
        Powers[t].Reset();
    }

    public void UpdatePowers()
    {
        foreach(KeyValuePair<PowerType,Power> pair in Powers)
        {
            pair.Value.Update();
        }
    }

    #endregion
}

/// <summary>
/// Power used by the player with attributes to check the value of the power and if it is active.
/// </summary>
public class Power
{

    private Color color = new Color(0,1,0);
    private bool active = false;
    private float value = 2;
    private float length = 10;

    private float until = 0;

    /// <summary>
    /// Create a new Power
    /// </summary>
    /// <param name="value">The magnitude of the power.</param>
    /// <param name="time">How long it lasts for when activates (In seconds).</param>
    /// <param name="color">The color of the Power.</param>
    public Power(float value, float time, Color color)
    {
        this.active = false;
        this.color = color;
        this.length = time;
        this.value = value;

        until = Time.time + time;
    }

    public void Update()
    {
        if (Time.time > until)
            active = false;
    }

    public void Reset()
    {
        active = true;
        until = Time.time + length;
    }

    #region Getters

    public bool IsActive()
    {
        return active;
    }

    public float Value() { return value; }
    public Color Color() { return color; }

    #endregion
}

public enum PowerType
{
    Speed,
    Weight,
    Jump,
    Solid,
    Sticky,
    None
}