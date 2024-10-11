using TMPro;
using UnityEngine;

[DisallowMultipleComponent]
public class VersionCodeText : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    public TMP_Text Text
    {
        get
        {
            if (_text == null)
            {
                _text = GetComponent<TMP_Text>();
            }
            return _text;
        }
    }

    void Start()
    {
        this.Text.text = AndroidUtils.GetVersionCode().ToString();    
    }
}
