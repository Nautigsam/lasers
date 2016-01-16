using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class EquipmentBar : MonoBehaviour {

	private LaserType leftHand;
	public LaserType LeftHand { get { return leftHand; } }

	private LaserType rightHand;
	public LaserType RightHand { get { return rightHand; } }

	private void ChangeImage (Image image, Color color) {
		image.color = color;
	}

	public void ChangeHand (bool isLeftHand, LaserType newType, Dictionary<LaserType, Color> colors) {
		LaserType oldType = (isLeftHand ? leftHand : rightHand);
		if (newType != oldType) {
			Transform handTransform = transform.Find (isLeftHand ? "LeftHand" : "RightHand");
			ChangeImage (handTransform.GetComponentInChildren<Image> (), colors [newType]);

			if (isLeftHand) {
				if (rightHand == newType) {
					ChangeImage (transform.Find ("RightHand").GetComponentInChildren<Image> (), colors [oldType]);
					rightHand = oldType;
				}
				leftHand = newType;
			} else {
				if (leftHand == newType) {
					ChangeImage (transform.Find ("LeftHand").GetComponentInChildren<Image> (), colors [oldType]);
					leftHand = oldType;
				}
				rightHand = newType;
			}
		}
	}

	// Use this for initialization
	void Start () {
		leftHand = LaserType.None;
		leftHand = LaserType.None;
	}
}
