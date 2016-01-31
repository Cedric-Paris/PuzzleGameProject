using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Manage events associated to the checkbox on Map Editor.
/// </summary>
public class CameraCheckBoxOnEditor : MonoBehaviour {

	/// <summary>
	/// The camera managed when the checkbox is checked.
	/// </summary>
	public Camera cameraManaged;
	/// <summary>
	/// The camera manager used to manage the cameraManaged.
	/// </summary>
	private CameraMovementManager camManager;

	/// <summary>
	/// Processing performed by Unity when an instance is created.
	/// </summary>
	void Start ()
	{
		camManager = cameraManaged.GetComponent<CameraMovementManager>();
		camManager.enabled = false;
		Toggle t = GetComponent<Toggle> ();
		if (t == null)
			Debug.LogError ("CameraChackBoxOnEditor doit etre associé a un Toggle (UI)");
		t.onValueChanged.AddListener((isCheck) =>
		             {
			if(isCheck)
				OnToggleCheck();
			else
				OnToggleUnCheck();
		});
	}

	/// <summary>
	/// Called if the checkBox is checked.
	/// Disable adding of items and activates the "Camera Functions".
	/// </summary>
	private void OnToggleCheck()
	{
		SelectElementOnEditorMenu.enableTileSet = false;
		camManager.enabled = true;
	}

	/// <summary>
	/// Called if the checkBox is unchecked.
	/// Disable "Camera Functions" and activates adding of items.
	/// </summary>
	private void OnToggleUnCheck()
	{
		camManager.enabled = false;
		SelectElementOnEditorMenu.enableTileSet = true;
		if (SelectElementOnEditorMenu.currentButton == null)
			return;
	}



}