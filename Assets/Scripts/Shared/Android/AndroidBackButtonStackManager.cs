using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Shared.Android
{
    public interface IAndroidBackButtonHandler
    {
        string HandlerName { get; }
        void HandleAndroidBackButton();
    }

    public interface IAndroidBackButtonStackManager
    {
        IAndroidBackButtonStackManager Add(IAndroidBackButtonHandler handler);
        bool Remove(IAndroidBackButtonHandler handler);
    }

    [DisallowMultipleComponent]
    public class AndroidBackButtonStackManager : MonoBehaviour, IAndroidBackButtonStackManager
    {
        const string TAG = "AndroidBackButtonStackManager";

        private static AndroidBackButtonStackManager _instance;
        public static AndroidBackButtonStackManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    GameObject go = new GameObject(TAG);
                    DontDestroyOnLoad(go);
                    _instance = go.AddComponent<AndroidBackButtonStackManager>();
                }
                return _instance;
            }
        }

        private List<IAndroidBackButtonHandler> _handlers = new List<IAndroidBackButtonHandler>();
        private bool _isSupportedPlatform = Application.platform == RuntimePlatform.Android;

        private IAndroidBackButtonHandler _quitAppHandler;
        public IAndroidBackButtonHandler QuitAppHandler
        {
            get => _quitAppHandler;
            set => _quitAppHandler = value;
        }

        public void Init()
        {
            
        }

        public IAndroidBackButtonStackManager Add(IAndroidBackButtonHandler handler)
        {
#if LOG_INFO
            Debug.LogFormat("{0}->Add {1}", TAG, handler.HandlerName);
#endif
            _handlers.Remove(handler);// Remove if existed
            _handlers.Insert(0, handler);
            return this;
        }

        public bool Remove(IAndroidBackButtonHandler handler)
        {
#if LOG_INFO
            Debug.LogFormat("{0}->Remove {1}", TAG, handler.HandlerName);
#endif
            return _handlers.Remove(handler);
        }

#if UNITY_ANDROID
        void Update()
        {
            if (_isSupportedPlatform)
            {
                if (Input.GetKeyUp(KeyCode.Escape))
                {
                    _OnAndroidBackButtonEvent();
                }

            }
        }
#endif
        private void _OnAndroidBackButtonEvent()
        {
#if LOG_INFO
            Debug.LogFormat("{0}->_OnAndroidBackButtonEvent", TAG);
#endif
            if (_handlers.Count > 0)
            {
                var firstHandler = _handlers[0];
                _handlers.RemoveAt(0);
#if LOG_INFO
                Debug.LogFormat("{0}->Handle: {1}", TAG, firstHandler.HandlerName);
#endif
                firstHandler.HandleAndroidBackButton();
            }
            else if (_quitAppHandler != null)
            {
                _quitAppHandler.HandleAndroidBackButton();
            }
        }
    }
}