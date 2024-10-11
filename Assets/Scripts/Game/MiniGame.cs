using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class MiniGame : MonoBehaviour
{

    public static MiniGame Inst;

    [SerializeField] float _timeScale;


    [SerializeField] GameObject _homeView;
    [SerializeField] GameObject[] _welldoneScreenObjs;
    [SerializeField] SkeletonGraphic _splashScreenAnim;
    [SerializeField] AudioClip _splashScreenSoundEffect;
    public static System.Action OnBackButtonClick = delegate { ShowSplashScreen(DestroyMiniGame, true); HideWelldoneScreen();};
    public static System.Action OnWinGame = delegate { ShowWellDoneScreen(); };
    public static System.Action OnWellDoneScreenClosed = delegate { HideWelldoneScreen(); };
    public static System.Action OnSplashScreenDone = delegate { };

    public static bool SplashScreenLoadFinished;

    public static GameObject CurrentMiniGameObj;

    static int s_completedAmount;
    static float s_gameTimer;
    static float s_miniGameStartTime;

    static float MiniGamePlayedDuration => s_gameTimer - s_miniGameStartTime;

    

    private void Awake()
    {
        Inst = this;

    }

    private void Update()
    {
        Time.timeScale = _timeScale;
        s_gameTimer += Time.unscaledDeltaTime;
    }
    public static void InstantiateMiniGamePrefab(GameObject miniGamePrefab, Transform gameParent)
    {
        StopGroundSoundEffect();

        if (CurrentMiniGameObj != null) Destroy(CurrentMiniGameObj);
        CurrentMiniGameObj = Instantiate(miniGamePrefab);
        CurrentMiniGameObj.transform.SetParent(gameParent);
        TrackOnOpenMiniGame();
    }

    static void DestroyMiniGame()
    {
        TrackOnQuitMiniGame();
        PlayGroundSoundEffect();
        DestroyImmediate(CurrentMiniGameObj);
        ClearEvents();
        
        SplashScreenLoadFinished = true;
    }

    static void ClearEvents()
    {
        OnWinGame = ShowWellDoneScreen;
        OnBackButtonClick = delegate { ShowSplashScreen(DestroyMiniGame, true); HideWelldoneScreen(); };
        OnWellDoneScreenClosed = HideWelldoneScreen;
        OnSplashScreenDone = delegate { };
    }

    static void TrackOnOpenMiniGame()
    {
        if (CurrentMiniGameObj == null) return;
        s_completedAmount = 0;
        s_miniGameStartTime = s_gameTimer;
        string miniGameName = CurrentMiniGameObj.name.Replace("(Clone)", "");
        Firebase.AnalyticsEvents.TrackMiniGameLoadTime(miniGameName, s_miniGameStartTime);


        DateTime time = DateTime.Now;
        string timeStamp = time.Year + "/" + time.Month + "/" + time.Day + "-" + time.Hour + ":" + time.Minute + ":" + time.Second;
        string deviceName = SystemInfo.deviceName;

        string path = timeStamp + deviceName;

        Dictionary<string, object> data = new Dictionary<string, object>();
        data.Add("date_time", timeStamp);
        data.Add("user_id", deviceName);
        data.Add("game_time", s_miniGameStartTime);

        EventTracking.SetData(path, data);
    }

    static void TrackOnQuitMiniGame()
    {
        if (CurrentMiniGameObj == null) return;
        string miniGameName = CurrentMiniGameObj.name.Replace("(Clone)", "");
        Firebase.AnalyticsEvents.TrackMiniGamePlayed(miniGameName, MiniGamePlayedDuration, s_completedAmount);


        string path = "tracking_event." + miniGameName;

        Dictionary<string, object> data = new Dictionary<string, object>();
        data.Add("play_duration", MiniGamePlayedDuration);
        data.Add("completed_count", s_completedAmount);

        EventTracking.PushEvent(path, data);

        s_miniGameStartTime = s_gameTimer;
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            TrackOnQuitMiniGame();
        }
        else
        {
            TrackOnOpenMiniGame();
        }
    }

    #region WellDone Screen
    public static void ShowWellDoneScreen()
    {
        Inst.StartCoroutine(IEShowWelldoneScreen());
    }

    static IEnumerator IEShowWelldoneScreen()
    {
        Inst._welldoneScreenObjs[UnityEngine.Random.Range(0, Inst._welldoneScreenObjs.Length)].SetActive(true);
        s_completedAmount++;
        yield return new WaitForSeconds(3);
        OnWellDoneScreenClosed.Invoke();
    }

    static void HideWelldoneScreen()
    {
        foreach (var welldoneScreenObj in Inst._welldoneScreenObjs)
        {
            welldoneScreenObj.gameObject.SetActive(false);
        }
        Inst.StopCoroutine(IEShowWelldoneScreen());
    }
    #endregion

    #region SplashScreen



    public static void ShowSplashScreen(Action loadAction, bool enableHomeView)
    {
        SplashScreenLoadFinished = false;
        Home.AudioManager.Inst.Play(Inst._splashScreenSoundEffect);
        Inst.StartCoroutine(IEShowSplashScreen(loadAction, enableHomeView));
    }

    static IEnumerator IEShowSplashScreen(Action loadAction, bool enableHomeView)
    {
        SkeletonGraphic splashScreenGraphic = Inst._splashScreenAnim;
        var anim = splashScreenGraphic.SkeletonDataAsset.GetSkeletonData(true).Animations.Items[0];
        splashScreenGraphic.AnimationState.SetAnimation(0, anim, false).TrackTime = 0;
        splashScreenGraphic.gameObject.SetActive(true);

        yield return new WaitForSeconds(anim.Duration * 0.5f);

        Inst._homeView.SetActive(enableHomeView);

        if (loadAction != null) loadAction.Invoke();

        while (!SplashScreenLoadFinished)
        {
            splashScreenGraphic.timeScale *= 0.9f;
            yield return null;
        }

        if (CurrentMiniGameObj == false) Inst._homeView.SetActive(true);

        splashScreenGraphic.timeScale += 0.1f;
        while (!splashScreenGraphic.AnimationState.GetCurrent(0).IsComplete)
        {
            if (splashScreenGraphic.timeScale < 1) splashScreenGraphic.timeScale *= 1.25f;
            else splashScreenGraphic.timeScale = 1;
            yield return null;
        }

        splashScreenGraphic.gameObject.SetActive(false);

        OnSplashScreenDone.Invoke();
    }

    #endregion

    #region Ground Sound Effect

    static void PlayGroundSoundEffect()
    {
        Home.AudioManager.Inst.PlayGroundSoundEffect();
    }

    static void StopGroundSoundEffect()
    {
        Home.AudioManager.Inst.StopGroundSoundEffect();
    }

    #endregion
}
