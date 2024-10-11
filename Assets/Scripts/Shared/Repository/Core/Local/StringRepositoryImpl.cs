using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StringRepositoryImpl : IStringRepository
{
    const string TAG = "StringRepositoryImpl";

    private string _name;
    public string Name => _name;

    private string _defaultValue;
    public string DefaultValue => _defaultValue;

    private IStringRepository.OnValueUpdated _onValueUpdated = new IStringRepository.OnValueUpdated();
    public IStringRepository.OnValueUpdated onValueUpdated => _onValueUpdated;

    public StringRepositoryImpl(string name, string defaultValue = "")
    {
        this._name = name;
        this._defaultValue = defaultValue;
    }

    // -------------------------------------------------
    private string GetFromPlayerPref() => PlayerPrefs.GetString(this.Name, _defaultValue);

    private void SaveToPlayerPref(string val)
    {
        PlayerPrefs.SetString(Name, val);
        PlayerPrefs.Save();
    }

    // -------------------------------------------------
    public bool InitIfNotExisted(string value)
    {
        if (!PlayerPrefs.HasKey(this.Name))
        {
            SaveToPlayerPref(value);
            return true;
        }
        else return false;
    }

    public string Get() => this.GetFromPlayerPref();

    public bool Set(string newValue)
    {
        string oldValue = Get();
        if (newValue.Equals(oldValue)) return false;
        else
        {
            SaveToPlayerPref(newValue);
            this._onValueUpdated.Invoke(oldValue, newValue);
            return true;
        }
    }

    public void Clear()
    {
        PlayerPrefs.DeleteKey(this.Name);
        PlayerPrefs.Save();
    }
}
