using System;
using System.Collections;
using UnityEngine;
using TMPro;
using Mirror;

public class FPSCounter : MonoBehaviour
{
    // fps counter text
    [SerializeField]
    private TMP_Text fpsText;

    // rtt text
    [SerializeField]
    private TMP_Text rttText;

    // poèet fps
    private int fps;

    void Awake() {
        StartCoroutine(meratFPS());
        StartCoroutine(ukazatFPS());
    }

    void Update() {
        rttText.text = $"{Math.Round(NetworkTime.rtt * 1000)} ms";
    }

    IEnumerator meratFPS() {
        while (true) {
            fps++;
            yield return null;
        }
    }

    IEnumerator ukazatFPS() {
        while (true) {
            yield return new WaitForSeconds(1);
            fpsText.text = fps.ToString();
            fps = 0;
        }
    }
}