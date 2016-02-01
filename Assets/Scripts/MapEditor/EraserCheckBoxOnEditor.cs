using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EraserCheckBoxOnEditor : MonoBehaviour {

		public RemoveEditorTiles removeEditorManaged;
		
		/// <summary>
		/// Processing performed by Unity when an instance is created.
		/// </summary>
		void Start ()
		{
			Toggle t = GetComponent<Toggle>();
			if (t == null)
				Debug.LogError ("EraserCheckBoxOnEditor doit etre associé a un Toggle (UI)");
			t.onValueChanged.AddListener((isCheck) =>
			                             {
				if(isCheck)
					OnToggleCheck();
				else
					OnToggleUnCheck();
			});
			removeEditorManaged.enabled = false;
			t.isOn = false;
		}
		

		private void OnToggleCheck()
		{
			removeEditorManaged.enabled = true;
		}
		

		private void OnToggleUnCheck()
		{
			removeEditorManaged.enabled = false;
		}
}
