using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Shared.Android
{
    public interface IAndroidDefaultSharedPreferences
    {
        string GetString(string key, string defaultValue = null);
        int GetInt(string key, int defaultValue = 0);

        void SetString(string key, string value);
    }

    public class AndroidDefaultSharedPreferences : IAndroidDefaultSharedPreferences
    {
        const string TAG = "AndroidDefaultSharedPreferences";

        private static AndroidDefaultSharedPreferences _instance;
        public static AndroidDefaultSharedPreferences Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new AndroidDefaultSharedPreferences();
                }
                return _instance;
            }
        }

        public int GetInt(string key, int defaultValue = 0)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            AndroidJavaClass preferenceManagerClass = new AndroidJavaClass("android.preference.PreferenceManager");
            AndroidJavaObject sharedPreferences = preferenceManagerClass.CallStatic<AndroidJavaObject>("getDefaultSharedPreferences", currentActivity);
            return sharedPreferences.Call<int>("getInt", key, defaultValue);
#else
            Debug.LogWarningFormat("{0} - This function donot support this platform.", TAG);
            return 0;
#endif
        }

        public string GetString(string key, string defaultValue = null)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            AndroidJavaClass preferenceManagerClass = new AndroidJavaClass("android.preference.PreferenceManager");
            AndroidJavaObject sharedPreferences = preferenceManagerClass.CallStatic<AndroidJavaObject>("getDefaultSharedPreferences", currentActivity);
            return sharedPreferences.Call<string>("getString", key, defaultValue);
#else
            Debug.LogWarningFormat("{0} - This function donot support this platform.", TAG);
            return null;
#endif
        }

        /// 2023-06-04 22:54:26.696 30354-30354/com.indiez.nonogram E/Unity: AndroidJavaException: java.lang.NoSuchFieldError: no "Ljava/lang/Object;" field "edit" in class "Landroid/app/SharedPreferencesImpl;" or its superclasses
        ///java.lang.NoSuchFieldError: no "Ljava/lang/Object;" field "edit" in class "Landroid/app/SharedPreferencesImpl;" or its superclasses
        public void SetString(string key, string value)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            AndroidJavaClass preferenceManagerClass = new AndroidJavaClass("android.preference.PreferenceManager");
            AndroidJavaObject sharedPreferences = preferenceManagerClass.CallStatic<AndroidJavaObject>("getDefaultSharedPreferences", currentActivity);
            AndroidJavaObject editor = sharedPreferences.Call<AndroidJavaObject>("edit");
            editor.Call<AndroidJavaObject>("putString", key, value);
            //editor.Call("apply");
            var result = editor.Call<bool>("commit");
#if LOG_INFO
            Debug.LogFormat("{0} - SetString {1} - {2} {3}", TAG, key, value, result);
#endif
#else

            Debug.LogWarningFormat("{0} - This function donot support this platform.", TAG);
#endif
        }
    }
}