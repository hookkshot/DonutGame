using UnityEngine;
using System.Collections;

public class Effect : MonoBehaviour {

    public string SortLayer = "";

	// Use this for initialization
	void Start () {
        if (renderer != null && SortLayer != "")
            renderer.sortingLayerName = SortLayer;
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
