using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DB : MonoBehaviour {

	/*
	private static DataManager m_Singleton = null;
	public static DataManager singleton {
		get {
			return m_Singleton;
		}
	}*/

	private static Dictionary<int, DATA_ACT> m_ACT;
	public static Dictionary<int, DATA_ACT> ACT { get{ FillDataIfRequired (); return m_ACT; }}
	private static Dictionary<int, DATA_ITEM> m_ITEM;
	public static Dictionary<int, DATA_ITEM> ITEM { get{ FillDataIfRequired (); return m_ITEM; }}
	private static Dictionary<int, DATA_INFO> m_INFO;
	public static Dictionary<int, DATA_INFO> INFO { get{ FillDataIfRequired (); return m_INFO; }}
	private static Dictionary<int, DATA_ACT_ITEM_COMBINATION> m_ACT_ITEM_COMBINATION;
	public static Dictionary<int, DATA_ACT_ITEM_COMBINATION> ACT_ITEM_COMBINATION { get{ FillDataIfRequired (); return m_ACT_ITEM_COMBINATION; }}
	private static Dictionary<int, DATA_INFO_COMBINATION> m_INFO_COMBINATION;
	public static Dictionary<int, DATA_INFO_COMBINATION> INFO_COMBINATION { get{ FillDataIfRequired (); return m_INFO_COMBINATION; }}
	private static Dictionary<int, DATA_ENDING> m_INFO_ENDING;
	public static Dictionary<int, DATA_ENDING> INFO_ENDING { get{ FillDataIfRequired (); return m_INFO_ENDING; }}

	private static void FillDataIfRequired() {
		if (m_ACT == null) {
			m_ACT = CSVParser.FindCSVAndParse<DATA_ACT> ();
			m_ITEM = CSVParser.FindCSVAndParse<DATA_ITEM> ();
			m_INFO = CSVParser.FindCSVAndParse<DATA_INFO> ();
			m_ACT_ITEM_COMBINATION = CSVParser.FindCSVAndParse<DATA_ACT_ITEM_COMBINATION> ();
			m_INFO_COMBINATION = CSVParser.FindCSVAndParse<DATA_INFO_COMBINATION> ();
			m_INFO_ENDING = CSVParser.FindCSVAndParse<DATA_ENDING> ();
		}
	}

}
