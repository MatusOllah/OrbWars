using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    // zoznam zvukov
    public Zvuk[] zvuky;

    [HideInInspector]
    public static AudioManager instancia;

    void Awake() {
        // nastavenie inštancie
        if (instancia == null) instancia = this;
        else {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        // nastavenie zvuku
        foreach (Zvuk zvuk in zvuky) {
            zvuk.src = gameObject.AddComponent<AudioSource>();

            zvuk.src.clip = zvuk.zvuk;

            zvuk.src.volume = zvuk.hlasitost;
            zvuk.src.pitch = zvuk.vyska;
            zvuk.src.spatialBlend = zvuk.prostredie;
            zvuk.src.loop = zvuk.opakovat;
            zvuk.src.outputAudioMixerGroup = zvuk.vystup;
        }
    }
    
    // prehrá zvuk
    public void PrehratZvuk(string meno) {
        // nájde zvuk...
        Zvuk zvuk = Array.Find(zvuky, zvuky => zvuky.meno == meno);
        if (zvuk == null) {
            Debug.LogWarning($"Neznámy zvuk: {meno}");
            return;
        }

        // ...a prehrá ho
        Debug.Log($"Zvuk: {meno}");
        zvuk.src.Play();
    }
}
