using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Extensions;
using Firebase.Analytics;

namespace Firebase
{
    public class Common : MonoBehaviour
    {
        static bool Initializing;
        static FirebaseApp App;

        public static bool InitSucceed;
        public static void Init()
        {
            if (Initializing || InitSucceed) return;

            Initializing = true;
            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
            {
                var dependencyStatus = task.Result;
                if (dependencyStatus == DependencyStatus.Available)
                {
                    // Create and hold a reference to your FirebaseApp,
                    // where app is a Firebase.FirebaseApp property of your application class.
                    App = FirebaseApp.DefaultInstance;

                    FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);

                    InitSucceed = true;

                    // Set a flag here to indicate whether Firebase is ready to use by your app.
                }
                else
                {
                    Debug.LogError(System.String.Format(
                      "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                    
                    // Firebase Unity SDK is not safe to use here.
                }

                Initializing = false;
            });
        }

        public virtual void Awake()
        {
            Init();
        }
    }

}
