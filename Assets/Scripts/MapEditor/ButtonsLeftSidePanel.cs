using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ButtonsLeftSidePanel : MonoBehaviour {

	public void OnClickExit()
	{
		UIMessageBox.ShowYesNo(new GlobalMultiling().getTranslatedValue("@+/Exit?"), ()=>{Application.LoadLevel("MenuPrincTemp");}, ()=>{});
	}

	public void OnClickClear()
	{
		UIMessageBox.ShowYesNo(new GlobalMultiling().getTranslatedValue("@+/EditorMessageClear"), ()=>{Application.LoadLevel("MapEditorScene");}, ()=>{});
	}

	public void OnClickSave()
	{
		UIMessageBox.ShowEditText(new GlobalMultiling().getTranslatedValue("@+/EditorSaveName"), Save);
	}

	private void Save(string nomFichier)
	{
		nomFichier = nomFichier.Replace("/","").Replace(".","-");
		LevelSave.SaveTileMap(nomFichier, null);
	}

	public void OnClickLoad()
	{
		List<string> fileSaved = LevelSave.GetLevels();
		if(fileSaved.Count == 0)
		{
			UIMessageBox.ShowMessage(new GlobalMultiling().getTranslatedValue("@+/NoMap"));
			return;
		}
		UIMessageBox.ShowSelectElementOnList(fileSaved, Load);
	}

	private void Load(string nomFichier)
	{
		LevelSave.LoadTileMap(nomFichier);
		PutEditorTiles putTiles = GameObject.FindGameObjectWithTag("TileMap").GetComponent<PutEditorTiles>();
		putTiles.RefreshPosition();
	}
}
