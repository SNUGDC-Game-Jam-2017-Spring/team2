using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerImg : MonoBehaviour {

	public static PlayerImg singleton;

	void Awake() {
		singleton = this;
		GetComponent<Animation> ().Stop ();
	}

	public static void ApplyImg(string imgName) {
		//singleton.GetComponent<Animation> ().Rewind ();
		singleton.GetComponent<Animation> ().Stop ();
		singleton.GetComponent<RectTransform> ().anchoredPosition = Vector2.zero;
		if (!string.IsNullOrEmpty (imgName)) {
			Sprite sprite = Resources.Load<Sprite> ("Png/" + imgName);
			if (sprite == null) {
				singleton.GetComponent<Image> ().sprite = null;
			} else {
				singleton.GetComponent<Image> ().sprite = sprite;
			}
			singleton.GetComponent<Animation> ().Play ("animation00");
		}
	}

}
