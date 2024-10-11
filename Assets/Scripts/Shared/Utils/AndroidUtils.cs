using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AndroidUtils
{
    public static int GetVersionCode()
    {
#if PLATFORM_ANDROID
        if (Application.isEditor) return 0;
        AndroidJavaClass contextCls = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject context = contextCls.GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaObject packageMngr = context.Call<AndroidJavaObject>("getPackageManager");
        string packageName = context.Call<string>("getPackageName");
        AndroidJavaObject packageInfo = packageMngr.Call<AndroidJavaObject>("getPackageInfo", packageName, 0);
        return packageInfo.Get<int>("versionCode");
#else
        return 0;
#endif
    }

}
