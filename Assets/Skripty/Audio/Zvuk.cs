using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Zvuk
{
    // meno zvuku
    public string meno;

    // zvuk
    public AudioClip zvuk;

    // hlasitos zvuku
    [Range(0f, 1f)]
    public float hlasitost = 1f;

    // vıška zvuku
    [Range(0.1f, 3f)]
    public float vyska = 1f;

    // prostredie zvuku (0f je 2D, 1f je 3D)
    [Range(0f, 1f)]
    public float prostredie = 0f;

    // má sa zvuk opakova?
    public bool opakovat = false;

    // vıstup zvuku (v mixéri)
    public AudioMixerGroup vystup;

    [HideInInspector]
    public AudioSource src;
}
