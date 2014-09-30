using UnityEngine;
using System.Collections;

public class SpeedPickUp : MonoBehaviour {
	public float Speed = 10;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter2D(Collider2D other)
	{
		
		if (other.gameObject.layer == LayerMask.NameToLayer("Character"))
		{

			other.GetComponent<CharacterController>().speed += Speed;
			Destroy(gameObject);

		}
	}
}
