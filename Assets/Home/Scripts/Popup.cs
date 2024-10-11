using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class Popup : MonoBehaviour
{
    public static GameObject PopupGameObj;

    public const string SettingPopupName = "Setting";
    public const string PurchasePopupName = "Purchase";

    public static bool AnimIsPlaying;

    const float ScaleAnimDuration = 0.7f;

    public static void ShowPopup(string name)
    {
        Instantiate(Resources.Load("Popups/" + name));
        Debug.Log("Show " + name);
    }

    [SerializeField] RectTransform _popupObjRect;
    [SerializeField] Image _fade;
    public bool PopupIsShowing
    {
        get
        {
            if (PopupGameObj == null) return false;
            else return true;
        }
    }

    public void Awake()
    {
        PopupGameObj = gameObject;
        _popupObjRect.localScale = Vector2.zero;
        if (_fade !=null)_fade.color = new Color(_fade.color.r, _fade.color.g, _fade.color.b, 0);
    }

    IEnumerator SetFadeOpacity()
    {
        float alphaValue = 0;
        float alphaValueTarget = 0.5f;
        while(alphaValue < alphaValueTarget)
        {
            alphaValue += Time.deltaTime;
            _fade.color = new Color(_fade.color.r, _fade.color.g, _fade.color.b, alphaValue);
            yield return null;
        }
        _fade.color = new Color(_fade.color.r, _fade.color.g, _fade.color.b, alphaValueTarget);
    }

    public virtual IEnumerator Start()
    {
        AnimIsPlaying = true;
        _popupObjRect.gameObject.SetActive(true);
        if (_fade != null)StartCoroutine(SetFadeOpacity());
        _popupObjRect.localScale = Vector2.zero;
        _popupObjRect.DOScale(new Vector2(1.1f, 1.2f), ScaleAnimDuration * 0.4f);
        yield return new WaitForSeconds(ScaleAnimDuration * 0.4f);
        _popupObjRect.DOScale(Vector2.one, ScaleAnimDuration * 0.1f);
        yield return new WaitForSeconds(ScaleAnimDuration * 0.1f);
        AnimIsPlaying = false;
    }

    public void EscButtonClick()
    {
        StartCoroutine(DisableAnim());
        Destroy(gameObject, 0.4f);
        //Destroy(gameObject);
    }

    IEnumerator DisableAnim()
    {
        Time.timeScale = 1;
        _popupObjRect.DOScale(new Vector2(1.1f, 1.05f), 0.1f);
        yield return new WaitForSeconds(0.2f);
        _popupObjRect.DOScale(Vector2.zero, 0.3f);
    }
}
