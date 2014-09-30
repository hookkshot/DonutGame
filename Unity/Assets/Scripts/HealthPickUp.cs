using UnityEngine;
using System.Collections;

public class HealthPickUp : MonoBehaviour {
	public float Health = 20;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter2D(Collider2D other)
	{

		if (other.gameObject.layer == LayerMask.NameToLayer("Character"))
		{
			HealthSystem h = other.gameObject.GetComponent<HealthSystem>();
			if (h != null)
			{
				h.Health += Health;
				Destroy(gameObject);
			}
		}
	}

}
