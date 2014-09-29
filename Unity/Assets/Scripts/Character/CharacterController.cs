using UnityEngine;
using System.Collections;

public class CharacterController : MonoBehaviour {

    CameraController cameraController;
	public Transform GroundCheck;
    public Transform CameraTarget;
	public LayerMask GroundMask;
	public float speed = 20;
	public float jumpForce = 120;
	public Vector2 maxSpeed = new Vector2(15, 80);
	public GameObject JumpPrefab;

    void Awake()
    {
        cameraController = Camera.main.GetComponent<CameraController>();
        cameraController.Target = CameraTarget;
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
        float hori = Input.GetAxisRaw("Horizontal") * speed;


        //this adds force to the player
        rigidbody2D.AddForce(new Vector2(hori, 0));
		if (rigidbody2D.velocity.x > 0 && rigidbody2D.velocity.x > maxSpeed.x) {
			rigidbody2D.velocity = new Vector2(maxSpeed.x, rigidbody2D.velocity.y);
		}
		else if (rigidbody2D.velocity.x < 0 && rigidbody2D.velocity.x < maxSpeed.x * -1){
			rigidbody2D.velocity = new Vector2(maxSpeed.x * -1, rigidbody2D.velocity.y);
		}

		if (rigidbody2D.velocity.y > 0 && rigidbody2D.velocity.y > maxSpeed.y) {
			rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, maxSpeed.y);
		}
		/*else if (rigidbody2D.velocity.y < 0 && rigidbody2D.velocity.y < maxSpeed.y * -1){
			rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, maxSpeed.y*-1);
		}*/

        if(Input.GetButtonDown("Jump"))
        {
			if (canJump ()){
				normalJump ();
			}
            //sd
        }

	
    }

	private bool canJump(){
		return  Physics2D.OverlapCircle(GroundCheck.transform.position, 0.4f, GroundMask);
	}

	private void normalJump(){
		if (JumpPrefab != null) {
			GameObject.Instantiate(JumpPrefab, GroundCheck.position, Quaternion.identity);
		}
		rigidbody2D.AddForce(new Vector2(0, jumpForce));
	}

}
