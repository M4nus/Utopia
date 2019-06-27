using UnityEngine.Audio;
using System;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{

    public static AudioManager instance;

    public AudioMixerGroup mixerGroup;

    public Sound[] sounds;

    void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.loop = s.loop;  
        }

        StartCoroutine(GameMusic());
    }
                 

    public void Play(string sound)
    {
        Sound s = Array.Find(sounds, item => item.name == sound);
        if(s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
        s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));
        Debug.Log("Playing " + sound);
        s.source.Play();
    }

    IEnumerator MusicQueue()
    {            
        //yield return StartCoroutine(IntroSong());    
        yield return StartCoroutine(GameMusic());
    }

    IEnumerator IntroSong()
    {        
        Play("Intro");
        yield return null;     
    }

    IEnumerator Theme1()
    {
        Play("Theme1");
        yield return null;
    }

    IEnumerator Theme2()
    {
        Play("Theme2");
        yield return null;
    }

    IEnumerator GameMusic()
    {      
        StartCoroutine(Theme1());
        //StartCoroutine(Theme2());
        yield return null;
    }

}
