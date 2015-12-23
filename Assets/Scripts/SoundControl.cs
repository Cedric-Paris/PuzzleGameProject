﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class SoundControl : MonoBehaviour {
	//Region Attribues
	public AudioSource audio_music;
	public AudioClip ambient;

	//End of Region



	//Region Unity methods
	void start() {
		audio_music=GetComponent<AudioSource>();
		audio_music.clip = ambient;
		audio_music.Play ();
	}
	//End of Region



	//Region Other method
	public void Play() { audio_music.Play (); }

	public void Pause() { audio_music.Pause (); }

	public void Stop() { audio_music.Stop (); }

	public void ToggleMute() {
		if (audio_music.mute) {
			audio_music.mute = false;
		} else {
			audio_music.mute = true;
		}

	}
	//End of Region
}