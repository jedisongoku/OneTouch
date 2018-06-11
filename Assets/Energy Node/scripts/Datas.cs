using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Datas : Singleton<Datas> {

		private TextAsset datas;
		private Dictionary<string, Dictionary<string, string>> data;
	void Start () {
				
	}

		public string[] getData(){
				datas = Resources.Load<TextAsset> ("datas/datas");
				string[] lines = new string[0];
				data = new Dictionary<string, Dictionary<string, string>>();
				Dictionary<string, string> loc = new Dictionary<string, string> ();
				lines = datas.text.Split ('\n');

				return lines;
		}
	
	// Update is called once per frame
	void Update () {
		
	}
}
