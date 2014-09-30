using UnityEngine;
using System.Collections;

public class DamagePickUp : MonoBehaviour {


	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter2D(Collider2D other)
	{
		
		if (other.gameObject.layer == LayerMask.NameToLayer("Character"))
		{
			
			other.GetComponent<CharacterController>().AddPower(new PlayerPower(2, 15, PowerType.Damage));
			Destroy(gameObject);
			
		}
	}
}
