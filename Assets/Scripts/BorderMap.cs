using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class BorderMap : MonoBehaviour {
	public GameObject wall;
	public int width;
	public int height;
	private readonly static string NOM_BORDER = "BorderMapSet";
	
	void Start()
	{
		GameObject borderInstance = new GameObject();
		GameObject instance;
		borderInstance.name = NOM_BORDER;
		Vector2 posTileMap = new Vector2(this.transform.position.x, this.transform.position.y);
		for(float i=posTileMap.y; i<height; i++)
		{
			instance = (GameObject)Instantiate(wall,new Vector3(posTileMap.x - 0.5f , i+0.5f, 0),Quaternion.identity);
			instance.transform.SetParent(borderInstance.transform);
			instance = (GameObject)Instantiate(wall,new Vector3(posTileMap.x + width + 0.5f, i+0.5f, 0),Quaternion.identity);
			instance.transform.SetParent(borderInstance.transform);
		}
		for(float j=posTileMap.x-1; j<width+1; j++)
		{
			instance = (GameObject)Instantiate(wall,new Vector3(j + 0.5f , posTileMap.y - 0.5f, 0),Quaternion.identity);
			instance.transform.SetParent(borderInstance.transform);
			instance = (GameObject)Instantiate(wall,new Vector3(j + 0.5f, posTileMap.y + height + 0.5f, 0),Quaternion.identity);
			instance.transform.SetParent(borderInstance.transform);
		}
		borderInstance.transform.SetParent(this.transform);
	}

}