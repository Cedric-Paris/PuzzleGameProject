using UnityEngine;
using System.Collections;
using SimpleJSON;

public class JsonReader : MonoBehaviour {
	//Region attribut
	private JSONNode _json;
	private object _jsonFile;
	//Fin Region

	//Region Unity Method
	void Start () {
		GlobalMultiling.jsonReader = this;
	}
	//Fin Region

	//Region Method
	public string ReadValue(string key) {
		if (_json == null)
			GetJsonFile ();
		print (_json.ToString ());
		if (_json == null || _json [key] == null) {
			return "UNKNOW";
		}
		return _json[key].Value;
	}

	private void GetJsonFile() {
		string path = string.Format("strings_{0}",GlobalMultiling.currentLanguage);
		_jsonFile = Resources.Load (path, typeof(object));
		_json = JSONNode.Parse (_jsonFile.ToString ());
			//JSON.Parse (_jsonFile.ToString());



		print (_jsonFile.ToString());
		print (_json);

	}
	//Fin Region
}
