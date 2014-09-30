using UnityEngine;
using System.Collections;

public class WorldBottom : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Character"))
        {
            CharacterController c = other.gameObject.GetComponent<CharacterController>();
            c.Respawn();
            return;
        }

        Destroy(other.gameObject);
    }
}
