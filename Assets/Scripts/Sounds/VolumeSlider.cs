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
    /// Initializes the Volume Slider.
    /// </summary>
	void Start ()
    {
        Camera camera = Camera.main;
        if (camera == null) return;
        SoundControl soundControl = camera.GetComponent<SoundControl>();
        if (soundControl == null) return;
        Slider.value = soundControl.GetVolume();
    }
}
