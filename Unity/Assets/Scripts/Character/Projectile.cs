using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

    public bool RandomSize = false;

    private float scale = 1;

	// Use this for initialization
	void Start () {
	    if(RandomSize)
        {
            scale = Random.Range(0.5f,1.2f);
            transform.localScale = new Vector3(scale,scale,scale);
        }
	}
	
	// Update is called once per frame
	void Update () {
        float p = rigidbody2D.velocity.x / 5;
        if (p < 0) p *= -1;
        if (p > 0.6f) p = 0.6f;
        
        transform.localScale = new Vector3(scale,(1-p)*scale,scale);
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Platforms"))
        {
            transform.localScale = new Vector3(scale, scale, scale);
            Destroy(rigidbody2D);
            Destroy(this);

            if (other.tag == "MovingPlatform")
                transform.parent = other.transform;

            SpriteRenderer r = other.gameObject.GetComponent<SpriteRenderer>();
            SpriteRenderer pr = GetComponent<SpriteRenderer>();
            if(r != null && pr != null)
            {
                r.color = Color.Lerp(r.color, pr.color, Time.deltaTime);
            }
        }
    }
}
