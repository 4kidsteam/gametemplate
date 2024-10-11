using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Timeline;

namespace Math.Game003
{
    public class Math003Balloon : MonoBehaviour
    {
        [SerializeField] ParticleSystem _explodeParticle;
        [SerializeField] Transform _explodeSpriteEffect;
        [SerializeField] Math003BalloonAnimController _animationControlelr;
        private SortingGroup _sortingGroup;
        private SortingGroup SortingGroup
        {
            get
            {
                if (_sortingGroup == null) _sortingGroup = GetComponent<SortingGroup>();
                return _sortingGroup;
            }
        }

        [HideInInspector] public bool BalloonEnabled;
        [HideInInspector] public int Number;
        public void Hide()
        {
            _animationControlelr.gameObject.SetActive(false);
            DisableEffect();
        }

        public void Show(float delay)
        {
            BalloonEnabled = true;
            SortingGroup.sortingOrder = 550;
            Number = Random.Range(0, 10);
            ChangeNumber();
            DisableEffect();
            StartCoroutine(IEShow(delay));
        }

        void ChangeNumber()
        {
            _animationControlelr.ChangeNumber(Number);
        }

        void DisableEffect()
        {
            _explodeSpriteEffect.gameObject.SetActive(false);
            _explodeParticle.gameObject.SetActive(false);
        }

        IEnumerator IEPlayExplodeEffect()
        {
            Math003AudioManager.Inst.Play("explode");
            _explodeParticle.gameObject.SetActive(true);
            _explodeSpriteEffect.gameObject.SetActive(true);
            _animationControlelr.gameObject.SetActive(false);
            _explodeParticle.Play();
            _explodeSpriteEffect.transform.localScale = Vector3.zero;
            _explodeSpriteEffect.transform.DOScale(1, 0.15f);
            yield return new WaitForSeconds(0.15f);
            _explodeSpriteEffect.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
            _explodeParticle.gameObject.SetActive(false);

        }

        IEnumerator IEShow(float delay)
        {
            _animationControlelr.gameObject.SetActive(false);
            _animationControlelr.transform.localScale = Vector3.zero;
            yield return new WaitForSeconds(delay);
            _animationControlelr.gameObject.SetActive(true);
            _animationControlelr.transform.DOScale(1.15f, 1);
            yield return new WaitForSeconds(1);
            _animationControlelr.transform.DOScale(1, 0.3f);
        }

        private void OnMouseDown()
        {
            if (!BalloonEnabled || !Math003GameManager.Inst.GamePlaying) return;

            if(Number == Math003GameManager.Inst.GamePlayNumber)
            {
                StartCoroutine(IEChoiceRight());
                BalloonEnabled = false;
                Math003GameManager.Inst.CheckWinGame();
            }
            else
            {
                ChoiceWrong();
            }
        }

        IEnumerator IEChoiceRight()
        {
            SortingGroup.sortingOrder = 600;
            Math003AudioManager.Inst.Play(Number.ToString());
            
            StartCoroutine(IEPlayExplodeEffect());
            yield return new WaitForSeconds(0.75f);
            Math003AudioManager.Inst.PlayOneShot("exactly" + Random.Range(0, 4));

        }
        
        void ChoiceWrong()
        {
            Math003AudioManager.Inst.PlayOneShot("wrong_bell");
            Shake();
        }

        void Shake()
        {
            StartCoroutine(IEShakeAnim(transform, 0.1f, 5));
        }

        public static IEnumerator IEShakeAnim(Transform objTransform, float shakeDelay, int loop = 0, float shakeAngle = 15, bool originLocal = false)
        {
            if (loop < 1) loop = 1;
            if (originLocal)
            {
                for (int i = 0; i < loop; i++)
                {
                    objTransform.DOLocalRotate(new Vector3(0, 0, i % 2 == 0 ? shakeAngle : -shakeAngle), shakeDelay);
                    yield return new WaitForSeconds(shakeDelay);
                }
                objTransform.DOLocalRotate(Vector3.zero, shakeDelay * 0.5f);
            }
            else
            {
                for (int i = 0; i < loop; i++)
                {
                    objTransform.DORotate(new Vector3(0, 0, i % 2 == 0 ? shakeAngle : -shakeAngle), shakeDelay);
                    yield return new WaitForSeconds(shakeDelay);
                }
                objTransform.DORotate(Vector3.zero, shakeDelay * 0.5f);
            }
        }
    }
}

