using UnityEngine;
using System.Collections;

public class MapGenerator : MonoBehaviour {

	private MenuGameAdapter menuAdapter;

	public Square emptySquare;
	public GameObject wallAround; 
	public int hauteur;
	public int largeur;
	public int nbChangementDirection;

	private Square[,] mapSquares;
	public Element[] elements;

	private RandomElementOnMapGenerator elementGenerator;

	public enum GenerationType{//Pour etre utilisé dans l'editeur unity
		Empty,
		WithRandomElements
	}
	public GenerationType modeDeGeneration;

	void Start () {
		if(menuAdapter == null)
			menuAdapter = GameObject.Find("MenuAdapter").GetComponent<MenuGameAdapter>();
		GenerateEmpty(hauteur, largeur);
		CreateBorder (hauteur, largeur, this.wallAround);
		if (modeDeGeneration == GenerationType.WithRandomElements)
		{
			int[,] tab = GenerateRandom (hauteur, largeur, nbChangementDirection);
			InstanciateForAllSquare (tab);
		}
	}
	
	public void GenerateEmpty(int hauteurArg, int largeurArg, bool stockReferences = false)
	{
		Square[,] tab = new Square[hauteurArg, largeurArg];
		this.transform.position = new Vector3(0.5f,0.5f,0);
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

	public int[,] GenerateRandom(int hauteur, int largeur, int nbChangeDirect)
	{
		elementGenerator = new RandomElementOnMapGenerator();
		return elementGenerator.GenerateRandom(hauteur, largeur, nbChangeDirect);
	}

	private void InstanciateForAllSquare(int[,] tab)
	{
		Element e;
		StartCase caseDepart = null;
		for (int i = 0; i<hauteur; i++)
		{
			for(int j=0; j<largeur; j++)
			{
				if(tab[i,j]>=0)
				{
					e = (Element) Instantiate (elements[tab[i,j]], mapSquares[i,j].transform.position, Quaternion.identity);
					if(tab[i,j] == 0)
						caseDepart = e as StartCase;
					e.transform.SetParent(mapSquares[i,j].transform);
					mapSquares[i,j].squareElement = e;
				}
			}
		}
		if(caseDepart == null)
		{
			foreach (Transform child in this.gameObject.transform)
				Destroy(child.gameObject);
			Start();
			return;
		}
		menuAdapter.adaptMenu(caseDepart, elementGenerator.getTabActionsPossible());
	}

	private void CreateBorder(int height, int width, GameObject wall)
	{
		GameObject borderInstance = new GameObject();
		GameObject instance;
		borderInstance.name = "BorderGeneratedMap";
		Vector2 posTileMap = new Vector2(this.transform.position.x, this.transform.position.y);
		for(float i=posTileMap.y; i<height; i++)
		{
			instance = (GameObject)Instantiate(wall,new Vector3(posTileMap.x - 1, i, 0),Quaternion.identity);
			instance.transform.SetParent(borderInstance.transform);
			instance = (GameObject)Instantiate(wall,new Vector3(posTileMap.x + width, i, 0),Quaternion.identity);
			instance.transform.SetParent(borderInstance.transform);
		}
		for(float j=posTileMap.x-1; j<width+1; j++)
		{
			instance = (GameObject)Instantiate(wall,new Vector3(j, posTileMap.y - 1, 0),Quaternion.identity);
			instance.transform.SetParent(borderInstance.transform);
			instance = (GameObject)Instantiate(wall,new Vector3(j, posTileMap.y + height, 0),Quaternion.identity);
			instance.transform.SetParent(borderInstance.transform);
		}
		borderInstance.transform.SetParent(this.transform);
	}
}

