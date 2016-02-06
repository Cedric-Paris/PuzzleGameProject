using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEditor;

public class UIMessageBox : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameObject myButtonPrefab = (GameObject)Resources.Load<GameObject>("BasicSquare");
		GameObject actualButton = GameObject.Instantiate(myButtonPrefab) as GameObject;
		GameObject g = new GameObject();
		Canvas canvas = g.AddComponent<Canvas>();
		g.AddComponent<CanvasScaler>();
		g.AddComponent<GraphicRaycaster>();
		canvas.renderMode = RenderMode.ScreenSpaceCamera;
		canvas.worldCamera = Camera.main;
		canvas.sortingOrder = 51;

		GameObject text = new GameObject();
		text.transform.SetParent(g.transform);
		Text t = text.AddComponent<Text>();
		t.font = new Font("Qlassik.ttf");

	}
	
	// Update is called once per frame
	void Update () {

	}
}
