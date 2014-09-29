using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ParallaxManager : MonoBehaviour {

    private ParallaxCamera parallaxCamera;
    List<Parallax> layers = new List<Parallax>();

	// Use this for initialization
	void Start () {
        parallaxCamera = Camera.main.GetComponent<ParallaxCamera>();

        if (parallaxCamera != null)
            parallaxCamera.onCameraTransition += Move;

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

    void Move(float delta)
    {
        Vector3 p = transform.position;
        p.y = Camera.main.transform.position.y;
        transform.position = p;
        foreach(Parallax l in layers)
        {
            l.Move(delta);
        }
    }
}
