using UnityEngine;
using System.Collections;

public class MapGenerator : MonoBehaviour {

	public Square emptySquare;
	public int hauteur;
	public int largeur;

	private Square[,] mapSquares;
	public Element[] elements;

	public enum GenerationType{//Pour etre utilisé dans l'editeur unity
		Empty,
		WithRandomElements
	}
	public GenerationType modeDeGeneration;

	void Start () {
		GenerateEmpty(hauteur, largeur);
		if (modeDeGeneration == GenerationType.WithRandomElements)
		{
			int[,] tab = GenerateRandom (hauteur, largeur);
			InstanciateForAllSquare (tab);
		}
	}
	
	public void GenerateEmpty(int hauteurArg, int largeurArg, bool stockReferences = false)
	{
		Square[,] tab = new Square[hauteurArg, largeurArg];
		for (int i= 0; i<hauteurArg; i++)
		{
			for(int j = 0; j<largeurArg; j++)
			{
				tab[i,j] = (Square) Instantiate (emptySquare, new Vector3(this.transform.position.x + j, this.transform.position.y + i, 0), Quaternion.identity);
				tab[i,j].transform.SetParent(this.transform);
			}
		}
		mapSquares = tab;
	}

	public int[,] GenerateRandom(int hauteur, int largeur)
	{
		RandomElementOnMapGenerator r = new RandomElementOnMapGenerator();
		return r.GenerateRandom(hauteur, largeur);
	}

	private void InstanciateForAllSquare(int[,] tab)
	{
		Element e;

		for (int i = 0; i<hauteur; i++)
		{
			for(int j=0; j<largeur; j++)
			{
				if(tab[i,j]>=0)
				{
					e = (Element) Instantiate (elements[tab[i,j]], mapSquares[i,j].transform.position, Quaternion.identity);
					e.transform.SetParent(mapSquares[i,j].transform);
					mapSquares[i,j].squareElement = e;
				}
			}
		}
	}
}
