using UnityEngine;
using System.Collections;

public enum LaserType {
	FIRE,
	FORCE
}

public class Inventory : MonoBehaviour {

	public GameObject BottomBar;

	private LaserType [] laserInventory;
	private int selectedLaser = -1;

	private void SelectorToLeft() {
		if (selectedLaser == 0) {
			selectedLaser = laserInventory.Length - 1;
		} else {
			selectedLaser--;
		}
	}

	private void SelectorToRight() {
		if (selectedLaser == laserInventory.Length - 1) {
			selectedLaser = 0;
		} else {
			selectedLaser++;
		}
	}


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		float wheel;
		if ((wheel = Input.GetAxis ("Mouse ScrollWheel")) != 0) {
			if (wheel < 0) {
				SelectorToLeft ();
			} else {
				SelectorToRight ();
			}
		}
	}
}
