using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    [HideInInspector]
    public static AudioManager instance;

    void Awake() {
        if (instance == null) instance = this;
        else {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds) {
            s.src = gameObject.AddComponent<AudioSource>();

            s.src.clip = s.sound;

            s.src.volume = s.volume;
            s.src.pitch = s.pitch;
            s.src.spatialBlend = s.spatialBlend;
            s.src.loop = s.loop;
            s.src.outputAudioMixerGroup = s.output;
        }
    }

    public Sound GetSound(string name) {
        Sound s = Array.Find(sounds, sounds => sounds.name == name);
        if (s == null)
        {
            Debug.LogWarning($"unknown sound: {name}");
            return null;
        }

        return s;
    }
    
    // play sound
    public void PlaySound(string name) {
        Debug.Log($"Sound: {name}");
        GetSound(name).src.Play();
    }
}
