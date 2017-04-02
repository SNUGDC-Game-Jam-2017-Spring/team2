using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CatPowerGageImage : MonoBehaviour {

	public static CatPowerGageImage singleton;

	void Awake() {
		singleton = this;
	}

	public static void ApplyImg(string imgName) {
		if (!string.IsNullOrEmpty (imgName)) {
			Sprite sprite = Resources.Load<Sprite> ("Png/" + imgName);
			if (sprite == null) {
				singleton.GetComponent<Image> ().sprite = null;
			} else {
				singleton.GetComponent<Image> ().sprite = sprite;
			}
		}
	}

}




















