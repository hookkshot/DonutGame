using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraController : MonoBehaviour {

    public List<Transform> Targets;
    public float Smoothness = 5f;
    public float ShakeAmount = 1f;
    public float ShakeTime = 0.5f;

    private Vector3 realPosition;
    private Vector3 shakeOffset = Vector3.zero;

    private float shakeStart = 0;
    private float shakeUntil = 0;
    private Vector3 shakePos = Vector3.zero;

    private float zLock;
    private float yLock = 0;

    public bool LockCameraY = false;
    


	// Use this for initialization
	void Awake () {

        realPosition = transform.position;
        zLock = realPosition.z;
        yLock = realPosition.y;
	}
	
	// Update is called once per frame
	void Update () {
        if (Targets.Count == 0)
            return;

        Vector3 targetPos = new Vector3(0,0,0);

        for (int i = 0; i < Targets.Count; i++)
        {
            targetPos.x += Targets[i].position.x;
            targetPos.y += Targets[i].position.y;
        }

        targetPos.x /= Targets.Count;
        targetPos.y /= Targets.Count;

        targetPos.z = transform.position.z;

        if (LockCameraY)
            targetPos.y = yLock;

        realPosition = Vector3.Lerp(realPosition, targetPos, Smoothness * Time.deltaTime);

        transform.position = realPosition + shakePos;

        if (Time.time < shakeUntil)
        {
            float p = Random.Range(0f, 1f);
            if (p > 0.5f)
            {
                CreateOffset();
            }

            shakePos = Vector3.Lerp(shakePos, shakeOffset, Time.deltaTime);
        }
        else if (shakePos != Vector3.zero) ;
            shakePos = Vector3.Lerp(shakePos, Vector3.zero, Time.deltaTime);
	}

    private void CreateOffset()
    {
        float x = Random.Range(-ShakeAmount, ShakeAmount);
        float y = Random.Range(-ShakeAmount, ShakeAmount);

        shakeOffset = new Vector3(x, y, 0);
    }

    public void Shake()
    {
        Shake(ShakeTime);
    }

    public void Shake(float time)
    {
        shakeStart = Time.time;
        shakeUntil = Time.time + time;
        CreateOffset();
    }

    public void Set(Vector2 position)
    {
        realPosition = new Vector3(position.x, position.y, zLock);
        if (LockCameraY)
            realPosition.y = yLock;
        transform.position = realPosition;
    }

    public void ClearTargets()
    {
        Targets.Clear();
    }

    public void AddTarget(Transform t)
    {
        Targets.Add(t);
    }
}
