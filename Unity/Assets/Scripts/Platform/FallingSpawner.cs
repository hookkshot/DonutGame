using UnityEngine;
using System.Collections;

public class FallingSpawner : MonoBehaviour {

	public float Interval = 5;
	public GameObject Prefab; 
	private float lastSpawn = 0;



	// Use this for initialization
	void Start () {
		
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
