using UnityEngine;
using System.Collections;

public class IcingTop : MonoBehaviour {

    float percent = 0;

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    
    //Timer Settings
    private float fadeSync = 0f;
    private float fadeInterval = 0.1f;

    private PowerType power = PowerType.Speed;


	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        if(fadeSync < Time.time)
        {
            if(percent > 0)
                percent -= 0.25f;


            fadeSync = Time.time+fadeInterval;
            UpdateAnimator();
        }
	}

    public void AddIcing(Color color, PowerType power)
    {
        this.power = power;
        Color n = Color.Lerp(spriteRenderer.color, color, 0.1f);
        spriteRenderer.color = n;
        Debug.Log(n.ToString());

        percent += 2.5f;
        if (percent > 100)
            percent = 100;
        UpdateAnimator();
    }

    private void UpdateAnimator()
    {
        
        animator.SetFloat("Percent", percent);
    }

    public PowerType GetPower()
    {
        if (percent > 10)
            return power;
        return PowerType.None;
    }
}
