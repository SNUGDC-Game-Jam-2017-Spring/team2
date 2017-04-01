using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class GridItemFiller : MonoBehaviour {

	public GameObject toCopy;
	public List<GameObject> gridItemList;

	public void Fill(int amount, Action<GameObject, int> fillAction) {
		if (gridItemList == null)
			gridItemList = new List<GameObject> ();
		if (toCopy.activeSelf)
			toCopy.SetActive (false);
		while (gridItemList.Count > 0) {
			Destroy (gridItemList [0]);
			gridItemList.RemoveAt (0);
		}
		for (int i = 0; i < amount; i++) {
			var copied = Instantiate (toCopy);
			copied.transform.SetParent (this.transform);
			gridItemList.Add (copied);
			if (fillAction != null) fillAction (copied, i);
			copied.SetActive (true);
		}
	}

}























