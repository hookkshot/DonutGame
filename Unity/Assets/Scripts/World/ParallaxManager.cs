using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ParallaxManager : MonoBehaviour {

    private ParallaxCamera parallaxCamera;
    List<Parallax> layers = new List<Parallax>();

    public GameObject character;

	// Use this for initialization
	void Start () {
        parallaxCamera = Camera.main.GetComponent<ParallaxCamera>();

        if (parallaxCamera != null)
        {
            parallaxCamera.onCameraTransitionX += Move;
        }

        SetLayers();
	}
	
	void SetLayers()
    {
        layers.Clear();

        for(int i = 0; i < transform.childCount; i++)
        {
            Parallax layer = transform.GetChild(i).GetComponent<Parallax>();

            if(layer!=null)
            {
                layers.Add(layer);
            }
        }

        

    }

    void Update()
    {
        if (false)
        {
            Vector3 p = transform.position;
            p.y = character.transform.position.y;
            transform.position = p;
        }
    }

    void Move(float delta)
    {
        
        foreach(Parallax l in layers)
        {
            l.Move(delta);
        }
    }
}
