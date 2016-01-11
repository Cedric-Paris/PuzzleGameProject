using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CameraCheckBoxOnEditor : MonoBehaviour {

	void Start () {
		Toggle t = GetComponent<Toggle> ();
		if (t == null)
			Debug.LogError ("CameraChackBoxOnEditor soit etre associé a un Toggle (UI)");
		t.onValueChanged.AddListener((isCheck) =>
		             {
			if(isCheck)
				OnToggleCheck();
			else
				OnToggleUnCheck();
		});
	}

	private void OnToggleCheck()
	{
		SelectElementOnEditorMenu.enableTileSet = false;
	}

	private void OnToggleUnCheck()
	{
		if (SelectElementOnEditorMenu.currentButton == null)
			return;
		SelectElementOnEditorMenu.enableTileSet = true;
	}



}