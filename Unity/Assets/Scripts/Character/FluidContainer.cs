using UnityEngine;
using System.Collections;

public class FluidContainer : MonoBehaviour {

	public GameObject liquidPrefab;

	public Transform liquidPosition;
	private const int MAX_CONTAINER_SIZE = 14; //The max amount of pixels the container can hold 
	private GameObject[] ammoLiquid; //array of liquid objects
	private int currentAmmoFill; //the last ammo checked for;
	// Use this for initialization
	void Start () {
		ammoLiquid = new GameObject[MAX_CONTAINER_SIZE];
		//Calculate percent of current ammo
		currentAmmoFill = ammoBlocks ();
		for (int i=0; i < currentAmmoFill; i++) {
			addLiquid (findTop());
		}
	}


	//Finds the top empty
	private int findTop(){
		int count = 0;
		while (count < ammoLiquid.Length) {
						if (ammoLiquid [count] == null) {
				return count;
						}
						count++;
				}
		return-1;
	}

	//Finds the top not empy
	private int findTopDe(){
		int count = 0;

		if(ammoLiquid.Length == 0){
			return -1;
		}

		while (count < ammoLiquid.Length) {
			if (ammoLiquid [count] == null) {
				return count-1;
			}
			count++;
		}
		return ammoLiquid.Length-1;
	}

	private void addLiquid(int ind){
		if (ind != -1) {
						ammoLiquid [ind] = (GameObject)GameObject.Instantiate (liquidPrefab, new Vector3 (liquidPosition.transform.position.x, (transform.position.y - 0.120f) + (ind * (liquidPrefab.renderer.bounds.size.y * 0.3333f)), 0), transform.rotation);
						ammoLiquid [ind].transform.parent = transform;
				}
	}

	//returns percent of current ammo
	private int ammoBlocks(){
		float maxAmmo = transform.parent.parent.GetComponent<CharacterController>().AmmoMax;
		float lastAmmo = transform.parent.parent.GetComponent<CharacterController>().AmmoCurrent;
		float currentAmmoPercent = 0;

		if (lastAmmo != 0) {
						currentAmmoPercent = (lastAmmo / maxAmmo) * 100;
				
			int showBlocks = 0;
			float ammoBlocks = (currentAmmoPercent * MAX_CONTAINER_SIZE) / 100;
			if(ammoBlocks > 0 && ammoBlocks < 1)
			{
				showBlocks = 1;
			}else{
				showBlocks = (int)Mathf.FloorToInt(ammoBlocks);
			}
			return showBlocks;
		}
		return 0;
	}

	// Update is called once per frame
	void Update () {
		int currentAmmo = ammoBlocks ();
		if (currentAmmo > currentAmmoFill) {
			int index = findTop();
			Debug.Log ("Adding " + index);
					addLiquid (index);
			if(currentAmmoFill != currentAmmo)
			{
				currentAmmoFill++;
			}

		}else if (currentAmmo < currentAmmoFill) {
					int index = findTopDe ();
					if(index > -1){
				Debug.Log ("Deleting " + (index));
						Destroy(ammoLiquid[index]);
					}
			if(currentAmmoFill != currentAmmo)
			{
				currentAmmoFill--;
			}

			
		}
	}
}
