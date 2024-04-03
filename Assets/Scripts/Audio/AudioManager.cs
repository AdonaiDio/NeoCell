using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using Unity.IO.LowLevel.Unsafe;
using UnityEditor;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }
    public EventInstance musicInstance;
    public EventInstance ambienceInstance;
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Audio Manager in the scene.");
        }
        instance = this;
        DontDestroyOnLoad(instance);        
    }
    public void OnEnable(){              
        
        SceneManager.activeSceneChanged += changeTrack;

    }

  
    public void PlayOneShot(EventReference sound, Vector3 worldPos)
    {
        RuntimeManager.PlayOneShot(sound, worldPos);
    }
    public EventInstance CreateInstance(EventReference eventReference)
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
        return eventInstance;
    }
    public void InitializeMusic (EventReference musicReference){
        musicInstance = CreateInstance(musicReference);
        musicInstance.start();
    }
    public void InitializeAmbience (EventReference ambienceReference){
        ambienceInstance = CreateInstance(ambienceReference);
        ambienceInstance.start();        
    }
    public void StopMusic (EventInstance musicInstance){
        
        musicInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        musicInstance.release();
    }
    public void StopAmbience (EventInstance ambienceInstance){
        ambienceInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        ambienceInstance.release();
    }
    public void changeTrack(Scene oldScene, Scene newScene){
        if (newScene.buildIndex == 0){
        //InitializeMusic(FMODEvents.instance.music_gameplay);
        StopMusic(musicInstance);
        StopAmbience(ambienceInstance);
        InitializeAmbience(FMODEvents.instance.ambience_menu);
        }
        if (newScene.buildIndex == 1){
        StopMusic(musicInstance);
        StopAmbience(ambienceInstance);
        InitializeMusic(FMODEvents.instance.music_gameplay);
        InitializeAmbience(FMODEvents.instance.ambience_gameplay);
        }
        if (newScene.buildIndex == 3){
        StopMusic(musicInstance);
        StopAmbience(ambienceInstance);
        PlayOneShot(FMODEvents.instance.tela_de_morte, transform.position);
        }
    }
        
    

}
