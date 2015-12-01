using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {

    public Texture2D BackgroundTexture;
    public Texture2D RightArrowTexture;
    public Texture2D DownArrowTexture;
    public Texture2D LeftArrowTexture;
    public Texture2D UpArrowTexture;
    public Texture2D JumpTexture;
    private GUIStyle Style = new GUIStyle();

    // Use this for initialization
    void Start () {
        Style.fontSize = 50;
	}

    void OnGUI()
    {
        GUI.DrawTexture(new Rect(Screen.width - 320, 50, 300, 600), BackgroundTexture);
        GUI.Label(new Rect(Screen.width - 280, 80, 100, 100), RightArrowTexture);
        GUI.Label(new Rect(Screen.width - 180, 100, 100, 100), " x ?", Style);
        GUI.Label(new Rect(Screen.width - 280, 190, 100, 100), DownArrowTexture);
        GUI.Label(new Rect(Screen.width - 180, 210, 100, 100), " x ?", Style);
        GUI.Label(new Rect(Screen.width - 280, 300, 100, 100), LeftArrowTexture);
        GUI.Label(new Rect(Screen.width - 180, 320, 100, 100), " x ?", Style);
        GUI.Label(new Rect(Screen.width - 280, 410, 100, 100), UpArrowTexture);
        GUI.Label(new Rect(Screen.width - 180, 430, 100, 100), " x ?", Style);
        GUI.Label(new Rect(Screen.width - 280, 520, 100, 100), JumpTexture);
        GUI.Label(new Rect(Screen.width - 180, 540, 100, 100), " x ?", Style);
    }

	// Update is called once per frame
	void Update () {
	    
	}
}
