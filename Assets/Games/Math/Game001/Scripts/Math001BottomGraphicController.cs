using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Math.Game001
{
    public class Math001BottomGraphicController : MonoBehaviour
    {

        [SerializeField] Transform number1Transform, number2Transform;
        [SerializeField] Transform countObj1, countObj2;
        public Math001CountObjGroup[] countObjGroup1, countObjGroup2;

        private void Awake()
        {
            HideAll();
            StartCoroutine(IESetCountObjGroupPosition());
        }

        public void HideAll()
        {
            foreach (var group in countObjGroup1)
            {
                group.gameObject.SetActive(false);
            }
            foreach (var group in countObjGroup2)
            {
                group.gameObject.SetActive(false);
            }
        }

        IEnumerator IESetCountObjGroupPosition()
        {
            countObj1.gameObject.SetActive(false);
            countObj2.gameObject.SetActive(false);

            while (number1Transform.position.x == number2Transform.position.x)
            {
                yield return null;
            }

            countObj1.gameObject.SetActive(true);
            countObj2.gameObject.SetActive(true);

            countObj1.position = new Vector2(number1Transform.position.x, (number1Transform.position.y + transform.position.y) * 0.5f);

            countObj2.position = new Vector2(number2Transform.position.x, (number2Transform.position.y + transform.position.y) * 0.5f);

        }

        public void UpdateOnInspector()
        {
            if (number1Transform != null && number2Transform != null && countObj1 != null && countObj2 != null)
            {
                if (GUILayout.Button("Set Count Obj Position"))
                {
                    countObj1.position = new Vector2(number1Transform.position.x, (number1Transform.position.y + transform.position.y) * 0.5f);

                    countObj2.position = new Vector2(number2Transform.position.x, (number2Transform.position.y + transform.position.y) * 0.5f);
                }
            }

        }
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(Math001BottomGraphicController))]
    public class Math001BottomGraphicControllerInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            ((Math001BottomGraphicController)target).UpdateOnInspector();
        }
    }

#endif

}
