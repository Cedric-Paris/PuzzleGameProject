using UnityEngine;
using System.Collections;

public class ButtonSound : MonoBehaviour {

	public enum ButtonTypes { Play, Pause, Stop, Mute}
	
	public ButtonTypes type;
	
	private SoundControl sound;
	//End of Region
	
	
	//Region Unity methods
	void start() {
		sound = Camera.main.GetComponent<SoundControl>();
	}
	
	
	void OnMouseDown() {
		Debug.Log ("Mouse Detected");

		if (sound == null)
		{
			sound = Camera.main.GetComponent<SoundControl>();
		}
		Debug.Log ("Mouse Detected");
		switch(type) {
		case ButtonTypes.Play: sound.Play(); break;
		case ButtonTypes.Pause: sound.Pause(); break;
		case ButtonTypes.Stop: 
			Debug.Log("Stop Detected");
			sound.Stop(); 
			break;
		case ButtonTypes.Mute: sound.ToggleMute(); break;
		}
		
	}
}
