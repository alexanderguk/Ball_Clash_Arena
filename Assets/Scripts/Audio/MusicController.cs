﻿using UnityEngine;

public class MusicController : MonoBehaviour {
	public float fadeInTime;
	public float fadeOutTime;

	private bool isFadeOut;
    private bool isFadeIn;

    private AudioSource music;

	void Start() {
		isFadeIn = true;
        music = GetComponent<AudioSource>();
    }

    void FixedUpdate() {
        if (isFadeIn) {
			FadeIn();
        } else if (isFadeOut) {
			FadeOut();
		}
	}

	private void FadeIn() {
        if (!music.isPlaying) { 
            music.Play(); 
        }
        if (music.volume < 1) {
			music.volume += Time.fixedDeltaTime / fadeInTime;
		} else {
			isFadeIn = false;
		}
	}
	
	private void FadeOut() {
		if (music.volume > 0) {
            music.volume -= Time.fixedDeltaTime / fadeOutTime;
		} else {
			isFadeOut = false;
			music.Stop();
		}
	}

    public void StartFadeIn() {
        isFadeIn = true;
        isFadeOut = false;
    }
    
    public void StartFadeOut() {
        isFadeOut = true;
        isFadeIn = false;
    }
    
    public void Stop()
    {
        music.volume = 0;
        music.Stop();
    }
}
