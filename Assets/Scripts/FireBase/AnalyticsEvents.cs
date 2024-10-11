using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Extensions;
using Firebase.Analytics;

namespace Firebase
{
    public class AnalyticsEvents : Common
    {
        public static AnalyticsEvents Inst;

        public override void Awake()
        {
            base.Awake();
            Inst = this;

        }

        public static void TrackMiniGamePlayed(string gameName, float duration, int completedAmount)
        {

            System.Action action = () =>
            {
#if LOG_INFO
                Debug.Log("TrackMiniGamePlayed " + gameName + ", duration: " + duration + "s, completed: " + completedAmount);
#endif
                FirebaseAnalytics.LogEvent(
                    "minigame_played",
                    new Parameter("game_name", gameName),
                    new Parameter("duration", duration),
                    new Parameter("completed", completedAmount)
                );
            };

            Inst.StartCoroutine(IEActionOnInitSucceed(action));


        }

        public static void TrackMiniGameLoadTime(string gameName, float loadTime)
        {
            System.Action action = () =>
            {


#if LOG_INFO
                Debug.Log(("TrackMiniGameLoadTime " + gameName + ", time: " + loadTime + "s"));
#endif
                FirebaseAnalytics.LogEvent(
                    "minigame_load_time",
                    new Parameter("game_name", gameName),
                    new Parameter("load_time", loadTime)
                );
            };

            Inst.StartCoroutine(IEActionOnInitSucceed(action));

        }

        static IEnumerator IEActionOnInitSucceed(System.Action action)
        {
            while (!InitSucceed) yield return null;

            action?.Invoke();
        }
    }
}