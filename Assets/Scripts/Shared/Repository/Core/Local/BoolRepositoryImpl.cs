using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoolRepositoryImpl : IBoolRepository
{
    public const int TRUE = 1;
    public const int FALSE = 0;

    private string _name;
    public string Name => _name;

    private IBoolRepository.OnValueChanged _onValueChanged = new IBoolRepository.OnValueChanged();
    public IBoolRepository.OnValueChanged onValueChanged => _onValueChanged;

    private bool _defaultValue = false;
    private int _realDefaultValue = FALSE;

    public BoolRepositoryImpl(string name, bool defaultValue = false)
    {
        _name = name;
        _defaultValue = defaultValue;
        _realDefaultValue = _defaultValue ? TRUE : FALSE;
    }

    // ------------------------------------------------------
    private bool _GetFromPlayerPref() => PlayerPrefs.GetInt(this.Name, _realDefaultValue) == TRUE;

    private void _SaveToPlayerPref(int val)
    {
        PlayerPrefs.SetInt(Name, val);
        PlayerPrefs.Save();
    }
    // ------------------------------------------------------

    public bool Get() => _GetFromPlayerPref();

    public void Set(bool val)
    {
        bool oldOne = Get();
        if (val != oldOne)
        {
            _SaveToPlayerPref(val ? TRUE : FALSE);
            onValueChanged.Invoke(val);
        }
    }
}
