using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundtrackManager : MonoBehaviour
{
    [HideInInspector]
    public static SoundtrackManager instance;

    void Awake()
    {
        if (instance == null) instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu") PlayInMenuSong();
        if (SceneManager.GetActiveScene().name == "Test") PlayInGameSong();
    }

    void PlayInMenuSong()
    {
        FindObjectOfType<AudioManager>().GetSound("InMenuIntro").src.Play();
        FindObjectOfType<AudioManager>().GetSound("InMenuLoop").src.PlayDelayed(16.008f);
    }

    void PlayInGameSong() => FindObjectOfType<AudioManager>().GetSound("InGame").src.Play();
}
