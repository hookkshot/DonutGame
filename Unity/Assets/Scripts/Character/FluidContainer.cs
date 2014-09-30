using UnityEngine;
using System.Collections;

public class FluidContainer : MonoBehaviour {

	public GameObject liquidPrefab;

	private const int MAX_CONTAINER_SIZE = 14; //The max amount of pixels the container can hold 
	private GameObject[] ammoLiquid; //array of liquid objects
	private int currentAmmoFill; //the last ammo checked for;
	// Use this for initialization
	void Start () {
		ammoLiquid = new GameObject[MAX_CONTAINER_SIZE];
		//Calculate percent of current ammo
		currentAmmoFill = ammoBlocks ();
		for (int i=0; i < currentAmmoFill; i++) {
			addLiquid (i);
		}
	}

	private GameObject addLiquid(int i){
		
		ammoLiquid[i] = (GameObject)GameObject.Instantiate(liquidPrefab, new Vector3(transform.position.x-(0.2450f), (transform.position.y-0.120f)+(i*(liquidPrefab.renderer.bounds.size.y*0.3333f)), 0), transform.rotation);
		ammoLiquid[i].transform.parent = transform;

		return ammoLiquid [i];
	}

	//returns percent of current ammo
	private int ammoBlocks(){
		int maxAmmo = transform.parent.parent.GetComponent<CharacterController>().AmmoMax;
		int lastAmmo = transform.parent.parent.GetComponent<CharacterController>().AmmoCurrent;
		int currentAmmoPercent = (maxAmmo / lastAmmo) * 100;

		return (currentAmmoPercent * MAX_CONTAINER_SIZE) / 100;
	}

	// Update is called once per frame
	void Update () {
		int currentAmmo = ammoBlocks ();
		if (currentAmmo > currentAmmoFill) {
			int toAdd=currentAmmo-currentAmmoFill;
			for(int i=0;i < toAdd;i++){
				if(ammoLiquid.Length == 0)
				{
					addLiquid (0);
				}else{
				addLiquid (ammoLiquid.Length-1);
				}
			}
		}else if (currentAmmo < currentAmmoFill) {
			int toTake=currentAmmoFill-currentAmmo;
			for(int i=0;i < toTake;i++){
				if(ammoLiquid.Length != 0)
				{
					Destroy(ammoLiquid[ammoLiquid.Length-1]);
				}
			}
		}

	}
}
