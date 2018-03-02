using UnityEngine;

public class GameLoader : MonoBehaviour {

    [SerializeField]
    private GameManager gameManager;
    
	void Awake ()
    {
        var camera = GetComponent<Camera>();

        if(GameManager.instance == null)
        {
            Instantiate(gameManager).transform.name = "GameManager";
        }

        GameManager.MainCamera = camera;
	}
}
