using UnityEngine;
using System.Collections;

/// <summary>
///  
/// </summary>
public class MainMenu : MonoBehaviour {
	/// <summary>
	/// The skin menu. Permet de paramétrer tout les éléments appartenant au menu.
	/// </summary>
	public GUISkin skinMenu;

	/// <summary>
	/// C'est l'affichage du menu du jeu
	/// </summary>
	void OnGUI()
	{
		GUI.skin = skinMenu;
		GUI.Label (new Rect(10,10,400,75),"Menu Principal"); 


		if (GUI.Button(new Rect(10,100,160,45),"Niveaux"))
		{
			Application.LoadLevel(11);
		}

		if (GUI.Button(new Rect(170,100,160,45),"Editeur de Niveaux"))
		{
			Application.LoadLevel(12);
		}

		if (GUI.Button(new Rect(170,155,160,45),"Mode Arcade"))
		{
			//Application.LoadLevel(1);
		}
			 

		if (GUI.Button(new Rect(10,155,160,45),"Quitter"))
		{
			Application.Quit();
		}
	}
} 
