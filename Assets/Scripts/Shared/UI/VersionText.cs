using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Shared.UI
{
    [DisallowMultipleComponent]
    public class VersionText : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private bool _includeVersionCode = true;

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
            if (_includeVersionCode)
            {
                this.Text.text = string.Format("{0}({1})", Application.version, AndroidUtils.GetVersionCode().ToString());
            }
            else
            {
                this.Text.text = string.Format("{0}", Application.version);
            }
        }
    }
}