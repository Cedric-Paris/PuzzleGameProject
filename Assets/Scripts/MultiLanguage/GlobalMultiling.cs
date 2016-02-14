using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GlobalMultiling {
	
	private JsonReader jsonReader;
	public static string CurrentLanguage
	{
		get {return mCurrentLanguage;}
		private set {mCurrentLanguage = value;}
	}
	private static string mCurrentLanguage;

	public GlobalMultiling()
	{
		jsonReader = new JsonReader();
		if(CurrentLanguage == null)
			CheckLanguage();
	}
	

	public string getTranslatedValue(string textValue)
	{
		string[] splitValue = textValue.Split("/".ToCharArray(), 2);
		try
		{
			if(splitValue[0] == "@+")
				return jsonReader.ReadValue(splitValue[1]);
		}
		catch
		{
			return textValue;
		}
		return textValue;
	}


	public void translateAll()
	{
		Text[] tabText = GameObject.FindObjectsOfType<Text>();
		foreach(Text t in tabText)
			translate(t);
	}

	public void translateForGameObject(GameObject gameObject)
	{
		foreach(Text t in gameObject.GetComponentsInChildren<Text>())
			translate(t);

	}


	private void translate(Text t)
	{
		t.text = getTranslatedValue(t.text);
	}

	private void CheckLanguage() {
		switch (Application.systemLanguage)
		{
			case SystemLanguage.French:
									CurrentLanguage="fr";
									break;
			default:
									CurrentLanguage="en";
									break;
		}
	}

}
