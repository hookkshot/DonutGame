using UnityEngine;
using System.Collections;

public class FallingSpawner : MonoBehaviour {

	public float Interval = 5;
	public GameObject Prefab; 
	private float lastSpawn = 0;
	public DamageType Damage;


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
		if (Time.time > lastSpawn + Interval) {

			lastSpawn = Time.time;
			GameObject.Instantiate(Prefab, transform.position, Quaternion.identity);
		}
		if (transform.position.y < -1000) {
			Destroy(gameObject);		
		}
	}
}
