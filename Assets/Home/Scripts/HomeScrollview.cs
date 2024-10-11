using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[DisallowMultipleComponent]
public class HomeScrollview : MAMonoBehaviour
{
    public static HomeScrollview Inst;

    const string TAG = "HomeScrollview";

    [SerializeField] RectTransform _cachedTransform;
    [SerializeField] private Transform _contentTransform;
    private float _designHeight = 720;
    // Start is called before the first frame update

    private void Awake()
    {
        
    }

    private void OnDestroy()
    {
        LobbyCategoryButton.OnLobbyCategoryButtonClick = delegate { };
    }

    void Start()
    {
        _UpdateContentScale();
    }

    

    

#if UNITY_EDITOR
    private void OnRectTransformDimensionsChange()
    {
        //Debug.LogFormat("{0}->OnRectTransformDimensionsChange", TAG);
        _UpdateContentScale();
    }
#endif

    private void _UpdateContentScale()
    {
        float height = this.GetHeight();
        float scale = height / _designHeight;
        //_contentTransform.localScale = new Vector3(scale, scale, 1f);
    }

    private float GetHeight()
    {
        return _cachedTransform.rect.height;
    }

    

#if UNITY_EDITOR
    public override void GUIEditor()
    {
        GUILayout.Space(10);
        GUILayout.Label("HomeScrollview");
        if (GUILayout.Button("Print Height"))
        {
            float height = ((RectTransform)transform).rect.height;
            Debug.LogFormat("{0}- height {1} <=> {2}", name, height, Screen.height);
        }

        if (GUILayout.Button("Scale Content"))
        {
            _UpdateContentScale();
        }
    }
#endif
}
