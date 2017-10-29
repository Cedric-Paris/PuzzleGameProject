using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public static Camera mainCamera = null;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        
        DontDestroyOnLoad(gameObject);

        InitGame();
    }

    private void InitGame()
    {

    }
}
