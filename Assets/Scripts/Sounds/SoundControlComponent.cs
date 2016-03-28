using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SoundControl))]
public class SoundControlComponent : MonoBehaviour {

    private SoundControl soundControlInstance;
    private static bool alreadyExistInScene = false;

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
        if (level == 0 && alreadyExistInScene)
        {
            Destroy(this.gameObject);
            alreadyExistInScene = false;
            return;
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
