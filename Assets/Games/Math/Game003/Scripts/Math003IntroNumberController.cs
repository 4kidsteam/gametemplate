using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


namespace Math.Game003
{
    public class Math003IntroNumberController : MonoBehaviour
    {
        [SerializeField] Math003BalloonAnimController _balloonAnimation;

        [SerializeField] SpriteRenderer _light;


        public void Show(int number)
        {
            gameObject.SetActive(true);
            StartCoroutine(IEShow(number));
            StartCoroutine(IERotateLight());
        }

        IEnumerator IEShow(int number)
        {
            _balloonAnimation.ChangeNumber(number);
            transform.localScale = Vector3.zero;
            transform.DOScale(1, 1);
            yield return new WaitForSeconds(3);
            transform.DOScale(1.15f, 0.3f);
            yield return new WaitForSeconds(0.3f);
            transform.DOScale(0, 0.5f);
            yield return new WaitForSeconds(0.5f);
            gameObject.SetActive(false);
        }

        IEnumerator IERotateLight()
        {
            _light.color = new Color(1, 1, 1, 0);
            StartCoroutine(IEChangeSpriteRendererColor(_light, Color.white, 2));
            float speed = 30;
            while (gameObject.activeSelf)
            {
                float rotationZ = _light.transform.eulerAngles.z;
                rotationZ += speed * Time.deltaTime;
                rotationZ %= 360;
                _light.transform.eulerAngles = new Vector3(0, 0, rotationZ);
                yield return null;
            }
        }

        public static IEnumerator IEChangeSpriteRendererColor(SpriteRenderer spr, Color colorTarget, float duration)
        {
            float timer = 0;
            Color colorStart = spr.color;

            while (timer < duration)
            {
                timer += Time.deltaTime;
                float t = timer / duration;
                spr.color = Color.Lerp(colorStart, colorTarget, t);
                yield return null;
            }
            spr.color = colorTarget;
        }
    }
}

