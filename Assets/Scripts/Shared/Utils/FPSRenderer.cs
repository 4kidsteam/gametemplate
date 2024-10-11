using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class FPSRenderer : MonoBehaviour
{
    private static FPSRenderer _instance;
    public static FPSRenderer Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("FPSRenderer");
                GameObject.DontDestroyOnLoad(go);
                _instance = go.AddComponent<FPSRenderer>();
                _instance._Init();
            }
            return _instance;
        }
    }

    private float _deltaTime = 0.0f;
    private GUIStyle _style;
    private Rect _rect;

    private void _Init()
    {
        int w = Screen.width, h = Screen.height;
        _rect = new Rect(10, 10, 100, 20);

        _style = new GUIStyle();
        _style.alignment = TextAnchor.UpperLeft;
        _style.fontSize = h * 2 / 100 + 15;
        _style.normal.textColor = Color.green;
    }

    void Update()
    {
        _deltaTime += (Time.deltaTime - _deltaTime) * 0.1f;
    }

    void OnGUI()
    {
        if (_style != null)
        {
            float fps = 1.0f / _deltaTime;

            float msec = _deltaTime * 1000.0f;

            string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);

            //GUI.Label(_rect, text, _style);
        }
    }

    public void StartRender()
    {
    }
}
