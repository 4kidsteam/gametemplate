using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSettingLocalRepository : BoolRepositoryImpl
{
    public static readonly MusicSettingLocalRepository DefaultInstance = new MusicSettingLocalRepository();
    public MusicSettingLocalRepository(bool defaultValue = true) : base("Music", defaultValue)
    {
    }
}
