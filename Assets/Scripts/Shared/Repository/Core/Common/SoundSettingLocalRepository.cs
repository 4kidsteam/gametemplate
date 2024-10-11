using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSettingLocalRepository : BoolRepositoryImpl
{
    public static readonly SoundSettingLocalRepository DefaultInstance = new SoundSettingLocalRepository();
    public SoundSettingLocalRepository(bool defaultValue = true) : base("Sound", defaultValue)
    {
    }
}
