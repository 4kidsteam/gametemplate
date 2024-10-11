using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Math.Game004
{
    public class Math004Controller : MonoBehaviour
    {
        public enum GameState
        {
            Begin,
            Choosing,
            End,
        }

        [HideInInspector] public int number1, number2;

        [SerializeField] Text textNumber1, textMinus, textNumber2, textEqual, textResult;

        [SerializeField] Math004ChoiceButton[] choiceButtons;

        [SerializeField] Math004Owl owl;
        [SerializeField] float talkDelay;

        [SerializeField] Math004CountObjGroup[] _countObjGroup;
        Math004CountObjGroup CountObjGroup
        {
            get { return _countObjGroup[number1 - 1]; }
        }

        [SerializeField]
        Sprite[] countObjSprites;

        [HideInInspector]public GameState gameState;


        public static Math004Controller inst;


        private void Awake()
        {
            inst = this;
            //GetComponent<Canvas>().worldCamera = Camera.main;
            DisableChoiceButtonGraphic();
            DisableText();
            DisableCountObjGroup();
            MiniGame.OnSplashScreenDone += NewGame;
            MiniGame.OnWellDoneScreenClosed += NewGame;
        }

        private void OnDestroy()
        {
            inst = null;
        }

        public void OwlTalk(float duration)
        {
            owl.Talk(duration);
        }

        void RandomNumber()
        {
            number1 = Random.Range(2, _countObjGroup.Length + 1);
            textNumber1.text = number1.ToString();
            number2 = Random.Range(1, number1);
            textNumber2.text = number2.ToString();
        }

        void RandomChoice()
        {
            List<int> resultValues = new List<int>();
            for(int i = 2; i < _countObjGroup.Length + 2; i++)
            {
                resultValues.Add(i);
            }

            int rightChoiceIndex = Random.Range(0, 3);
            choiceButtons[rightChoiceIndex].SetNumber(number1 - number2);
            choiceButtons[rightChoiceIndex].correct = true;
            resultValues.Remove(number1 - number2);

            for(int i = 0; i < choiceButtons.Length; i++)
            {
                if(i != rightChoiceIndex)
                {
                    int randomValue = resultValues[Random.Range(0, resultValues.Count)];
                    choiceButtons[i].SetNumber(randomValue);
                    choiceButtons[i].correct = false;
                    resultValues.Remove(randomValue);
                }
            }
        }

        void DisableChoiceButtonGraphic()
        {
            foreach (var button in choiceButtons)
            {
                HideObj(button.graphic.gameObject);
            }
        }



        void DisableText()
        {
            HideObj(textNumber1.gameObject);
            HideObj(textNumber2.gameObject);
            HideObj(textMinus.gameObject);
            HideObj(textEqual.gameObject);
            HideObj(textResult.gameObject);
        }

        void SetCountObjSprite()
        {
            Sprite spr = countObjSprites[Random.Range(0, countObjSprites.Length)];
            CountObjGroup.SetSprites(spr);
        }

        void DisableCountObjGroup()
        {
            foreach (var item in _countObjGroup)
            {
                item.gameObject.SetActive(false);
            }
        }

        void HideObj(GameObject obj)
        {
            obj.SetActive(false);
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localScale = Vector3.zero;
        }

        void ShowObj(GameObject obj, string soundName = "")
        {
            StartCoroutine(IEShowObj(obj, soundName));
        }

        IEnumerator IEShowObj(GameObject obj, string soundName = "")
        {
            if (soundName != "")
            {
                Math004AudioManager.Inst.PlayOneShot(soundName);
                OwlTalk(0.85f);
            }
            obj.SetActive(true);
            obj.transform.localScale = Vector3.zero;
            obj.transform.DOScale(Vector3.one * 1.1f, 0.6f);
            yield return new WaitForSeconds(0.6f);
            obj.transform.DOScale(Vector3.one, 0.2f);
        }

        public void NewGame()
        {
            gameState = GameState.Begin;
            RandomNumber();
            RandomChoice();
            DisableChoiceButtonGraphic();
            DisableText();
            SetCountObjSprite();
            DisableCountObjGroup();
            StartCoroutine(IEIntro());
        }

        IEnumerator IEIntro()
        {
            yield return new WaitForSeconds(talkDelay);

            ShowObj(textNumber1.gameObject, number1.ToString());  // Number 1
            Math004AudioManager.Inst.PlayOneShot("pop");

            CountObjGroup.gameObject.SetActive(true);

            CountObjGroup.ShowObjToCountAnim();

            yield return new WaitForSeconds(talkDelay);

            ShowObj(textMinus.gameObject, "minus");
            Math004AudioManager.Inst.PlayOneShot("pop");

            

            yield return new WaitForSeconds(talkDelay);

            CountObjGroup.ShowXMinusIcons(number2);
            ShowObj(textNumber2.gameObject, number2.ToString());  // Number 2
            Math004AudioManager.Inst.PlayOneShot("pop");



            yield return new WaitForSeconds(talkDelay);

            ShowObj(textEqual.gameObject, "equal");
            Math004AudioManager.Inst.PlayOneShot("pop");

            yield return new WaitForSeconds(talkDelay);

            ShowObj(textResult.gameObject);
            yield return new WaitForSeconds(talkDelay);

            for (int i = 0; i < choiceButtons.Length; i++)
            {
                ShowObj(choiceButtons[i].graphic.gameObject);
            }

            gameState = GameState.Choosing;
        }


        public void EndGame()
        {
            gameState = GameState.End;
            StartCoroutine(IEEndGame());
        }

        IEnumerator IEEndGame()
        {
            

            Math004ChoiceButton correctButton = null;
            for (int i = 0; i < choiceButtons.Length; i++)
            {
                Math004ChoiceButton button = choiceButtons[i];

                if (button.correct)
                {
                    correctButton = button;
                }
                else
                {
                    button.graphic.gameObject.SetActive(false);
                }
            }

            yield return new WaitForSeconds(1);

            correctButton.graphic.DOMove(textResult.rectTransform.position, 1);
            yield return new WaitForSeconds(1.5f);

            EndGameShowObj(textNumber1.gameObject, 0 * talkDelay, number1.ToString());

            EndGameShowObj(textMinus.gameObject, 1 * talkDelay, "minus");

            EndGameShowObj(textNumber2.gameObject, 2 * talkDelay, number2.ToString());

            EndGameShowObj(textEqual.gameObject, 3 * talkDelay, "equal");

            EndGameShowObj(correctButton.graphic.gameObject, 4 * talkDelay, (number1 - number2).ToString());

            yield return new WaitForSeconds(5 * talkDelay);
            Math004AudioManager.Inst.PlayOneShot("end");
            owl.Jump(3);
            MiniGame.OnWinGame.Invoke();

        }

        void EndGameShowObj(GameObject obj, float delay, string soundName = "")
        {
            StartCoroutine(IEEndGameShowObj(obj, delay, soundName));
        }

        IEnumerator IEEndGameShowObj(GameObject obj, float delay, string soundName = "")
        {
            yield return new WaitForSeconds(delay);
            if (soundName != "")
            {
                Math004AudioManager.Inst.PlayOneShot(soundName);
                OwlTalk(0.85f);
            }
            obj.transform.DOScale(Vector3.one * 1.2f, 0.35f);
            yield return new WaitForSeconds(0.6f);
            obj.transform.DOScale(Vector3.one, 0.35f);
        }
    }
}

