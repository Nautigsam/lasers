using UnityEngine;
using System.Collections;

public delegate void InventoryItemClickAction (LaserType type);

public class InventoryItem : MonoBehaviour {

	public LaserType type;

	public InventoryItemClickAction onLeftClick;
	public InventoryItemClickAction onRightClick;

	public void OnClick () {
		if (Input.GetMouseButton (0)) {
			onLeftClick (type);
		} else if (Input.GetMouseButton (1)) {
			onRightClick (type);
		}
	}

	// Use this for initialization
	void Start () {
		
	}
}
