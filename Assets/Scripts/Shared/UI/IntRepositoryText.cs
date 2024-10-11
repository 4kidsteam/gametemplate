using TMPro;
using UnityEngine;

namespace Shared.UI
{
    [DisallowMultipleComponent]
    public class IntRepositoryText : MonoBehaviour
    {
        public interface ITextFormater
        {
            string Format(int value);
        }

        const string TAG = "IntRepositoryText";
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private IIntRepository _repository;
        public IIntRepository Repository => _repository;

        private ITextFormater _textFormater;
        public ITextFormater TextFormater
        {
            get => _textFormater;
            set => _textFormater = value;
        }

        public void SetRepository(IIntRepository repository)
        {
            if (repository == null) throw new System.Exception(string.Format("{0}->SetRepository: ERROR: repository == null", TAG));
            if (this._repository == repository) return;
            if (this._repository != null) this.RemoveRepository();
            this._repository = repository;
            this._repository.onIntValueUpdated.AddListener(_OnValueUpdated);
            this._SetIntValue(this._repository.Get());
        }

        public bool RemoveRepository()
        {
            if (_repository == null) return false;
            this._repository.onIntValueUpdated.RemoveListener(_OnValueUpdated);
            this._repository = null;
            return true;
        }

        private void _OnValueUpdated(int oldValue, int newValue)
        {
            this._SetIntValue(newValue);
        }

        private void _SetIntValue(int value)
        {
            if (_textFormater != null) this._text.text = _textFormater.Format(value);
            else this._text.text = value.ToString();
        }

    }
}