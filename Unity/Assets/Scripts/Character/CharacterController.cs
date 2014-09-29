using UnityEngine;
using System.Collections;

public class CharacterController : MonoBehaviour {

    CameraController cameraController;

    public Transform cameraTarget;

    void Awake()
    {
        cameraController = Camera.main.GetComponent<CameraController>();
        cameraController.Target = cameraTarget;
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
    void Update()
    {

        //Debug stuff
        {
            if (Input.GetKeyDown(KeyCode.Q))
                cameraController.Shake(4);
        }
    }

    void FixedUpdate()
    {

        //This gets a float that is either negative or positive based on the buttons the plyaer is pressing
        float hori = Input.GetAxisRaw("Horizontal");



        //this adds force to the player
        rigidbody2D.AddForce(new Vector2(hori, 0));

        if(Input.GetButtonDown("Jump"))
        {
            rigidbody2D.AddForce(new Vector2(0, 120));
            //sd
        }
    }


}
