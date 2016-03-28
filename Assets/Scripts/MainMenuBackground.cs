using UnityEngine;
using System.Collections;
using System.Linq;

public class MainMenuBackground : MonoBehaviour
{
    private static bool alreadyExistInScene = false;
    public bool isFirstOne = false;

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        alreadyExistInScene = true;
    }

    void OnLevelWasLoaded(int level)
    {
        if (level != 0 && level != 11 && level != 14 && alreadyExistInScene)
        {
            Debug.Log("Destroy");
            Destroy(this.gameObject);
            alreadyExistInScene = false;
        }
        
        if (isFirstOne && (level == 0 || level == 11))
        {
            MainMenuBackground[] mainMenuBackgrounds = GameObject.FindObjectsOfType<MainMenuBackground>();
            foreach (MainMenuBackground mmb in mainMenuBackgrounds.Where(mmb => mmb != this))
            {
                Destroy(mmb.gameObject);
            }
        }
    }
}
