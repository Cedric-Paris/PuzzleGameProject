using UnityEngine;
using System.Collections;

public class ButtonsLeftSidePanel : MonoBehaviour {

	public void OnClickExit()
	{
		UIMessageBox.ShowYesNo("Voulez vous vraiment quitter l'éditeur?", ()=>{}, ()=>{});
	}

	public void OnClickClear()
	{
		UIMessageBox.ShowYesNo("Toute la carte va etre réinitialisée, voulez-vous continuer?", ()=>{Application.LoadLevel("MapEditorScene");}, ()=>{});
	}

	public void OnClickSave()
	{
		UIMessageBox.ShowEditText("Saisir un nom de sauvegarde");
	}

	public void OnClickLoad()
	{
		
	}
}
