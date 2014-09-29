using UnityEngine;
using System.Collections;

public class Effect : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        bool alive = false;
	    
        if(audio != null)
        {
            if (audio.isPlaying)
                alive = true;
        }

        if(particleSystem != null)
        {
            if (particleSystem.isPlaying)
                alive = true;
        }

        if(!alive)
        {
            Destroy(gameObject);
        }
	}
}
