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

    // po�et fps
    private int fps;

    void Awake() {
        StartCoroutine(meratFPS());
        StartCoroutine(ukazatFPS());
        StartCoroutine(ukazatRTT());
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

    IEnumerator ukazatRTT() {
        while (true) {
            yield return new WaitForSeconds(1);
            rttText.text = $"{Math.Round(NetworkTime.rtt * 1000)} ms";
        }
    }
}