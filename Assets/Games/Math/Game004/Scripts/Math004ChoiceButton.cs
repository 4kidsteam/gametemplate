using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Math.Game004
{
    public class Math004ChoiceButton : MonoBehaviour
    {
        [HideInInspector] public int number;
        [HideInInspector] public bool correct;
        [SerializeField] Text numberText;
        public RectTransform graphic;

        public void SetNumber(int numberValue)
        {
            number = numberValue;
            numberText.text = numberValue.ToString();

        }

        public void ButtonClick()
        {
            if (Math004Controller.inst.gameState != Math004Controller.GameState.Choosing) return;

            if (correct)
            {
                Correct();
            }
            else
            {
                Incorrect();
            }
        }

        void Correct()
        {
            Math004AudioManager.Inst.PlayOneShot("right_bell");
            Math004AudioManager.Inst.PlayOneShot("exactly" + Random.Range(0, 5));
            Math004Controller.inst.OwlTalk(1.2f);
            Math004Controller.inst.EndGame();
        }


        void Incorrect()
        {
            Math004AudioManager.Inst.PlayOneShot("wrong_bell");
            
            Math004AudioManager.Inst.PlayOneShot(Random.Range(0, 2) == 0 ? "try_again" : "its_wrong");
            Math004Controller.inst.OwlTalk(1.2f);
            graphic.gameObject.SetActive(false);
        }
    }
}

