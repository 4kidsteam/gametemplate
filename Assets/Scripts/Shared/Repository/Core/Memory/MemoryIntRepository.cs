
namespace Shared.Repository.Memory
{
    public class MemoryIntRepository : IIntRepository
    {
        public static readonly int ZERO = 0;
        const string TAG = "MemoryIntRepository";

        private string _name;
        public string Name => _name;

        private int _defaultValue;
        public int DefaultValue => _defaultValue;

        private int _value;

        private IIntRepository.OnIntValueUpdated _onIntValueUpdated = new IIntRepository.OnIntValueUpdated();
        private IIntRepository.OnIntValueDecreased _onIntValueDecreased = new IIntRepository.OnIntValueDecreased();
        private IIntRepository.OnIntValueIncreased _onIntValueIncreased = new IIntRepository.OnIntValueIncreased();

        public IIntRepository.OnIntValueUpdated onIntValueUpdated => _onIntValueUpdated;
        public IIntRepository.OnIntValueDecreased onIntValueDecreased => _onIntValueDecreased;
        public IIntRepository.OnIntValueIncreased onIntValueIncreased => _onIntValueIncreased;


        public MemoryIntRepository(string name, int defaultValue = 0)
        {
            this._name = name;
            this._defaultValue = defaultValue;
            this._value = defaultValue;
        }

        public bool InitIfNotExisted(int value)
        {
            this._value = value;
            return true;
        }

        public int Get() => this._value;

        public void Set(int newValue)
        {
            if (this._value != newValue)
            {
                int oldValue = this._value;
                this._value = newValue;
                if (_onIntValueUpdated != null) _onIntValueUpdated.Invoke(oldValue, newValue);
            }
        }

        public bool SetIfLargeThanCurrentValue(int newValue)
        {
            int oldValue = this.Get();
            if (newValue > oldValue)
            {
                this._value = newValue;
                if (_onIntValueUpdated != null) _onIntValueUpdated.Invoke(oldValue, newValue);
                if (_onIntValueIncreased != null) _onIntValueIncreased.Invoke(oldValue, newValue);
                return true;
            }
            else return false;
        }

        public int AddMore(int more)
        {
            int oldValue = this.Get();
            int newValue = oldValue + more;
            this._value = newValue;

            if (_onIntValueUpdated != null) _onIntValueUpdated.Invoke(oldValue, newValue);
            if (_onIntValueIncreased != null) _onIntValueIncreased.Invoke(oldValue, newValue);

            return newValue;
        }

        public int Minus(int less)
        {
            int oldValue = Get();
            int newValue = oldValue - less;
            this._value = newValue;

            if (_onIntValueUpdated != null) _onIntValueUpdated.Invoke(oldValue, newValue);
            if (_onIntValueIncreased != null) _onIntValueDecreased.Invoke(oldValue, newValue);

            return newValue;
        }

        public bool IsGreaterThanEqual(int val)
        {
            int total = Get();
            return total >= val;
        }

        public bool IsGreaterThanZERO()
        {
            int total = Get();
            return total > ZERO;
        }

        public void Clear()
        {
            this._value = _defaultValue;
        }
    }
}