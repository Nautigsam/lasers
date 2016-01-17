using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public enum LaserType {
	None,
	Water,
	Force
}

public class Inventory : MonoBehaviour {

	public Dictionary<LaserType, Color> laserColors = new Dictionary<LaserType, Color> () {
		{ LaserType.None, new Color (0f, 0f, 0f, 0f) },
		{ LaserType.Water, new Color (0f, 0f, 1f, 0.5f) },
		{ LaserType.Force, new Color (0.4f, 0.3f, 0.3f, 0.6f) }
	};

	public GameObject inventoryModalPanel;
	public GameObject equipmentBarPanel;
	public GameObject inventoryItemPrefab;

	private Transform contentPanelTransform;
	private Button closeButton;
	private UnityAction closeAction;

	private void CloseActionMethod () {
		inventoryModalPanel.gameObject.SetActive (false);
		closeButton.onClick.RemoveListener (closeAction);
		foreach (InventoryItem contentItem in contentPanelTransform.GetComponentsInChildren<InventoryItem> ()) {
			contentItem.onLeftClick -= inventoryItemLeftClickAction;
			contentItem.onRightClick -= inventoryItemRightClickAction;
		}
	}

	private InventoryItemClickAction inventoryItemLeftClickAction;
	private void InventoryItemLeftClickMethod (LaserType type) {
		equipmentBarPanel.GetComponent<EquipmentBar> ().ChangeHand (true, type, laserColors);
	}

	private InventoryItemClickAction inventoryItemRightClickAction;
	private void InventoryItemRightClickMethod (LaserType type) {
		equipmentBarPanel.GetComponent<EquipmentBar> ().ChangeHand (false, type, laserColors);
	}

	// Use this for initialization
	void Start () {
		contentPanelTransform = inventoryModalPanel.transform.Find ("InventoryModalPanel/InventoryModalContentPanel");

		foreach (KeyValuePair<LaserType, Color> laser in laserColors) {
			if (laser.Key != LaserType.None) {
				GameObject item = Instantiate (inventoryItemPrefab);
				item.GetComponent<InventoryItem> ().type = laser.Key;
				item.GetComponent<Image> ().color = laser.Value;
				item.transform.SetParent (contentPanelTransform, false);
			}
		}

		foreach (InventoryItem item in contentPanelTransform.GetComponentsInChildren<InventoryItem> ()) {
			item.GetComponent<Image> ().color = laserColors [item.type];
		}

		closeButton = inventoryModalPanel.transform.Find ("InventoryModalPanel/InventoryModalCloseButtonPanel/InventoryModalCloseButton").GetComponent<Button> ();
		closeAction = new UnityAction (CloseActionMethod);
		inventoryItemLeftClickAction = InventoryItemLeftClickMethod;
		inventoryItemRightClickAction = InventoryItemRightClickMethod;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.E)) {
			if (!inventoryModalPanel.gameObject.activeSelf) {
				closeButton.onClick.AddListener (closeAction);
				foreach (InventoryItem contentItem in contentPanelTransform.GetComponentsInChildren<InventoryItem> ()) {
					contentItem.onLeftClick += inventoryItemLeftClickAction;
					contentItem.onRightClick += inventoryItemRightClickAction;
				}
				inventoryModalPanel.gameObject.SetActive (true);
			}
		}
		if (Input.GetKey (KeyCode.Escape)) {
			if (inventoryModalPanel.gameObject.activeSelf) {
				closeAction.Invoke ();
			}
		}
	}
}
