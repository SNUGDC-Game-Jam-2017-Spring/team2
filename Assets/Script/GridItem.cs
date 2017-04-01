using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GridItem : MonoBehaviour {

	private DATA m_Data;
	public DATA data {
		set { 
			m_Data = value;
			if (m_Data is DATA_ACT) {
				GetComponentInChildren<Text> ().text = (m_Data as DATA_ACT).name;
			}else if (m_Data is DATA_ITEM) {
				GetComponentInChildren<Text> ().text = (m_Data as DATA_ITEM).name;
			}else if (m_Data is DATA_INFO) {
				GetComponentInChildren<Text> ().text = (m_Data as DATA_INFO).discription;
			}
			//GetComponentInChildren<Text>().text = 
		}
		get { return m_Data; }
	}

	public void Click() {
		Debug.Log ("GridItem Click " + m_Data.GetType ().Name + " " + m_Data.index);
		if (CombineAndReward.dataSelected1 == null) {
			CombineAndReward.SelectFirstItemAndFixGridButton (this);
		} else {
			CombineAndReward.SelectSecondItemAndCombine (this);
		}
	}

}
