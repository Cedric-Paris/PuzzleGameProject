using UnityEngine;
using System.Collections;

public class GlobalMultiling : MonoBehaviour {
	//Region attribut
	public static JsonReader jsonReader;
	public static string currentLanguage;
	//Fin Region

	//Region Unity Method
	void Start() {
		CheckLanguage ();
		string value = jsonReader.ReadValue ("test");
		print (value);
	}
	//Fin Region

	//Region Method
	static void CheckLanguage() {
		switch (Application.systemLanguage) {
		case SystemLanguage.French: currentLanguage="fr"; break;
		case SystemLanguage.English: currentLanguage="en"; break;
		}
	}
	//Fin Region
}
