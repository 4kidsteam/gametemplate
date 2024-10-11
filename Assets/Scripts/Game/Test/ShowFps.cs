using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowFps : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI tex;

    private void Awake()
    {
        StartCoroutine(UpdateFps());
    }

    IEnumerator UpdateFps()
    {
        int delayFrame = 15;
        while(true)
        {
            float stampTime = Time.unscaledTime;

            for (int i = 0; i < delayFrame; i++)
            {
                yield return null;
            }
            float fps = delayFrame / (Time.unscaledTime - stampTime);
            tex.text = "FPS: " + Mathf.RoundToInt(fps);
        }
    }
}
