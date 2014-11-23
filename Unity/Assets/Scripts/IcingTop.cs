using UnityEngine;
using System.Collections;

public class IcingTop : MonoBehaviour {

    float percent = 0;

    private Animator animator;
    
    //Timer Settings
    private float fadeSync = 0f;
    private float fadeInterval = 0.1f;


	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if(fadeSync > Time.time)
        {
            if(percent > 0)
                percent -= 0.25f;

            fadeSync = Time.time+fadeInterval;
            UpdateAnimator();
        }
	}

    public void AddIcing()
    {
        percent += 10;
        if (percent > 100)
            percent = 100;
        UpdateAnimator();
    }

    private void UpdateAnimator()
    {
        animator.SetFloat("Percent", percent);
    }
}
