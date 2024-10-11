using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using SliBox.Setting;
using SliBoxEngine;

namespace SliBox.Popup.SettingSliderType
{


    public class SlideSettingButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        enum SlideOptionButtonType
        {
            Music,
            Sound,
            Vibration,
        }

        [SerializeField] SlideOptionButtonType slideOptionButtonType;
        [SerializeField] Slider slider;
        [SerializeField] Image handlerOn, bgOn;
        float OptionValue
        {
            get
            {
                if (slideOptionButtonType == SlideOptionButtonType.Music)
                {
                    return MusicVolume;
                }
                else if (slideOptionButtonType == SlideOptionButtonType.Sound)
                {
                    return SoundVolume;
                }
                else return 1;
            }
            set
            {
                if (slideOptionButtonType == SlideOptionButtonType.Music)
                {
                    MusicVolume = value;
                }
                else if (slideOptionButtonType == SlideOptionButtonType.Sound)
                {
                    SoundVolume = value;
                }
                else SettingInfor.Vibration = value > 0.5f ? true : false;
            }
        }

        public static float MusicVolume
        {
            set { Home.AudioManager.MusicVolume = value; }
            get { return PlayerPrefs.GetFloat("MusicVolume"); }
        }

        public static float SoundVolume
        {
            set { Home.AudioManager.SoundVolume = value; }
            get
            {
                return PlayerPrefs.GetFloat("SoundVolume");
            }
        }

        public static void SetDefault()
        {
            if (PlayerPrefs.GetInt("SetDefaultVolumeIsDone") == 0)
            {
                PlayerPrefs.SetInt("SetDefaultVolumeIsDone", 1);
                MusicVolume = 1;
                SoundVolume = 1;
            }
        }

        private void Awake()
        {
            SetDefault();
            slider.value = OptionValue;
            SetColor();
        }


        public void MoveHandler()
        {
            SetOptionValue();
            SetColor();
        }

        void SetOptionValue()
        {
            OptionValue = slider.value;
        }


        void SetColor()
        {
            handlerOn.color = new Color(1, 1, 1, slider.value);
            bgOn.color = new Color(1, 1, 1, slider.value);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            //StartCoroutine(CheckMouseUp());
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            SetOptionValue();
        }

        IEnumerator CheckMouseUp()
        {
            while (!Input.GetMouseButtonUp(0))
            {
                yield return null;
            }
            OptionValue = slider.value;
        }
    }
}

