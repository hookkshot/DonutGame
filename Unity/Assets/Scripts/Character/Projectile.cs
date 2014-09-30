using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

    public bool RandomSize = false;

    private float scale = 1;

    public GameObject Owner;

    public DamageType Damage;

    public PowerType IcingType = PowerType.Speed;

    public float Life = 10;
    public float Fade = 10;

    private float fadeStart = -1;
    private float fadeEnd = 0;

	// Use this for initialization
	void Start () {
	    if(RandomSize)
        {
            scale = Random.Range(0.5f,1.2f);
            transform.localScale = new Vector3(scale,scale,scale);
        }

        fadeStart = Time.time + Life;
        fadeEnd = fadeStart + Fade;
	}
	
	// Update is called once per frame
	void Update () {
        if (rigidbody2D.gravityScale != 0)
        {
            float p = rigidbody2D.velocity.x / 5;
            if (p < 0) p *= -1;
            if (p > 0.6f) p = 0.6f;

            transform.localScale = new Vector3(scale, (1 - p) * scale, scale);
        }
        else
        {
            //Shrink the icing after its life is up until its destroys it self
            if(Time.time > fadeStart)
            {
                float d = fadeEnd - fadeStart;
                float n = Time.time - fadeStart;

                float p = n / d;
                float s = scale - scale * p;
                transform.localScale = new Vector3(s, s, s);
            }
            if(Time.time > fadeEnd)
            {
                Destroy(gameObject);
            }
        }
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == Owner || other.gameObject.layer == LayerMask.NameToLayer("Projectiles") || rigidbody2D.gravityScale == 0)
            return;

        HealthSystem h = other.gameObject.GetComponent<HealthSystem>();

        if (h != null)
        {
            h.TakeDamage(Damage, Owner);
        }


        if (other.gameObject.layer == LayerMask.NameToLayer("Platforms"))
        {

            transform.localScale = new Vector3(scale, scale, scale);
            rigidbody2D.velocity = new Vector2(0, 0);
            rigidbody2D.gravityScale = 0;
            Owner = null;
            

            if (other.tag == "MovingPlatform")
                transform.parent = other.transform;

            SpriteRenderer r = other.gameObject.GetComponent<SpriteRenderer>();
            SpriteRenderer pr = GetComponent<SpriteRenderer>();
            if (r != null && pr != null)
            {
                r.color = Color.Lerp(r.color, pr.color, Time.deltaTime);
            }
        }

        if(other.gameObject.layer == LayerMask.NameToLayer("AI") && rigidbody2D.gravityScale != 0)
        {
            Destroy(gameObject);
        }
    }
}
