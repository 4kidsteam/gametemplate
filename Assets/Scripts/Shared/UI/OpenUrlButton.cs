using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class OpenUrlButton : MonoBehaviour
{
    const string TAG = "OpenUrlButton";

    [SerializeField]
    private Button _btn;
    public Button Button => _btn ? _btn : this.GetComponent<Button>();

    // https://indiez.net/privacy
    // https://indiez.net/tos
    [SerializeField] private string _url;
    
    void Start()
    {
        if (this.Button) this.Button.onClick.AddListener(OpenUrl);
    }

    private void OpenUrl()
    {
        if (string.IsNullOrEmpty(_url)) throw new System.Exception(string.Format("{0}->OpenUrl: URL cannot be NULL or EMPTY.", TAG));
        Application.OpenURL(_url);
    }
}
