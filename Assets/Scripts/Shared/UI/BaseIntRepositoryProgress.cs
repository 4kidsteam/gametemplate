using System.Collections.Generic;
using UnityEngine;

namespace Shared.UI
{
    public interface IBaseIntRepositoryProgress
    {
        int MaxValue { get; set; }
        IIntRepository Repository { get; }

        void SetRepository(IIntRepository repository);
        bool RemoveRepository();

        void Visualize();
    }

    public interface IBaseIntRepositoryProgressGroup
    {
        List<BaseIntRepositoryProgress> FollowingProgresses { get; }
    }

    public abstract class BaseIntRepositoryProgress : MonoBehaviour, IBaseIntRepositoryProgress, IBaseIntRepositoryProgressGroup
    {
        const string TAG = "BaseIntRepositoryProgress";

        protected int _maxValue;
        public int MaxValue
        {
            get => _maxValue;
            set
            {
                _maxValue = value;
                if (_repository != null) this.Visualize();

                if (_followingProgresses != null && _followingProgresses.Count > 0)
                {
                    foreach (var a in _followingProgresses) a.MaxValue = value;
                }
            }
        }

        protected IIntRepository _repository;
        public IIntRepository Repository => _repository;

        [SerializeField] private List<BaseIntRepositoryProgress> _followingProgresses;
        public List<BaseIntRepositoryProgress> FollowingProgresses => _followingProgresses;

        public void SetRepository(IIntRepository repository)
        {
            if (repository == null) throw new System.Exception(string.Format("{0}->SetRepository: ERROR: repository == null", TAG));
            if (this._repository == repository) return;
            if (this._repository != null) this.RemoveRepository();
            this._repository = repository;
            this._repository.onIntValueUpdated.AddListener(_OnValueUpdated);
            this.Visualize();

            if (_followingProgresses != null && _followingProgresses.Count > 0)
            {
                foreach (var a in _followingProgresses) a.SetRepository(repository);
            }
        }

        public bool RemoveRepository()
        {
            if (_repository == null) return false;
            this._repository.onIntValueUpdated.RemoveListener(_OnValueUpdated);
            this._repository = null;

            if (_followingProgresses != null && _followingProgresses.Count > 0)
            {
                foreach (var a in _followingProgresses) a.RemoveRepository();
            }

            return true;
        }

        private void _OnValueUpdated(int oldValue, int newValue)
        {
            this.Visualize();
        }

        public abstract void Visualize();
    }
}