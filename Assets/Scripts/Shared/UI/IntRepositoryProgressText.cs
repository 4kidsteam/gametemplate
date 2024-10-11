using TMPro;
using UnityEngine;

namespace Shared.UI
{
    [DisallowMultipleComponent]
    public class IntRepositoryProgressText : BaseIntRepositoryProgress
    {
        public interface ITextFormater
        {
            string Format(int currentValue, int maxValue);
        }

        public class DefaultTextFormater : ITextFormater
        {
            const string F = "{0}/{1}";

            public string Format(int currentValue, int maxValue)
            {
                return string.Format(F, currentValue, maxValue);
            }
        }

        //const string TAG = "ProgressText";
        [SerializeField] private TextMeshProUGUI _text;

        private ITextFormater _textFormater;
        public ITextFormater TextFormater
        {
            get => _textFormater;
            set => _textFormater = value;
        }

        public override void Visualize()
        {
            int value = this._repository.Get();
            if (_textFormater != null) _textFormater = new DefaultTextFormater();
            this._text.text = _textFormater.Format(value, _maxValue);
        }
    }
}