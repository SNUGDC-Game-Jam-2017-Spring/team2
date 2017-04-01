using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainPopUpText : MonoBehaviour {

	public float remainTime;

	public static MainPopUpText singleton;

	void Awake() {
		singleton = this;
		ShowNothing ();
	}

	public void ShowText(string text, float showTime = float.PositiveInfinity) {
		StopAllCoroutines ();
		GetComponent<Text> ().text = text;
		remainTime = showTime;
		StartCoroutine (ShowText_Cor ());
	}

	public void ShowNothing() {
		GetComponent<Text> ().text = "";
	}

	IEnumerator ShowText_Cor() {
		yield return new WaitForFixedUpdate ();
		while (true) {
			remainTime -= Time.fixedDeltaTime;
			if (remainTime <= 0) {
				ShowNothing ();
				break;
			}
			yield return new WaitForFixedUpdate ();
		}
	}

}
























