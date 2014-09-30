using UnityEngine;
using System.Collections;

public class ParallaxCamera : MonoBehaviour {

    public delegate void ParallaxCameraDelegate(float deltaMovement);
    public ParallaxCameraDelegate onCameraTransitionX;
    private float oldPosition;

	// Use this for initialization
	void Start () {
        oldPosition = transform.position.x;
	}
	
	// Update is called once per frame
	void Update () {

	    if(transform.position.x != oldPosition)
        {

            if(onCameraTransitionX != null)
            {
                onCameraTransitionX(oldPosition - transform.position.x);
            }

            oldPosition = transform.position.x;
        }
	}
}
