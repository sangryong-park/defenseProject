using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class test : MonoBehaviour {

	public int _exp = 0;

	void Start () {
		//List<Dictionary<string,object>> data = CSVReader.Read("데이터테이블1");


        /*
		for(var i=0; i< data.Count; i++){
			Debug.Log("index " + (i).ToString() + " : " + data[i]["number"] + " " + data[i]["rating"] + " " + data[i]["synergy1"]);
		}

		_exp = (int)data[1]["number"];

        */
		Debug.Log(_exp);
	}
}
