using UnityEngine;

public class IntRepositoryImpl : IIntRepository
{
    public static readonly int ZERO = 0;
    const string TAG = "IntRepositoryImpl";

    private string _name;
    public string Name => _name;

    private int _defaultValue;
    public int DefaultValue => _defaultValue;

    private IIntRepository.OnIntValueUpdated _onIntValueUpdated = new IIntRepository.OnIntValueUpdated();
    private IIntRepository.OnIntValueDecreased _onIntValueDecreased = new IIntRepository.OnIntValueDecreased();
    private IIntRepository.OnIntValueIncreased _onIntValueIncreased = new IIntRepository.OnIntValueIncreased();

    public IIntRepository.OnIntValueUpdated onIntValueUpdated => _onIntValueUpdated;
    public IIntRepository.OnIntValueDecreased onIntValueDecreased => _onIntValueDecreased;
    public IIntRepository.OnIntValueIncreased onIntValueIncreased => _onIntValueIncreased;


    public IntRepositoryImpl(string name, int defaultValue = 0)
    {
        this._name = name;
        this._defaultValue = defaultValue;
    }


    //------------------------------------------------------------
    private int _GetFromPlayerPref() => PlayerPrefs.GetInt(this.Name, _defaultValue);

    private void _SaveToPlayerPref(int val)
    {
        PlayerPrefs.SetInt(Name, val);
        PlayerPrefs.Save();
    }
    //------------------------------------------------------------
    public bool InitIfNotExisted(int value)
    {
        if (!PlayerPrefs.HasKey(this.Name))
        {
            _SaveToPlayerPref(value);
            return true;
        }
        else return false;
    }

    public int Get() => _GetFromPlayerPref();

    public void Set(int value) => this._SaveToPlayerPref(val: value);

    public bool SetIfLargeThanCurrentValue(int newValue)
    {
        int oldValue = this.Get();
        if (newValue > oldValue)
        {
            _SaveToPlayerPref(newValue);
            if (_onIntValueUpdated != null) _onIntValueUpdated.Invoke(oldValue, newValue);
            if (_onIntValueIncreased != null) _onIntValueIncreased.Invoke(oldValue, newValue);
            return true;
        }
        else return false;
    }

    public int AddMore(int more)
    {
        int oldValue = _GetFromPlayerPref();
        int newValue = oldValue + more;
        _SaveToPlayerPref(newValue);

        if (_onIntValueUpdated != null) _onIntValueUpdated.Invoke(oldValue, newValue);
        if (_onIntValueIncreased != null) _onIntValueIncreased.Invoke(oldValue, newValue);

        return newValue;
    }

    public int Minus(int less)
    {
        int oldValue = _GetFromPlayerPref();
        int newValue = oldValue - less;
        _SaveToPlayerPref(newValue);

        if (_onIntValueUpdated != null) _onIntValueUpdated.Invoke(oldValue, newValue);
        if (_onIntValueIncreased != null) _onIntValueDecreased.Invoke(oldValue, newValue);

        return newValue;
    }

    public bool IsGreaterThanEqual(int val)
    {
        int total = _GetFromPlayerPref();
        return total >= val;
    }

    public bool IsGreaterThanZERO()
    {
        int total = _GetFromPlayerPref();
        return total > ZERO;
    }

    public void Clear()
    {
        PlayerPrefs.DeleteKey(this.Name);
        PlayerPrefs.Save();
    }
}
