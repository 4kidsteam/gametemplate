using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Math.Game003
{
    public class Math003GameManager : MonoBehaviour
    {
        public static Math003GameManager Inst;

        [SerializeField] float _ballDistance;
        [SerializeField] Vector2Int _ballArraySize;
        [SerializeField] Math003Balloon[] _balloons;
        [SerializeField] Math003IntroNumberController _introNumber;
        [SerializeField] Canvas _bgCanvas;
        [HideInInspector] public int GamePlayNumber;
        [HideInInspector] public bool GamePlaying;

        private void Awake()
        {
            Inst = this;
            _bgCanvas.worldCamera = Camera.main;
            SetPositionOfBalloons();
            HideAllBalloons();
            MiniGame.OnSplashScreenDone += NewGame;
            MiniGame.OnWellDoneScreenClosed += NewGame;
        }
        private void OnDestroy()
        {
            Inst = null;
        }
        void HideAllBalloons()
        {
            foreach (var balloon in _balloons)
            {
                balloon.Hide();
            }
        }
        void SetPositionOfBalloons()
        {
            Vector3 origin = new Vector3(-(_ballArraySize.x - 1) * _ballDistance / 2f, (_ballArraySize.y - 1) * _ballDistance / 2f, 0f);

            for (int row = 0; row < _ballArraySize.y; row++)
            {
                for (int col = 0; col < _ballArraySize.x; col++)
                {
                    int index = row * _ballArraySize.x + col;
                    if (index >= _balloons.Length) return;

                    Vector3 balloonPosition = origin + new Vector3(col * _ballDistance, -row * _ballDistance, 0f);

                    _balloons[index].transform.position = balloonPosition;
                }
            }
        }

        void NewGame()
        {
            StartCoroutine(IEShowNewGame());
        }

        IEnumerator IEShowNewGame()
        {
            
            for (int i = 0; i < _balloons.Length; i++)
            {
                _balloons[i].Show(i * 0.1f + 3);
            }
            GamePlayNumber = _balloons[Random.Range(0, _balloons.Length)].Number;

            _introNumber.Show(GamePlayNumber);
            yield return new WaitForSeconds(1);
            Math003AudioManager.Inst.PlayOneShot(GamePlayNumber.ToString());
            yield return new WaitForSeconds(2);
            Math003AudioManager.Inst.PlayOneShot("blink");
            yield return new WaitForSeconds(2);
            GamePlaying = true;
        }

        public void CheckWinGame()
        {
            foreach (var balloon in _balloons)
            {
                if (balloon.Number == GamePlayNumber && balloon.BalloonEnabled) return;

            }
            GamePlaying = false;
            StartCoroutine(IEWinGame());

        }

        IEnumerator IEWinGame()
        {
            yield return new WaitForSeconds(2);
            Math003AudioManager.Inst.Play("win");
            MiniGame.OnWinGame.Invoke();
        }
    }
}

