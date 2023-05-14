using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundtrackManager : MonoBehaviour
{
    [HideInInspector]
    public static SoundtrackManager instancia;

    void Awake()
    {
        // nastavenie in�tancie
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
        if (SceneManager.GetActiveScene().name == "HlavneMenu") PlayInMenuSong();
        if (SceneManager.GetActiveScene().name == "Test") PlayInGameSong();
    }

    void PlayInMenuSong()
    {
        FindObjectOfType<AudioManager>().NajistZvuk("InMenuIntro").src.Play();
        FindObjectOfType<AudioManager>().NajistZvuk("InMenuLoop").src.PlayDelayed(16.008f);
    }

    void PlayInGameSong() => FindObjectOfType<AudioManager>().NajistZvuk("InGame").src.Play();
}
