using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    AudioSource bgmSound;
    public AudioClip[] bgmList;
    public AudioClip buttonClip;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }

        bgmSound = GetComponent<AudioSource>();
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        for (int i = 0; i < bgmList.Length; i++)
        {
            if(arg0.name == bgmList[i].name)
            {
                BGMPlay(bgmList[i]);
            }
        }        
    }

    public void SoundPlay(string soundName, AudioClip clip)
    {
        GameObject so = new GameObject(soundName + "Sound");
        AudioSource audiosource = so.AddComponent<AudioSource>();
        audiosource.clip = clip;
        audiosource.volume = 0.3f;
        audiosource.Play();

        Destroy(so, clip.length);
    }

    public void ButtonSoundPlay()
    {
        GameObject buttonSound = new GameObject("ButtonSound");
        AudioSource audiosource = buttonSound.AddComponent<AudioSource>();
        audiosource.clip = buttonClip;
        audiosource.volume = 0.3f;
        audiosource.Play();

        Destroy(buttonSound, buttonClip.length);
    }


    public void BGMPlay(AudioClip clip)
    {
        bgmSound.clip = clip;
        bgmSound.loop = true;
        bgmSound.volume = 0.1f;
        bgmSound.Play();
    }
}
