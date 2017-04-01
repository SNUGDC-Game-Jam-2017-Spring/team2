using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class CSVParser {

	public static Dictionary<int, T> FindCSVAndParse<T>() where T : DATA
	{
		return ParseCSVText<T>(LoadCSVTextFromResources (typeof(T).Name.Replace ("DATA_", "")));
	}

	private static string LoadCSVTextFromResources(string fileName) { //ByFileName
		TextAsset txtFile = (TextAsset)Resources.Load("CSV/"+fileName) as TextAsset;
		return txtFile.text;
	}

	private static Dictionary<int, T> ParseCSVText<T>(string csvText) where T : DATA {
		Dictionary<int, T> toReturn = new Dictionary<int, T> ();
		var strListParsedCSVLine = ParseCSVTextToLines (csvText);
		for (int i = 0; i < strListParsedCSVLine.Count; i++) {
			var toAdd = Activator.CreateInstance(typeof(T), ParseCSVLineToItems (strListParsedCSVLine [i])) as T;
			toReturn.Add (toAdd.index, toAdd);
		}
		return toReturn;
	}

	private static List<string> ParseCSVTextToLines(string csvText) {
		string[] csvTextSplited = csvText.Split ('\n');
		List<string> toReturn = new List<string> ();
		for (int i = 2; i < csvTextSplited.Length; i++) {
			toReturn.Add (csvTextSplited [i]);
		}
		//Debug.Log ("ParseCSVTextToLines toReturn : " + toReturn.Count);
		return toReturn;
	}

	private static List<string> ParseCSVLineToItems(string csvLine) {
		string[] csvLineSplited = csvLine.Split (',');
		List<string> toReturn = new List<string> ();
		toReturn.AddRange (csvLineSplited);
		return toReturn;
	}


}


