using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public class CombineAndReward : MonoBehaviour {

	//public GameManager gameManager;
	//public MainPopUpText mainPopUpText;
	public static DATA dataSelected1;
	public static DATA dataSelected2;

	private static CombineAndReward singleton;

	void Awake() {
		singleton = this;
	}

	private static Type m_TypeToFixGrid = null;
	public static Type typeToFixGrid {
		get { return m_TypeToFixGrid; }
		set { 
			m_TypeToFixGrid = value; 
			if (m_TypeToFixGrid != null) {
				if (m_TypeToFixGrid == typeof(DATA_ACT)) {
					GameManager.singleton.Button_Act ();
					MainPopUpText.singleton.ShowText ("'" + (dataSelected1 as DATA_ITEM).name + "' 아이템으로 어떤 행동을 할 지 선택해주세요");
				} else if (m_TypeToFixGrid == typeof(DATA_ITEM)) {
					GameManager.singleton.Button_Item ();
					MainPopUpText.singleton.ShowText ("'" + (dataSelected1 as DATA_ACT).name + "' 행동을 어떤 아이템으로 할 지 선택해주세요");
				} else if (m_TypeToFixGrid == typeof(DATA_INFO)) {
					GameManager.singleton.Button_Info ();
					MainPopUpText.singleton.ShowText ("'" + (dataSelected1 as DATA_INFO).discription + "'\n이 정보와 조합해 무언가를 알아낼 정보를 선택하세요");
				} else {
					Debug.LogError (m_TypeToFixGrid.Name + " ????");
				}
			}
		}
	}

	public static void SelectFirstItemAndFixGridButton(GridItem gridItem) {
		dataSelected1 = gridItem.data;
		if (gridItem.data is DATA_ACT) {
			if ((gridItem.data as DATA_ACT).isItemNeed == false) {
				CombineReward.AddOrRunIfNotNull(CombineActItem (gridItem.data as DATA_ACT, null));
				return;
			}
		}
		if (gridItem.data is DATA_ACT)
			typeToFixGrid = typeof(DATA_ITEM);
		else if (gridItem.data is DATA_ITEM)
			typeToFixGrid = typeof(DATA_ACT);
		else if (gridItem.data is DATA_INFO)
			typeToFixGrid = typeof(DATA_INFO);
		else
			Debug.LogError ("[Error]" + typeToFixGrid.Name);
	}

	public static void SelectSecondItemAndCombine(GridItem gridItem) {
		dataSelected2 = gridItem.data;
		if (dataSelected1.GetType () == typeof(DATA_ACT) && dataSelected2.GetType () == typeof(DATA_ITEM)) {
			CombineReward.AddOrRunIfNotNull (CombineActItem (dataSelected1 as DATA_ACT, dataSelected2 as DATA_ITEM));
		} else if (dataSelected1.GetType () == typeof(DATA_ITEM) && dataSelected2.GetType () == typeof(DATA_ACT)) {
			CombineReward.AddOrRunIfNotNull (CombineActItem (dataSelected2 as DATA_ACT, dataSelected1 as DATA_ITEM));
		} else if (dataSelected1.GetType () == typeof(DATA_INFO) && dataSelected2.GetType () == typeof(DATA_INFO)) {
			CombineReward.AddOrRunIfNotNull (CombineInfos (dataSelected1 as DATA_INFO, dataSelected2 as DATA_INFO));
		} else {
			Debug.LogError (dataSelected1.GetType().Name +" "+dataSelected2.GetType().Name+ " ????");
		}
	}

	private static CombineReward CombineActItem(DATA_ACT dataAct, DATA_ITEM dataItem) {
		var combineList = 
			(from kvp in DB.ACT_ITEM_COMBINATION
			 where kvp.Value.actID == dataAct.index
				&& ((dataItem == null) ? true : kvp.Value.itemID == dataItem.index)
			 select kvp.Value).ToList ();
		DATA_ACT_ITEM_COMBINATION selected = null;
		for (int i = 0; i < combineList.Count; i++) {
			if (combineList [i].conditionSwitch1ID <= 0) {
				selected = combineList [i];
			} else if (combineList [i].conditionSwitch1State == true) {
				if (GameManager.intBoolDicSwitch.ContainsKey (combineList [i].conditionSwitch1ID)
				    && GameManager.intBoolDicSwitch [combineList [i].conditionSwitch1ID] == true) {
					selected = combineList [i];
				}
			} else if (combineList [i].conditionSwitch1State == false) {
				if (!(GameManager.intBoolDicSwitch.ContainsKey (combineList [i].conditionSwitch1ID)
					&& GameManager.intBoolDicSwitch [combineList [i].conditionSwitch1ID] == true)) {
					selected = combineList [i];
				}
			}
		}
		if (selected == null) {
			SetSelectedNull ();
			return null;
		}
		if (selected.resultSwitchID > 0) {
			if (GameManager.intBoolDicSwitch.ContainsKey (selected.resultSwitchID)) {
				GameManager.intBoolDicSwitch.Add (selected.resultSwitchID, true);
			} else {
				GameManager.intBoolDicSwitch [selected.resultSwitchID] = true;
			}
		}
		SetSelectedNull ();
		return new CombineReward (selected.rewardKind, selected.rewardID, selected.discription, selected.catImgName, selected.playerImgName, selected.fxName);
	}

	private static CombineReward CombineInfos(DATA_INFO dataInfo1, DATA_INFO dataInfo2) {
		Debug.Log ("CombineInfos : " + dataInfo1.index + " " + dataInfo2.index);
		var selected = 
			(from kvp in DB.INFO_COMBINATION
				where (kvp.Value.combInfo1ID == dataInfo1.index
					&& kvp.Value.combInfo2ID == dataInfo2.index)
				|| (kvp.Value.combInfo1ID == dataInfo2.index
					&& kvp.Value.combInfo2ID == dataInfo1.index)
			 select kvp.Value).FirstOrDefault ();
		if (selected == null) {
			SetSelectedNull ();
			return null;
		}
		SetSelectedNull ();
		return new CombineReward (selected.rewardKind, selected.rewardID, selected.discription, null, null, null);
	}

	private static void SetSelectedNull() {
		if (dataSelected1.GetType () == typeof(DATA_ACT)) {
			GameManager.singleton.Button_Act ();
		} else if (dataSelected1.GetType () == typeof(DATA_ITEM)) {
			GameManager.singleton.Button_Item ();
		}
		dataSelected1 = null;
		dataSelected2 = null;
		m_TypeToFixGrid = null;
		MainPopUpText.singleton.ShowNothing ();
	}

}

public class CombineReward {

	public int rewardKind;
	public int rewardID;
	public string frontAddText;
	public string catImgSpriteName;
	public string playerImgSpriteName;
	public string fxName;


	public CombineReward(int rewardKind, int rewardID, string frontAddText, string catImgSpriteName, string playerImgSpriteName, string fxName) {
		this.rewardKind = rewardKind;
		this.rewardID = rewardID;
		this.frontAddText = frontAddText;
		this.catImgSpriteName = catImgSpriteName;
		this.playerImgSpriteName = playerImgSpriteName;
		this.fxName = fxName;
	}

	public void AddOrRun() {
		if (rewardKind != 4) {
			if (rewardID > 0) {
				if (rewardKind == 1) {
					MainPopUpText.singleton.ShowText (frontAddText
					+ ((GameManager.havingAct [DB.ACT [rewardID]] == false) ? "\n(새 행동 '" + DB.ACT [rewardID].name + "' 획득)" : ""), 5f);
					GameManager.havingAct [DB.ACT [rewardID]] = true;
					GameManager.singleton.Button_Act ();
				} else if (rewardKind == 2) {
					MainPopUpText.singleton.ShowText (frontAddText
					+ ((GameManager.havingItem [DB.ITEM [rewardID]] == false) ? "\n(새 아이템 '" + DB.ITEM [rewardID].name + "' 획득)" : ""), 5f);
					GameManager.havingItem [DB.ITEM [rewardID]] = true;
					GameManager.singleton.Button_Item ();
				} else if (rewardKind == 3) {
					MainPopUpText.singleton.ShowText (frontAddText
					+ ((GameManager.havingInfo [DB.INFO [rewardID]] == false) ? "\n(새 정보 '" + DB.INFO [rewardID].discription + "' 획득)" : ""), 5f);
					GameManager.havingInfo [DB.INFO [rewardID]] = true;
					GameManager.singleton.Button_Info ();
				} else {
					MainPopUpText.singleton.ShowText ("[미구현]rewardKind : " + rewardKind + ", rewardID" + rewardID, 5f);
				}
			} else {
				if (string.IsNullOrEmpty (frontAddText)) {
					MainPopUpText.singleton.ShowText ("아무런 일도 일어나지 않았다.", 5f);
				} else {
					MainPopUpText.singleton.ShowText (frontAddText, 5f);
				}
			}
		} else {
			if (GameManager.havingInfo [DB.INFO [6]]) {
				EndingPlayer.Play (null, 2f, 1);
			} else {
				EndingPlayer.Play (null, 2f, 2);
			}
		}
		if (string.IsNullOrEmpty (playerImgSpriteName)) {
			CatImage.ApplyImg (catImgSpriteName);
		} else {
			CatImage.ApplyImgWithDelay (catImgSpriteName, 0.75f);
		}
		PlayerImg.ApplyImg (playerImgSpriteName);
		if (!string.IsNullOrEmpty(fxName)) {
			//Debug.Log()
			var clip = Resources.Load<AudioClip> ("Sound/" + fxName);
			Debug.Log ("AudioClip : " + clip + " " + ("Sound/" + fxName));
			GameManager.singleton.GetComponent<AudioSource>().clip = clip;
			GameManager.singleton.GetComponent<AudioSource> ().Play ();
			GameManager.singleton.GetBgmLow();
		}
		
		
		/*
		if (rewardID == 0) {
			MainPopUpText.singleton.ShowText (frontAddText, 5f);
			CatImage.ApplyImg (catImgSpriteName);
			PlayerImg.ApplyImg (playerImgSpriteName);
		}else if (rewardKind == 1) {
			GameManager.havingAct [DB.ACT [rewardID]] = true;
			GameManager.singleton.Button_Act ();
			MainPopUpText.singleton.ShowText (frontAddText + "\n(새 행동 '" + DB.ACT [rewardID].name + "' 획득)", 5f);
			CatImage.ApplyImg (catImgSpriteName);
		} else if (rewardKind == 2) {
			GameManager.havingItem [DB.ITEM [rewardID]] = true;
			GameManager.singleton.Button_Item ();
			MainPopUpText.singleton.ShowText (frontAddText + "\n(새 아이템 '" + DB.ITEM [rewardID].name + "' 획득)", 5f);
			CatImage.ApplyImg (catImgSpriteName);
		} else if (rewardKind == 3) {
			GameManager.havingInfo [DB.INFO [rewardID]] = true;
			GameManager.singleton.Button_Info ();
			MainPopUpText.singleton.ShowText (frontAddText + "\n(새 정보 '" + DB.INFO [rewardID].discription + "' 획득)", 5f);
			CatImage.ApplyImg (catImgSpriteName);
		} else {
			MainPopUpText.singleton.ShowText ("[미구현]rewardKind : "+rewardKind+", rewardID"+rewardID, 5f);
		}
		*/
	}

	public static void AddOrRunIfNotNull(CombineReward cr) {
		//string disText = 
		if (cr == null) {
			MainPopUpText.singleton.ShowText ("아무런 일도 일어나지 않았다.", 5f);
		} else {
			cr.AddOrRun ();
		}
	}
	

}





























