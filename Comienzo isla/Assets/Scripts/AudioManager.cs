using UnityEngine.Audio;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;

    void Awake()
    {
        if(instance == null){
            instance = this;
        }else if(instance != this){
            Destroy(instance.gameObject);
            instance = this;
        }

        DontDestroyOnLoad(gameObject);

        foreach(Sound s in sounds){
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    void Start(){
        int index = SceneManager.GetActiveScene().buildIndex;
        
        if(index == 0)
            Play("MainTheme");
        else if(index == 1)
            Play("Tutorial");
        else if(index == 2)
            Play("Bibury");
    }

    public void Play(string name){
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null) return;
        s.source.Play();
    }

    public void Stop(string name){
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null) return;
        s.source.Stop();
    }

    public void StopWalking(){
        foreach (string name in Enum.GetNames(typeof(WalkingAudios)))  
        {  
            Sound s = Array.Find(sounds, sound => sound.name == name);
            if(s == null) break;
            s.source.Stop();
        } 
    }

    public void HoverButton(){
        Sound s = Array.Find(sounds, sound => sound.name == "HoverButton");
        if(s == null) return;
        s.source.Play();
    }

    public void ClickButton(){
        Sound s = Array.Find(sounds, sound => sound.name == "ClickButton");
        if(s == null) return;
        s.source.Play();
    }

}

public enum WalkingAudios { SandWalking, PathWalking, GrassWalking, WoodWalking}
