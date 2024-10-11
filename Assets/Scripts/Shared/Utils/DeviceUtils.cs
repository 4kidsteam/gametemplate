using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DeviceUtils
{
    public static bool IsTablet()
    {
        float diagonalInches = 4;

        if (Screen.dpi > 0)
        {
            float screenWidth = Screen.width / Screen.dpi;
            float screenHeight = Screen.height / Screen.dpi;
            diagonalInches = Mathf.Sqrt(Mathf.Pow(screenWidth, 2) + Mathf.Pow(screenHeight, 2));
        }

        var aspectRatio = Mathf.Max(Screen.width, Screen.height) / Mathf.Min(Screen.width, Screen.height);

        return (diagonalInches > 6.5f && aspectRatio < 1.7f);
    }

    public static float GetScreenRatio() => Screen.height * 1f / Screen.width;
    public static float GetPhoneScaleRatio() => Mathf.InverseLerp(1.77777778f, 2.16666667f, GetScreenRatio());//Full HD => LG G7

    public static bool IsLowEndDevice()
    {
        return SystemInfo.systemMemorySize < 1024 * 3;
    }

}
