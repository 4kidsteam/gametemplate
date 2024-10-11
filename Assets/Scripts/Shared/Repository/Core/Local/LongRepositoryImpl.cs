using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LongRepositoryImpl : ILongRepository
{
    const string TAG = "IntRepositoryImpl";

    private string _name;
    public string Name => _name;

    private long _defaultValue;
    public long DefaultValue => _defaultValue;

    private ILongRepository.OnValueUpdated _onValueUpdated = new ILongRepository.OnValueUpdated();
    private ILongRepository.OnValueDecreased _onValueDecreased = new ILongRepository.OnValueDecreased();
    private ILongRepository.OnValueIncreased _onValueIncreased = new ILongRepository.OnValueIncreased();

    public ILongRepository.OnValueUpdated onValueUpdated => _onValueUpdated;
    public ILongRepository.OnValueDecreased onValueDecreased => _onValueDecreased;
    public ILongRepository.OnValueIncreased onValueIncreased => _onValueIncreased;

    public LongRepositoryImpl(string name, long defaultValue = 0)
    {
        this._name = name;
        this._defaultValue = defaultValue;
    }


    //------------------------------------------------------------
    private long _GetFromPlayerPref()
    {
        string str = PlayerPrefs.GetString(this.Name, null);
        if (string.IsNullOrEmpty(str)) return _defaultValue; else return long.Parse(str);
    }

    private void _SaveToPlayerPref(long val)
    {
        PlayerPrefs.SetString(Name, "" + val);
        PlayerPrefs.Save();
    }
    //------------------------------------------------------------
    public bool InitIfNotExisted(long value)
    {
        if (!PlayerPrefs.HasKey(this.Name))
        {
            _SaveToPlayerPref(value);
            return true;
        }
        else return false;
    }

    public long Get() => _GetFromPlayerPref();

    public void Set(long value) => this._SaveToPlayerPref(val: value);

    public bool SetIfLargeThanCurrentValue(long newValue)
    {
        long oldValue = this.Get();
        if (newValue > oldValue)
        {
            _SaveToPlayerPref(newValue);
            return true;
        }
        else return false;
    }

    public long AddMore(long more)
    {
        long oldValue = _GetFromPlayerPref();
        long newValue = oldValue + more;
        _SaveToPlayerPref(newValue);

        if (_onValueUpdated != null) _onValueUpdated.Invoke(oldValue, newValue);
        if (_onValueIncreased != null) _onValueIncreased.Invoke(oldValue, newValue);

        return newValue;
    }

    public long Minus(long less)
    {
        long oldValue = _GetFromPlayerPref();
        long newValue = oldValue - less;
        _SaveToPlayerPref(newValue);

        if (_onValueUpdated != null) _onValueUpdated.Invoke(oldValue, newValue);
        if (_onValueIncreased != null) _onValueDecreased.Invoke(oldValue, newValue);

        return newValue;
    }

    public bool IsGreaterThanEqual(long val)
    {
        long total = _GetFromPlayerPref();
        return total >= val;
    }

    public void Clear()
    {
        PlayerPrefs.DeleteKey(this.Name);
        PlayerPrefs.Save();
    }
}
