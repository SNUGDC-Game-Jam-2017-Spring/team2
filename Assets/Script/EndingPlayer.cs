using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public interface IEndingReceiver {

	void EndingEnd();

}

public class EndingPlayer : MonoBehaviour {

	public static EndingPlayer singleton;
	bool isPlaying = false;
	int curTextID = 0;
	public List<GameObject> toHideList;
	bool clickedLastFrame = false;

	void Awake() {
		singleton = this;
		gameObject.SetActive (false);
	}

	public static void Play(IEndingReceiver caller, float afterSecond, int endingGroudID) {
		if (!singleton.isPlaying) {
			for (int i = 0; i < singleton.toHideList.Count; i++) {
				singleton.toHideList [i].SetActive (false);
			}
			singleton.gameObject.SetActive (true);
			singleton.StartCoroutine(singleton.endingCor (caller, afterSecond, endingGroudID));
		}
	}

	IEnumerator endingCor(IEndingReceiver caller, float afterSecond, int endingGroudID) {		
			isPlaying = true;
			var endingList = 
				(from kvp in DB.INFO_ENDING
					where kvp.Value.groupID == endingGroudID
					select kvp.Value).ToList();	
		//Debug.Log ("asdfasdf1");
			yield return new WaitForSeconds (afterSecond);
		//Debug.Log ("asdfasdf2");
		transform.GetChild(0).GetComponent<Text> ().text = "";
			float remainTime = 2f;
			while (remainTime > 0) {
				GetComponent<Image> ().color = new Color (1f, 1f, 1f, 1f - remainTime / 2f);
				yield return new WaitForFixedUpdate ();
				remainTime -= Time.fixedDeltaTime;
			}
			while (curTextID < endingList.Count) {
			if (string.IsNullOrEmpty (endingList [curTextID].imgName)) {
				transform.GetChild(0).GetComponent<Text> ().text = endingList [curTextID].script;
				transform.GetChild (1).GetComponent<Text> ().text = "";
				transform.GetChild (2).GetComponent<Image> ().sprite = null;
				transform.GetChild (2).GetComponent<Image> ().color = new Color (1, 1, 1, 0);
			} else {
				transform.GetChild(0).GetComponent<Text> ().text = "";
				transform.GetChild (1).GetComponent<Text> ().text = endingList [curTextID].script;
				transform.GetChild (2).GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Png/"+endingList [curTextID].imgName);
				transform.GetChild (2).GetComponent<Image> ().color = new Color (1, 1, 1, 1);
			}				
				//yield return new WaitForFixedUpdate ();
			yield return singleton.StartCoroutine(waitForClick());
			}
			isPlaying = false;
			if (caller != null) caller.EndingEnd ();
			//
			for (int i = 0; i < toHideList.Count; i++) {
				toHideList[i].SetActive(true);
			}
		CatImage.ApplyImg ("Basic_Cat");
			gameObject.SetActive (false);
	}


	IEnumerator waitForClick() {
		while (true) {
			if (clickedLastFrame) {
				clickedLastFrame = false;
				break;
			}
			yield return new WaitForFixedUpdate ();
		}
	}

	public void Click() {
		curTextID++;
		clickedLastFrame = true;
	}

}
