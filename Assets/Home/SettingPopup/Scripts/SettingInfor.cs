using UnityEngine;

namespace SliBox.Setting
{


    public class SettingInfor
    {
        static void PlayerPrefsSetBool(string key, bool value)
        {
            PlayerPrefs.SetInt(key, value ? 1 : 0);
        }

        static bool PlayerPresGetBool(string key)
        {
            return PlayerPrefs.GetInt(key) == 1;
        }

        public static bool Music
        {
            get
            {
                return PlayerPrefs.GetInt("Music") == 0;
            }
            set
            {
                PlayerPrefs.SetInt("Music", value ? 0 : 1);
            }
        }
        public static bool Sound
        {
            get
            {
                return PlayerPrefs.GetInt("Sound") == 0;
            }
            set
            {
                PlayerPrefs.SetInt("Sound", value ? 0 : 1);
            }
        }
        public static bool Vibration
        {
            get
            {
                return PlayerPrefs.GetInt("Vibration") == 0;
            }
            set
            {
                PlayerPrefs.SetInt("Vibration", value ? 0 : 1);
            }
        }
    }
}