using UnityEngine;
using System.Collections;

public class TimedEffect : MonoBehaviour {

    public float Life = 10;
    private float end = 0;

	// Use this for initialization
	void Start () {
        end = Time.time + Life;
	}
	
	// Update is called once per frame
	void Update () {
	    if(Time.time > end)
        {
            Destroy(gameObject);
        }
	}
}
