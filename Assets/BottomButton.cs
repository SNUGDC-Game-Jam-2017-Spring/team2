using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class BottomButton : MonoBehaviour {

	public int typeKind;
	public Type type;

	void Awake() {
		if (typeKind == 0)
			type = typeof(DATA_ACT);
		else if (typeKind == 1)
			type = typeof(DATA_ITEM);
		else if (typeKind == 2)
			type = typeof(DATA_INFO);
	}

	void Update() {
		if (CombineAndReward.typeToFixGrid == null) {
			if (GameManager.typeButtonSelected == type) {
				GetComponent<Image> ().color = Color.yellow/2f + Color.white/2f;
			} else {
				GetComponent<Image> ().color = Color.white;
			}
		} else {
			if (CombineAndReward.typeToFixGrid == type) {
				GetComponent<Image> ().color = Color.yellow;
			} else {
				GetComponent<Image> ().color = Color.gray;
			}
		}
	}

}
