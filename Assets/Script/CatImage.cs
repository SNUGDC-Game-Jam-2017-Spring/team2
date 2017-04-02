using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CatImage : MonoBehaviour {

	public static CatImage singleton;

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

	public static void ApplyImgWithDelay(string imgName, float delay) {
		singleton.StopAllCoroutines ();
		singleton.StartCoroutine(singleton.ApplyImgWithDelay_Cor (imgName, delay));
	}

	IEnumerator ApplyImgWithDelay_Cor(string imgName, float delay) {
		yield return new WaitForSeconds (delay);
		ApplyImg (imgName);
	}

}




















