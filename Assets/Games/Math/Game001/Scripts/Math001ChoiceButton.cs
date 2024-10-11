using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Math.Game001
{
    public class Math001ChoiceButton : MonoBehaviour
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
            if (Math001Controller.inst.gameState != Math001Controller.GameState.Choosing) return;

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
            Math001AudioManager.Inst.PlayOneShot("right_bell");
            Math001AudioManager.Inst.PlayOneShot("exactly" + Random.Range(0, 5));
            Math001Controller.inst.OwlTalk(1.2f);
            Math001Controller.inst.EndGame();
        }


        void Incorrect()
        {
            Math001AudioManager.Inst.PlayOneShot("wrong_bell");
            
            Math001AudioManager.Inst.PlayOneShot(Random.Range(0, 2) == 0 ? "try_again" : "its_wrong");
            Math001Controller.inst.OwlTalk(1.2f);
            graphic.gameObject.SetActive(false);
        }
    }
}

