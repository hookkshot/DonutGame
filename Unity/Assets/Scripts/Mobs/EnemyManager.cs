using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour {

	public static EnemyManager Instance;

	public GameObject Player;

	void Awake()
	{
		Instance = this;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
