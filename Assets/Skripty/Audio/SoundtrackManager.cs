using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundtrackManager : MonoBehaviour
{
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "HlavnéMenu") PlayInMenuSong();
        else PlayInGameSong();
    }

    void PlayInMenuSong() {
        FindObjectOfType<AudioManager>().NajistZvuk("InMenuIntro").src.Play();
        FindObjectOfType<AudioManager>().NajistZvuk("InMenuLoop").src.PlayDelayed(22.15f);
    }

    void PlayInGameSong() => FindObjectOfType<AudioManager>().NajistZvuk("InGame").src.Play();
}
