using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class PrivacyButton : MonoBehaviour
{
    const string TAG = "PrivacyButton";

    public static string PrivacyUrl;

    [SerializeField]
    private Button _btn;
    public Button Button => _btn ? _btn : this.GetComponent<Button>();

    void Start()
    {
        if (this.Button) this.Button.onClick.AddListener(OpenUrl);
    }

    private void OpenUrl()
    {
        if (string.IsNullOrEmpty(PrivacyUrl)) throw new System.Exception(string.Format("{0}->OpenUrl: URL cannot be NULL or EMPTY.", TAG));
        Application.OpenURL(PrivacyUrl);
    }
}
