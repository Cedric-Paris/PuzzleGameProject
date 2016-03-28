using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Describes the Volumes Slider's behaviour.
/// </summary>
public class VolumeSlider : MonoBehaviour
{
    /// <summary>
    /// The actual UI Volume Slider
    /// </summary>
    public Slider Slider;

    /// <summary>
    /// The <see cref="SoundControlComponent"/> we use to reguilate the volume of music and sounds.
    /// </summary>
    private SoundControlComponent soundControlComponent;

    /// <summary>
    /// Initializes the Volume Slider.
    /// </summary>
	void Start ()
    {
        soundControlComponent = GameObject.FindObjectOfType<SoundControlComponent>();
        if (soundControlComponent == null) return;
        Slider.value = soundControlComponent.GetVolume();
    }
    
    public void SetVolume(float volume)
    {
        soundControlComponent.SetVolume(volume);
    }
}
