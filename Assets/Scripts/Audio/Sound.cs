using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{
    public string name;

    public AudioClip sound;

    [Range(0f, 1f)]
    public float volume = 1f;

    [Range(0.1f, 3f)]
    public float pitch = 1f;

    [Range(0f, 1f)]
    public float spatialBlend = 0f;

    public bool loop = false;

    public AudioMixerGroup output;

    [HideInInspector]
    public AudioSource src;
}
