using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VibrationSettingLocalRepository : BoolRepositoryImpl
{
    public static readonly VibrationSettingLocalRepository DefaultInstance = new VibrationSettingLocalRepository();
    public VibrationSettingLocalRepository(bool defaultValue = true) : base("Vibration", defaultValue)
    {
    }
}
