using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

/// <summary>
/// Provides methods to display message boxes for the user.
/// </summary>
public class UIMessageBox : MonoBehaviour {

	private static Canvas initializeCanvas(GameObject gameObject)
	{
		Canvas canvas = gameObject.AddComponent<Canvas>();
		gameObject.AddComponent<CanvasScaler>();
		gameObject.AddComponent<GraphicRaycaster>();
		canvas.renderMode = RenderMode.ScreenSpaceCamera;
		canvas.worldCamera = Camera.main;
		canvas.sortingOrder = 51;
		return canvas;
	}

	public static void ShowMessage(string message)
	{
		GameObject g = new GameObject("CanvasMessageBox");
		Canvas canvas = initializeCanvas(g);
		GameObject messageBox = (GameObject)Resources.Load<GameObject>("UITools/YesNoMessageBox");
		messageBox = Instantiate(messageBox);
		messageBox.transform.SetParent(canvas.transform, false);
		messageBox.transform.name = "MessageBox";
		Text textMessage = messageBox.GetComponentInChildren<Text>();
		textMessage.text = message;
		Button[] b = messageBox.GetComponentsInChildren<Button>();
		b[0].onClick.AddListener( ()=> {Destroy(g);});
		b[0].GetComponentInChildren<Text>().text = "OK";
		Destroy(b[1].gameObject);
	}

	/// <summary>
	/// Displays a message box with Yes and No buttons.
	/// Performs treatments passed as parameters according to user response.
	/// </summary>
	/// <param name="message">The message in the message box.</param>
	/// <param name="actionIfYes">Action performed if the response is yes.</param>
	/// <param name="actionIfNo">Action performed if the response is no.</param>
	public static void ShowYesNo(string message, UnityEngine.Events.UnityAction actionIfYes, UnityEngine.Events.UnityAction actionIfNo)
	{
		GameObject g = new GameObject("CanvasMessageBox");
		Canvas canvas = initializeCanvas(g);
		GameObject messageBox = (GameObject)Resources.Load<GameObject>("UITools/YesNoMessageBox");
		messageBox = Instantiate(messageBox);
		messageBox.transform.SetParent(canvas.transform, false);
		messageBox.transform.name = "YesNoMessageBox";
		Text textMessage = messageBox.GetComponentInChildren<Text>();
		textMessage.text = message;
		Button[] b = messageBox.GetComponentsInChildren<Button>();
		b[0].onClick.AddListener( ()=> {Destroy(g);});
		b[1].onClick.AddListener( ()=> {Destroy(g);});
		if(b[0].name == "ButtonYes")
		{
			b[0].onClick.AddListener( () => {
				actionIfYes();
			});
			b[1].onClick.AddListener( () => {
				actionIfNo();
			});
		}
		else
		{
			b[0].onClick.AddListener( () => {
				actionIfNo();
			});
			b[1].onClick.AddListener( () => {
				actionIfYes();
			});
		}
	}

	public delegate void ShowEditTextCallback(string userText);
	public static void ShowEditText(string placeHolderMessage, ShowEditTextCallback callbackFonctionIfClickOk)
	{
		GameObject g = new GameObject("CanvasMessageBox");
		Canvas canvas = initializeCanvas(g);
		GameObject messageBox = (GameObject)Resources.Load<GameObject>("UITools/EditTextMessageBox");
		messageBox = Instantiate(messageBox);
		messageBox.transform.SetParent(canvas.transform, false);
		messageBox.transform.name = "EditTextMessageBox";
		Text[] textMessage = messageBox.GetComponentsInChildren<Text>();
		textMessage[0].text = placeHolderMessage;
		Text inputText = textMessage[1];
		Button[] b = messageBox.GetComponentsInChildren<Button>();
		b[0].onClick.AddListener( ()=> {Destroy(g);});
		b[1].onClick.AddListener( ()=> {
			string userMessage = inputText.text;
			Destroy(g);
			if(callbackFonctionIfClickOk != null)
				callbackFonctionIfClickOk(userMessage);
		});
	}

	public delegate void ShowSelectElementCallback(string valueSelected);
	public static void ShowSelectElementOnList(List<string> elements, ShowSelectElementCallback callbackFonctionIfValueIsSelected)
	{
		GameObject g = new GameObject("CanvasMessageBox");
		Canvas canvas = initializeCanvas(g);
		GameObject messageBox = (GameObject)Resources.Load<GameObject>("UITools/SelectListMessageBox");
		messageBox = Instantiate(messageBox);
		messageBox.transform.SetParent(canvas.transform, false);
		messageBox.transform.name = "SelectListMessageBox";
		Toggle modelToggle = messageBox.GetComponentInChildren<Toggle>();
		modelToggle.isOn = true;
		Text textOnToggle = modelToggle.GetComponentInChildren<Text>();
		foreach(string str in elements)
		{
			textOnToggle.text = str;
			Instantiate(modelToggle.gameObject).transform.SetParent(modelToggle.transform.parent.transform);
			modelToggle.isOn = false;
		}
		ToggleGroup toggleGroup = modelToggle.transform.parent.GetComponent<ToggleGroup>();
		Destroy(modelToggle.gameObject);

		Button[] b = messageBox.GetComponentsInChildren<Button>();
		b[0].onClick.AddListener( ()=> {Destroy(g);});
		b[1].onClick.AddListener( ()=> {
			Destroy(g);
			if(callbackFonctionIfValueIsSelected != null)
			{
				foreach(Toggle t in toggleGroup.ActiveToggles())
				{
					callbackFonctionIfValueIsSelected(t.GetComponentInChildren<Text>().text);
					break;
				}
			}
		});
	}

}
