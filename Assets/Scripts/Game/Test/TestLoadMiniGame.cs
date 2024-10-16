using SliBoxEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using SliBox.CustomInspector;

#if UNITY_EDITOR

using UnityEditor;

#endif

namespace Test
{
    public class TestLoadMiniGame : ButtonClick, IOnInspectorChanged
    {
        public static GameObject currentMiniGameObj;

        [HideInInspector] public string miniGamePrefabPath; // load Resources
        [SerializeField] UnityEvent pointerHandlerEvent;

#if UNITY_EDITOR

        public GameObject miniGamePrefab;

        public void OnInspectorChanged()
        {
            GetMiniGamePrefabPath();

            
        }

        void GetMiniGamePrefabPath()
        {
            if (miniGamePrefab == null)
            {
                miniGamePrefabPath = "";
            }
            else
            {
                miniGamePrefabPath = GetObjectPathInResourcesFolder(miniGamePrefab);
            }
        }

        string GetObjectPathInResourcesFolder(Object obj)
        {
            string path = AssetDatabase.GetAssetPath(obj);

            int resourcesIndex = path.IndexOf("Resources/");
            if (resourcesIndex >= 0)
            {
                path = path.Substring(resourcesIndex + "Resources/".Length);

                int dotIndex = path.LastIndexOf('.');
                if (dotIndex >= 0)
                {
                    path = path.Substring(0, dotIndex);
                }

                Debug.Log("Resources/" + path);
                
            }
            else
            {
                Debug.LogWarning("Object Is Not Contain In Resources Folder.");
            }
            return path;
        }
#endif

        public void LoadMiniGame()
        {
            DestroyMiniGame();
            currentMiniGameObj = (GameObject)Instantiate(Resources.Load(miniGamePrefabPath));
            MiniGame.OnBackButtonClick += EnableHome;
            MiniGame.OnBackButtonClick += DestroyMiniGame;
            StartCoroutine(IECloseSplashScreen());
        }

        IEnumerator IECloseSplashScreen()
        {
            yield return null;
            MiniGame.OnSplashScreenDone.Invoke();
        }

        public void DestroyMiniGame()
        {
            if(currentMiniGameObj != null)
            {
                Destroy(currentMiniGameObj.gameObject);
            }
        }

        void EnableHome()
        {
            transform.parent.gameObject.SetActive(true);
        }

        void DisableHome()
        {
            transform.parent.gameObject.SetActive(false);
        }

        public override void PointerHandler()
        {
            pointerHandlerEvent.Invoke();
        }
    }

}
