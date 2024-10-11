using System;
using System.Collections;
using System.Collections.Generic;
using Shared.Repository;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace Shared.Utils
{
    [DisallowMultipleComponent]
    public class SessionMonitor : MonoBehaviour
    {
        [System.Serializable]
        public class OnFirstSessionEvent : UnityEvent<long> { }

        [System.Serializable]
        public class OnNewSessionEvent : UnityEvent<long> { }

        private const float SESSION_REQUIRED_MINUTES = 30;

        const string TAG = "SessionMonitor";
        
        private static SessionMonitor _instance;
        public static SessionMonitor Instance
        {
            get
            {
                if (_instance == null)
                {
                    GameObject go = new GameObject(TAG);
                    _instance = go.AddComponent<SessionMonitor>();
                    DontDestroyOnLoad(go);
                }
                return _instance;
            }
        }

        public readonly OnFirstSessionEvent onFirstSessionEvent = new OnFirstSessionEvent();
        public readonly OnNewSessionEvent onNewSessionEvent = new OnNewSessionEvent();

        private IIntRepository _counterRepository = SessionCounterLocalRepository.DefaultInstance;
        private DateTime _lastActiveDatetime;

        public void StartMonitor()
        {
            _lastActiveDatetime = DateTime.Now;
            int count = _counterRepository.AddMore(1);
            this.onNewSessionEvent.Invoke(count);
            this.onFirstSessionEvent.Invoke(count);
        }

        private void OnApplicationPause(bool pause)
        {
#if UNITY_EDITOR || LOG_INFO
            Debug.LogFormat("{0}->OnApplicationPause: {1}", TAG, pause);
#endif
            if (pause) _lastActiveDatetime = DateTime.Now;
            else this._CalculateSession();
        }

        private void _CalculateSession()
        {
            TimeSpan timeSpan = DateTime.Now - _lastActiveDatetime;
            if (timeSpan.TotalMinutes >= SESSION_REQUIRED_MINUTES)
            {
                //Debug.LogFormat("{0}->_CalculateSession::SessionCounter:: Increase by 1", TAG);
                _lastActiveDatetime = DateTime.Now;
                int count = this._counterRepository.AddMore(1);
                this.onNewSessionEvent.Invoke(count);
            }
        }
    }
}