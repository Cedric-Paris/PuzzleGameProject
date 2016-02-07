using UnityEngine;
using System.Collections;
using SimpleJSON;

public class JsonReader : MonoBehaviour {


	private JSONNode _json;
	private Object _jsonFile;

	//Utilisé pour l'initialisation des variables (appelé avant tous les start())
	void Awake()
	{
		GlobalMultiling.jsonReader = this;
	}


	public string ReadValue(string key) {
		if (_json == null)
			GetJsonFile ();
		if (_json == null || _json [key] == null) {
			return "UNKNOW";
		}
		return _json[key].Value;
	}

	private void GetJsonFile() {
		string path = string.Format("Languages/strings_{0}",GlobalMultiling.currentLanguage);
		_jsonFile = Resources.Load (path, typeof(object));
		_json = JSON.Parse (_jsonFile.ToString());
	}

}
