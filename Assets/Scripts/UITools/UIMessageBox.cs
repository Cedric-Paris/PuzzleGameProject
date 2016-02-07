using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Provides methods to display message boxes for the user.
/// </summary>
public class UIMessageBox : MonoBehaviour {
	
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
		Canvas canvas = g.AddComponent<Canvas>();
		g.AddComponent<CanvasScaler>();
		g.AddComponent<GraphicRaycaster>();
		canvas.renderMode = RenderMode.ScreenSpaceCamera;
		canvas.worldCamera = Camera.main;
		canvas.sortingOrder = 51;
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
}
