using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
		UIMessageBox.ShowEditText("Saisir un nom de sauvegarde", Save);
	}

	private void Save(string nomFichier)
	{
		LevelSave.SaveTileMap(nomFichier);
	}

	public void OnClickLoad()
	{
		List<string> fileSaved = LevelSave.GetLevels();
		if(fileSaved.Count == 0)
		{
			UIMessageBox.ShowMessage("Aucune map enregistrée.");
			return;
		}
		UIMessageBox.ShowSelectElementOnList(fileSaved, Load);
	}

	private void Load(string nomFichier)
	{
		LevelSave.LoadTileMap(nomFichier);
	}
}
