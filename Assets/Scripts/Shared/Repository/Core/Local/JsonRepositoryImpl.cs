using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class JsonRepositoryImpl<T> : IJsonRepository<T>
{
    const string TAG = "JsonRepositoryImpl";

    private string _name;
    public string Name => _name;

    public JsonRepositoryImpl(string name, object defaultObj)
    {
        _name = name;
        if (defaultObj != null) this.InitIfNotExisted(defaultObj);
    }
    // -----------------------------------------------
    private string _GetFromPlayerPref()
    {
        return PlayerPrefs.GetString(this.Name, null);
    }

    private void _SaveToPlayerPref(string val)
    {
        PlayerPrefs.SetString(Name, val);
        PlayerPrefs.Save();
    }
    // -----------------------------------------------

    public bool InitIfNotExisted(object value)
    {
        if (!PlayerPrefs.HasKey(this.Name))
        {
            Save(value);
            return true;
        }
        else return false;
    }

    public T Get()
    {
        string str = _GetFromPlayerPref();
        if (string.IsNullOrEmpty(str)) return default(T);
        return JsonConvert.DeserializeObject<T>(str);
    }

    public void Save(object ob)
    {
        string str = JsonConvert.SerializeObject(ob);
        this._SaveToPlayerPref(str);
    }

    public bool IsExisted()
    {
        string str = _GetFromPlayerPref();
        return !string.IsNullOrEmpty(str);
    }

    public void Clear()
    {
        PlayerPrefs.DeleteKey(this.Name);
        PlayerPrefs.Save();
    }    
}
