using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Shared.UI
{
    [DisallowMultipleComponent]
    public class IntRepositoryProgressBar : BaseIntRepositoryProgress
    {
        [SerializeField] private Image _image;
        [SerializeField] private TextMeshProUGUI _text;

        private IntRepositoryProgressText.ITextFormater _textFormater;
        public IntRepositoryProgressText.ITextFormater TextFormater
        {
            get => _textFormater;
            set => _textFormater = value;
        }

        public override void Visualize()
        {
            float a = _repository.Get();
            float b = _maxValue;
            _image.fillAmount = a / b;

            if (_textFormater == null) _textFormater = new IntRepositoryProgressText.DefaultTextFormater();
            this._text.text = _textFormater.Format((int)a, _maxValue);
        }
    }
}
