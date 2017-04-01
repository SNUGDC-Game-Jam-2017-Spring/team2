using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class GameManager : MonoBehaviour {

	public static Dictionary<DATA_ACT, bool> havingAct;
	public static Dictionary<DATA_ITEM, bool> havingItem;
	public static Dictionary<DATA_INFO, bool> havingInfo;
	public static Dictionary<int, bool> intBoolDicSwitch;
	public static Type typeButtonSelected;

	public GridItemFiller gridItemFiller;

	public static GameManager singleton;

	void Awake() {
		singleton = this;
		havingAct = new Dictionary<DATA_ACT, bool> ();
		foreach (var kvp in DB.ACT) {
			havingAct.Add (kvp.Value, kvp.Value.isBasic);
		}
		havingItem = new Dictionary<DATA_ITEM, bool> ();
		foreach (var kvp in DB.ITEM) {
			havingItem.Add (kvp.Value, kvp.Value.isBasic);
		}
		havingInfo = new Dictionary<DATA_INFO, bool> ();
		foreach (var kvp in DB.INFO) {
			havingInfo.Add (kvp.Value, false);
		}
		intBoolDicSwitch = new Dictionary<int, bool> ();
		Debug.Log ("DB.ACT : " + DB.ACT.Keys.Count);
		Debug.Log ("DB.ITEM : " + DB.ITEM.Keys.Count);
		Debug.Log ("DB.INFO : " + DB.INFO.Keys.Count);
		Button_Act ();
	}

	List<T> HavingDicToHavingList<T>(Dictionary<T, bool> havingDic) {
		List<T> toReturn = new List<T> ();
		foreach (var kvp in havingDic) {
			if (kvp.Value)
				toReturn.Add (kvp.Key);
		}
		//Debug.Log ("HavingDicToHavingList " + typeof(T) + " toReturn.Count : " + toReturn.Count);
		return toReturn;
	}

	public void Button_Act() {
		
		Button_Common (havingAct);
	}

	public void Button_Item() {

		Button_Common (havingItem);
	}

	public void Button_Info() {
		
		Button_Common (havingInfo);
	}

	void Button_Common<T>(Dictionary<T, bool> havingSomething) where T : DATA {
		if (CombineAndReward.typeToFixGrid == null || CombineAndReward.typeToFixGrid == typeof(T)) {
			typeButtonSelected = typeof(T);
			var toFill = HavingDicToHavingList (havingSomething);
			//Debug.Log (typeof(T).Name + " " + toFill.Count);
			gridItemFiller.Fill (
				toFill.Count, 
				(GameObject go, int i) => {
					go.GetComponent<GridItem> ().data = toFill [i];
				}
			);
		}
	}

}
