using UnityEngine;
using System.Collections;

public class FallingBlock : MonoBehaviour {

	public DamageType Damage;
	public float HorizontalDirection = 0;
	public float VerticalDirection = 0;
	public float Speed = 0.05f;




	// Use this for initialization
	void Start () {
	
	}

	void OnTriggerEnter2D(Collider2D other)	{
		
		if (other.gameObject.layer == LayerMask.NameToLayer ("Character")) {
			
			HealthSystem h = other.gameObject.GetComponent<HealthSystem>();
			if(h != null){
				
				h.TakeDamage(Damage, gameObject);
				Destroy(gameObject);
				
			}
			
		}
	
	}

	// Update is called once per frame
	void Update () {
		if (HorizontalDirection > 0) {
			transform.position += new Vector3(Speed*Time.deltaTime,0,0);
		}
		if (HorizontalDirection < 0) {
			transform.position -= new Vector3(Speed*Time.deltaTime,0,0);
		}
		if (VerticalDirection > 0) {
			transform.position += new Vector3(0,Speed*Time.deltaTime,0);	
		}
		if (VerticalDirection < 0) {
			transform.position -= new Vector3(0,Speed*Time.deltaTime,0);
		}

		if (transform.position.y < -10) {
			Destroy(gameObject);		
		}
	}
}
