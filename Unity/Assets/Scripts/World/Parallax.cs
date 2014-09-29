using UnityEngine;
using System.Collections;

public class Parallax : MonoBehaviour {

    public float factor = 1;

	public void Move(float delta)
    {
        Vector3 newPos = transform.localPosition;
        newPos.x -= delta * factor;

        transform.localPosition = newPos;
    }
}
