using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CameraCheckBoxOnEditor : MonoBehaviour {

	public Camera cameraManaged;
	private CameraMovementManager camManager;

	void Start () {
		camManager = cameraManaged.GetComponent<CameraMovementManager>();
		camManager.enabled = false;
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
		camManager.enabled = true;
	}

	private void OnToggleUnCheck()
	{
		camManager.enabled = false;
		SelectElementOnEditorMenu.enableTileSet = true;
		if (SelectElementOnEditorMenu.currentButton == null)
			return;
	}



}