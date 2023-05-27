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

    // fps
    private int fps;

    void Awake() {
        StartCoroutine(getFPS());
        StartCoroutine(renderFPS());
        StartCoroutine(renderRTT());
    }

    IEnumerator getFPS() {
        while (true) {
            fps++;
            yield return null;
        }
    }

    IEnumerator renderFPS() {
        while (true) {
            yield return new WaitForSeconds(1);
            fpsText.text = fps.ToString();
            fps = 0;
        }
    }

    IEnumerator renderRTT() {
        while (true) {
            yield return new WaitForSeconds(1);
            rttText.text = $"{Math.Round(NetworkTime.rtt * 1000)} ms";
        }
    }
}