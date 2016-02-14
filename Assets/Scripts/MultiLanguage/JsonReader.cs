using UnityEngine;
using System.Collections;
using SimpleJSON;

public class JsonReader {


	private JSONNode _json;
	private Object _jsonFile;


	public string ReadValue(string key) {
		if (_json == null)
			GetJsonFile ();
		if (_json == null || _json [key] == null) {
			return "UNKNOW";
		}
		return _json[key].Value;
	}

	private void GetJsonFile() {
		string path = string.Format("Languages/strings_{0}",GlobalMultiling.CurrentLanguage);
		_jsonFile = Resources.Load (path, typeof(object));
		_json = JSON.Parse (_jsonFile.ToString());
	}

}
