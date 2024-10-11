using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEditor;
using UnityEngine;

namespace Math.Game003
{
    public enum AudioClipType
    {
        Sound,
        Music,
    }
    [System.Serializable]
    public class Sound
    {
        public string name;
        [HideInInspector] public AudioSource audioSource;
        public AudioClip clip;
        public AudioClipType type;
        [Range(0, 1f)] public float volume = 1;

    }
    public class Math003AudioManager : MonoBehaviour
    {

        public static Math003AudioManager Inst;
        public static float MusicVolume
        {
            set { PlayerPrefs.SetFloat("MusicVolume", value); }
            get { return PlayerPrefs.GetFloat("MusicVolume"); }
        }

        public static float SoundVolume
        {
            set { PlayerPrefs.SetFloat("SoundVolume", value); }
            get
            {
                return PlayerPrefs.GetFloat("SoundVolume");
            }
        }

        public List<Sound> sounds;

        AudioSource playOneShotAudioSource;

        public void Awake()
        {
            Inst = this;
            SetDefault();
            InitPlayOneShotAudioSource();
        }

        public void InitPlayOneShotAudioSource()
        {
            playOneShotAudioSource = gameObject.AddComponent<AudioSource>();
        }

        private void OnDestroy()
        {
            Inst = null;
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

        public void PlayOneShot(string name)
        {
            Sound sound = sounds.Find(x => (x.name == name || (x.name == "" && x.clip.name == name)));
            if (sound == null)
            {
#if UNITY_EDITOR
                Debug.LogWarning("Not Found Sound " + name);
#endif
            }
            else
            {
                PlayOneShot(sound.clip);
            }
        }

        public void PlayOneShot(AudioClip clip)
        {
            playOneShotAudioSource.volume = SoundVolume;
            playOneShotAudioSource.PlayOneShot(clip);
        }

        public void Play(string name, bool loop = false)
        {
            Sound sound = sounds.Find(x => x.name == name || (x.name == "" && x.clip.name == name));
            if (sound == null)
            {
#if UNITY_EDITOR
                Debug.LogWarning("Not Found Sound " + name);
#endif
            }
            else
            {
                if (sound.audioSource == null)
                {
                    sound.audioSource = gameObject.AddComponent<AudioSource>();
                }
                SetVolume(sound);
                sound.audioSource.clip = sound.clip;
                sound.audioSource.loop = loop;
                sound.audioSource.Play();
            }
        }

        public void Play(AudioClip clip, bool loop = false)
        {
            Sound sound = sounds.Find(x => x.clip == clip);
            if (sound == null)
            {
                Sound newSound = new Sound();
                newSound.clip = clip;
                newSound.volume = 1;
                newSound.type = loop ? newSound.type = AudioClipType.Music : AudioClipType.Sound;
                sound = newSound;
                sounds.Add(newSound);
            }

            if (sound.audioSource == null)
            {
                sound.audioSource = gameObject.AddComponent<AudioSource>();
            }
            SetVolume(sound);
            sound.audioSource.clip = sound.clip;
            sound.audioSource.loop = loop;
            sound.audioSource.Play();
        }

        void SetVolume(Sound sound)
        {
            sound.audioSource.volume = (sound.type == AudioClipType.Music ? MusicVolume : SoundVolume) * sound.volume;
            sound.audioSource.mute = sound.audioSource.volume == 0;
        }

        public void Stop(string name)
        {
            Sound sound = sounds.Find(x => x.name == name || (x.name == "" && x.clip.name == name));
            if (sound == null)
            {
#if UNITY_EDITOR
                Debug.LogWarning("Not Found Sound " + name);
# endif
            }
            else if (sound.audioSource != null)
            {
                sound.audioSource.Stop();
            }
        }


        public void OnInspectorChanged()
        {
            if (sounds == null) return;
            foreach (var sound in sounds)
            {
                if (sound.name == "" && sound.clip != null)
                {
                    sound.name = sound.clip.name;
                }
            }
        }

    }

#if UNITY_EDITOR
    [UnityEditor.CustomEditor(typeof(Math003AudioManager), true)]
    public class Math003AudioManagerInspector : Editor
    {
        Math003AudioManager targetClass;

        public void OnEnable()
        {
            targetClass = (Math003AudioManager)target;
        }

        private void OnDisable()
        {
            try
            {
                UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(targetClass.gameObject.scene);
            }
            catch { }
        }


        public override void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();
            base.OnInspectorGUI();
            if (EditorGUI.EndChangeCheck())
            {
                targetClass.OnInspectorChanged();
            }
        }
    }
#endif
}

