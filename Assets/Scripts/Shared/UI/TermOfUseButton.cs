using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TermOfUseButton : MonoBehaviour
{
    const string TAG = "TermOfUseButton";

    public static string TermOfUseUrl;

    [SerializeField]
    private Button _btn;
    public Button Button => _btn ? _btn : this.GetComponent<Button>();

    void Start()
    {
        if (this.Button) this.Button.onClick.AddListener(OpenUrl);
    }

    private void OpenUrl()
    {
        if (string.IsNullOrEmpty(TermOfUseUrl)) throw new System.Exception(string.Format("{0}->OpenUrl: URL cannot be NULL or EMPTY.", TAG));
        Application.OpenURL(TermOfUseUrl);
    }
}
