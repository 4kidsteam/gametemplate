using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Math.Game004
{
    public class Math004CountObjGroup : MonoBehaviour
    {
        List<Image> images;

        [Range(60, 150)][SerializeField] int maxDeltaSize = 60;


        void GetImages()
        {
            images = new List<Image>();
            for (int i = 0; i < transform.childCount; i++)
            {
                images.Add(transform.GetChild(i).GetComponent<Image>());
            }
        }


        public void SetSprites(Sprite spr)
        {
            GetImages();
            foreach (var img in images)
            {
                img.sprite = spr;

            }
            SetImagesDeltaSize();
        }

        public void ShowObjToCountAnim()
        {
            StartCoroutine(IEShow());
        }

        IEnumerator IEShow()
        {
            foreach (var img in images)
            {
                img.rectTransform.localScale = Vector3.zero;
                img.transform.GetChild(0).gameObject.SetActive(false);
                img.rectTransform.DOScale(Vector3.one * 1.1f, 0.5f);
            }
            yield return new WaitForSeconds(0.5f);
            foreach (var img in images)
            {
                img.rectTransform.DOScale(Vector3.one, 0.2f);
            }
        }

        public void ShowXMinusIcons(int minusNumber)
        {
            for (int i = 0; i < minusNumber; i++)
            {
                Transform minusIcon = images[i].transform.GetChild(0);
                minusIcon.gameObject.SetActive(true);
                minusIcon.localScale = Vector2.zero;
                minusIcon.DOScale(1, 0.5f);
            }
        }

        void SetImagesDeltaSize()
        {
            foreach (var img in images)
            {
                if (img.sprite != null)
                {
                    SetRectTransformMaxSize(img.rectTransform, img.sprite.rect.size, maxDeltaSize);
                }
            }
        }

        public static void SetRectTransformMaxSize(RectTransform rect, Vector2 nativeSize, float maxSize)
        {
            float currentDeltaSize = nativeSize.x > nativeSize.y ? nativeSize.x : nativeSize.y;

            rect.sizeDelta = nativeSize * maxSize / (float)currentDeltaSize;
        }


        public void OnInspectorChanged()
        {
            GetImages();
            SetImagesDeltaSize();
        }

    }

#if UNITY_EDITOR
    [UnityEditor.CustomEditor(typeof(Math004CountObjGroup), true)]
    public class Math004CountObjGroupInspector : Editor
    {
        Math004CountObjGroup targetClass;

        public void OnEnable()
        {
            targetClass = (Math004CountObjGroup)target;

            ActiveSeftOnly();
        }

        void ActiveSeftOnly()
        {
            for (int i = 0; i < targetClass.transform.parent.childCount; i++)
            {
                targetClass.transform.parent.GetChild(i).gameObject.SetActive(false);
            }
            targetClass.gameObject.SetActive(true);
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
