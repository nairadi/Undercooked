using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScript: MonoBehaviour {
	public AudioClip musicClipNormal;
	public AudioClip musicClipSlow;
	public AudioClip musicClipSuperSlow;
	public AudioClip musicClipEndGame;

	public AudioSource musicSource;

	MusicState state;

	// Use this for initialization
	void Start () {
		musicSource.clip = musicClipNormal;
		state = MusicState.Normal;
	}

	public void PlayMusicNormal(){
		if (state != MusicState.Normal) {
			state = MusicState.Normal;
			musicSource.clip = musicClipNormal;
		}
		musicSource.Play ();
	}

	public void PlayMusicSlow (){
		if (state != MusicState.Slow) {
			state = MusicState.Slow;
			musicSource.clip = musicClipSlow;
		}
		musicSource.Play ();
	}

	public void PlayMusicSuperSlow (){
		if (state != MusicState.SuperSlow) {
			state = MusicState.SuperSlow;
			musicSource.clip = musicClipSuperSlow;
		}
		musicSource.Play ();
	}

	public void PlayMusicEndGame(){
		if (state != MusicState.EndGame) {
			Debug.LogError ("Playing end game music");
			state = MusicState.EndGame;
			musicSource.clip = musicClipEndGame;
		}
		musicSource.Play ();
	}

	public void StopMusic(){
		musicSource.Stop ();
	}
}
