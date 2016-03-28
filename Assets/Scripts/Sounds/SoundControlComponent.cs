using UnityEngine;
using System.Collections;
using System.Linq;

[RequireComponent(typeof(SoundControl))]
public class SoundControlComponent : MonoBehaviour {

    private SoundControl soundControlInstance;
    private static bool alreadyExistInScene = false;
    public bool isFirstOne = false;

    void Awake()
    {
        soundControlInstance = GetComponent<SoundControl>();
    }

    void Start()
    {
        Play();
        DontDestroyOnLoad(this.gameObject);
        alreadyExistInScene = true;
    }

    void OnLevelWasLoaded(int level)
    {
        if (isFirstOne)
        {
            SoundControlComponent[] soundControlComponents = GameObject.FindObjectsOfType<SoundControlComponent>();
            if (soundControlComponents.Length > 1)
            { 
                foreach (SoundControlComponent scc in soundControlComponents.Where(scc => scc != this))
                {
                    Destroy(scc.gameObject);
                }
            }
        }

        Play();
    }

    private void Play()
    {
        if (!soundControlInstance.isPlaying())
        {
            soundControlInstance.Play();
        }
    }

    public void SetVolume(float volume)
    {
        soundControlInstance.SetVolume(volume);
    }

    public float GetVolume()
    {
        return soundControlInstance.GetVolume();
    }
}
