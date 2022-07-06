using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundtrackManager : MonoBehaviour
{
    [HideInInspector]
    public static SoundtrackManager instancia;

    void Awake()
    {
        // nastavenie inštancie
        if (instancia == null) instancia = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "HlavnéMenu") PlayInMenuSong();
        if (SceneManager.GetActiveScene().name == "Test") PlayInGameSong();
    }

    void PlayInMenuSong()
    {
        FindObjectOfType<AudioManager>().NajistZvuk("InMenuIntro").src.Play();
        FindObjectOfType<AudioManager>().NajistZvuk("InMenuLoop").src.PlayDelayed(22.15f);
    }

    void PlayInGameSong() => FindObjectOfType<AudioManager>().NajistZvuk("InGame").src.Play();
}
