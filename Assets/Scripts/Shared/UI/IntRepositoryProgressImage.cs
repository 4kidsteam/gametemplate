using UnityEngine;
using UnityEngine.UI;

namespace Shared.UI
{
    [DisallowMultipleComponent]
    public class IntRepositoryProgressImage : BaseIntRepositoryProgress
    {
        //const string TAG = "ProgressImage";
        [SerializeField] private Image _image;

        public override void Visualize()
        {
            float a = _repository.Get();
            float b = _maxValue;
            _image.fillAmount = a / b;
        }
    }
}