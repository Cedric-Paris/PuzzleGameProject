using UnityEngine;
using System.Collections;

public class SceneArcadeManager : SceneManager {

	public GameObject mapGeneratorPrefab;
	public GameObject currentMapGenerated;
	private int numeroLevel = 1;

	public override void LoadNextScene()
	{
		MapGenerator m = mapGeneratorPrefab.GetComponent<MapGenerator>();
		if( numeroLevel % 5 == 0)
		{
			m.hauteur += 1;
			m.largeur += 2;
			m.nbChangementDirection++;
		}
		numeroLevel++;
		GameObject newMap = (GameObject)Instantiate(mapGeneratorPrefab, new Vector3(0,0,0), Quaternion.identity);
		m.hauteur = 9;
		m.largeur = 12;
		m.nbChangementDirection = 6;
		Destroy(currentMapGenerated);
		currentMapGenerated = newMap;
	}
	
	public override void ReloadCurrentScene()
	{
	}
	
	public override void LoadPreviousScene()
	{
	}
}
