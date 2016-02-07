using UnityEngine;
using System.Collections;

public class GlobalMultiling : MonoBehaviour {

	public static JsonReader jsonReader;
	public static string currentLanguage;

	void Start()
	{
		CheckLanguage ();
		string value = jsonReader.ReadValue ("test");
		Debug.Log(value);
	}


	static void CheckLanguage() {
		switch (Application.systemLanguage)
		{
			case SystemLanguage.French:
									currentLanguage="fr";
									break;
			case SystemLanguage.English:
									currentLanguage="en";
									break;
		}
	}

}
