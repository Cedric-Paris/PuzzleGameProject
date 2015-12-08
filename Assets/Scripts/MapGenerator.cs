using UnityEngine;
using System.Collections;

public class MapGenerator : MonoBehaviour {

	public Square emptySquare;
	public int hauteur;
	public int largeur;

	void Start () {
		GenerateEmpty(largeur, hauteur);
	}
	
	public void GenerateEmpty(int largeurArg, int hauteurArg, bool stockReferences = false)
	{
		Square[,] tab = new Square[largeurArg,hauteurArg];
		for (int i= 0; i<hauteurArg; i++)
		{
			for(int j = 0; j<largeurArg; j++)
				tab[j,i] = (Square) Instantiate (emptySquare, new Vector3(this.transform.position.x + j, this.transform.position.y + i, 0), Quaternion.identity);
		}
	}
}
