using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
public class MusicManagement : MonoBehaviour
{
    public Sound[] musicSounds, SFXSounds;
    public AudioSource musicSource, SFXSource;

    public void PlayMusic(string name) {
        Sound s = Array.Find(musicSounds, x => x.name == name);
        if(s == null) {
            Debug.Log("No Sounds");
        } else {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }

    public void PlaySFX(string name) {
        Sound s = Array.Find(SFXSounds, x => x.name == name);
        if(s == null) {
            Debug.Log("No Sounds");
        } else {
            SFXSource.pitch = UnityEngine.Random.Range(0.85f, 1.15f);
            SFXSource.clip = s.clip;
            SFXSource.Play();
        }
    }

    // Update is called once per frame
    void Start()
    {
        if(SceneManager.GetActiveScene().buildIndex == 0) {
            PlayMusic("Title");
        } else {
            PlayMusic("Main");
        }
    }

    public void setVolume(float volume) {
        musicSource.volume = volume;
        GlobalVariables.Volume = volume;
    }
}