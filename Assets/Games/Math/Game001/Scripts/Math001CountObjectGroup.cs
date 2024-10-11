using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Math.Game001
{
    public class Math001CountObjGroup : MonoBehaviour
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

        public IEnumerator IEShow()
        {
            foreach (var img in images)
            {
                img.rectTransform.localScale = Vector3.zero;
                img.rectTransform.DOScale(Vector3.one * 1.1f, 0.5f);
            }
            yield return new WaitForSeconds(0.5f);
            foreach (var img in images)
            {
                img.rectTransform.DOScale(Vector3.one, 0.2f);
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
    [UnityEditor.CustomEditor(typeof(Math001CountObjGroup), true)]
    public class Math001CountObjGroupInspector : Editor
    {
        Math001CountObjGroup targetClass;

        public void OnEnable()
        {
            targetClass = (Math001CountObjGroup)target;
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
