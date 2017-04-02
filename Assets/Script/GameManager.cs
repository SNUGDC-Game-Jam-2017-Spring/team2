using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

<<<<<<< HEAD
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
		Debug.Log ("public void Button_Act() {");
		Button_Common (havingAct);
	}

	public void Button_Item() {
		Debug.Log ("public void Button_Item() {");
		Button_Common (havingItem);
	}

	public void Button_Info() {
		Debug.Log ("public void Button_Info() {");
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
=======
public class GameManager : MonoBehaviour
{

    public static Dictionary<DATA_ACT, bool> havingAct;
    public static Dictionary<DATA_ITEM, bool> havingItem;
    public static Dictionary<DATA_INFO, bool> havingInfo;
    public static Dictionary<int, bool> intBoolDicSwitch;
    public static Type typeButtonSelected;

    public GridItemFiller gridItemFiller;

    public static GameManager singleton;

    void Awake()
    {
        singleton = this;
        havingAct = new Dictionary<DATA_ACT, bool>();
        foreach (var kvp in DB.ACT)
        {
            havingAct.Add(kvp.Value, kvp.Value.isBasic);
        }
        havingItem = new Dictionary<DATA_ITEM, bool>();
        foreach (var kvp in DB.ITEM)
        {
            havingItem.Add(kvp.Value, kvp.Value.isBasic);
        }
        havingInfo = new Dictionary<DATA_INFO, bool>();
        foreach (var kvp in DB.INFO)
        {
            havingInfo.Add(kvp.Value, false);
        }
        intBoolDicSwitch = new Dictionary<int, bool>();
        Debug.Log("DB.ACT : " + DB.ACT.Keys.Count);
        Debug.Log("DB.ITEM : " + DB.ITEM.Keys.Count);
        Debug.Log("DB.INFO : " + DB.INFO.Keys.Count);
        Button_Act();
    }

    List<T> HavingDicToHavingList<T>(Dictionary<T, bool> havingDic)
    {
        List<T> toReturn = new List<T>();
        foreach (var kvp in havingDic)
        {
            if (kvp.Value)
                toReturn.Add(kvp.Key);
        }
        //Debug.Log ("HavingDicToHavingList " + typeof(T) + " toReturn.Count : " + toReturn.Count);
        return toReturn;
    }

    public void Button_Act()
    {

        Button_Common(havingAct);
    }

    public void Button_Item()
    {

        Button_Common(havingItem);
    }

    public void Button_Info()
    {

        Button_Common(havingInfo);
    }

    void Button_Common<T>(Dictionary<T, bool> havingSomething) where T : DATA
    {
        if (CombineAndReward.typeToFixGrid == null || CombineAndReward.typeToFixGrid == typeof(T))
        {
            typeButtonSelected = typeof(T);
            var toFill = HavingDicToHavingList(havingSomething);
            //Debug.Log (typeof(T).Name + " " + toFill.Count);
            gridItemFiller.Fill(
                toFill.Count,
                (GameObject go, int i) =>
                {
                    go.GetComponent<GridItem>().data = toFill[i];
                }
            );
        }
    }

    public void GetBgmLow()
    {
        AudioSource bgm = GameObject.FindWithTag("MainCamera").GetComponent<AudioSource>();
        AudioSource fx = GetComponent<AudioSource>();
        if (bgm.isPlaying && fx.isPlaying)
        {
			StopAllCoroutines();
            StartCoroutine(GetVolumeTrick(bgm, fx));
        }
    }
    IEnumerator GetVolumeTrick(AudioSource bgm, AudioSource fx)
    {
		IEnumerator high = GetBgmToVolume(bgm, 1.0f);
		IEnumerator low = GetBgmToVolume(bgm, 0.05f);
		
		StopCoroutine(high);
        StartCoroutine(low);
        yield return new WaitForSeconds(fx.clip.length + 0.5f);
		StopCoroutine(low);
        StartCoroutine(high);
    }

    IEnumerator GetBgmToVolume(AudioSource bgm, float to)
    {
        while (true)
        {
            if (bgm.volume > to + Mathf.Epsilon)
            {
                bgm.volume /= 1.3f;
            }
            else if (bgm.volume < to - Mathf.Epsilon)
            {
                bgm.volume *= 1.3f;
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
>>>>>>> origin/master

}
