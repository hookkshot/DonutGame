using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour {

	public float rightEdge = 4.00f;
	public float leftEdge = -4.00f;
	public float topEdge = 4.00f;
	public float bottomEdge = -4.00f;
	public float rotationSpeed = 0.00f;


	public float horizontalSpeed = 1;
	private float horizontalDirection = 1;

	public float verticalSpeed = 1;
	private float verticalDirection = 1;

	// Use this for initialization
	void Start () {
		rightEdge += transform.position.x;
		leftEdge += transform.position.x;
		topEdge += transform.position.y;
		bottomEdge += transform.position.y;
		horizontalDirection = horizontalSpeed;
		verticalDirection = verticalSpeed;
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void FixedUpdate() {

		if (transform.position.x > rightEdge) {
			horizontalDirection = horizontalSpeed*-1;
				}
		if (transform.position.x < leftEdge) {
			horizontalDirection = horizontalSpeed;
		}
		if (transform.position.y > topEdge) {
			verticalDirection = verticalSpeed*-1;
		}
		if (transform.position.y < bottomEdge) {
			verticalDirection = verticalSpeed;
		}
		transform.position += new Vector3 (horizontalDirection * Time.deltaTime, verticalDirection * Time.deltaTime, 0);
		transform.Rotate (new Vector3 (0, 0, rotationSpeed));
	
	}
}
