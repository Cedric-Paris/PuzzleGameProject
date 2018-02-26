using UnityEngine;
using Assets.Scripts.Utilities;

public class GameManager : MonoBehaviour
{
    private static Camera mainCamera;
    public static GameManager instance;

    public static Camera MainCamera
    {
        get { return mainCamera; }
        set
        {
            mainCamera = value;
            OnCameraSizeChanged();
        }
    }

    public static int GameUnitSizeInPixel {
        get { return Mathf.CeilToInt(Screen.height / (MainCamera.orthographicSize * 2)); }
    }
    
    public static event EmptyEventHandler CameraSizeChanged;

    public static void OnCameraSizeChanged()
    {
        if (CameraSizeChanged != null)
            CameraSizeChanged();
    }

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
