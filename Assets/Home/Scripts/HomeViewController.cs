using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeViewController : MonoBehaviour
{

    public enum HomeViewScreen
    {
        LobbyCategory = 0,
        GameCategory = 1080,
    }

    private RectTransform _homeView;

    [SerializeField] bool _isFreeGame;

    [SerializeField] float _changeViewScreenSpeed;
    private HomeViewScreen _homeViewScreen;

    private void Awake()
    {

        _homeView = gameObject.GetComponent<RectTransform>();
        LobbyCategoryButton.OnLobbyCategoryButtonClick += this.OnLobbyCategoryButtonClick;
        BackLobby();
    }

    private void OnDestroy()
    {
        LobbyCategoryButton.OnLobbyCategoryButtonClick = delegate { };
    }



    public void BackLobby()
    {
        gameObject.SetActive(true);
        _homeViewScreen = HomeViewScreen.LobbyCategory;

        StopCoroutine(IEChangeCategoryScrollView());
        StartCoroutine(IEChangeCategoryScrollView());
    }

    void OnLobbyCategoryButtonClick(int lobbyCategoryIndex)
    {
        
        _homeViewScreen = HomeViewScreen.GameCategory;

        StopCoroutine(IEChangeCategoryScrollView());
        StartCoroutine(IEChangeCategoryScrollView());
    }

    IEnumerator IEChangeCategoryScrollView()
    {
        if (_homeViewScreen == HomeViewScreen.LobbyCategory)
        {
        }

        float checkDistance = Time.deltaTime * _changeViewScreenSpeed;
        while (Mathf.Abs(_homeView.anchoredPosition.y - (float)_homeViewScreen) > checkDistance)
        {
            
            Vector2 moveDir = new Vector2(0, (float)_homeViewScreen - _homeView.anchoredPosition.y).normalized;
            Vector2 updatePos = _homeView.anchoredPosition + ( moveDir * Time.deltaTime * _changeViewScreenSpeed);
            _homeView.anchoredPosition = updatePos;
            yield return null;
        }

        _homeView.anchoredPosition = new Vector2(0, (float)_homeViewScreen);
        if (_homeViewScreen == HomeViewScreen.GameCategory)
        {
        }
    }

    public void ChangeImageColor(Image img, Color colorTarget, float duration)
    {
        StopCoroutine(IEChangeImageColor(img, colorTarget, duration));
        StartCoroutine(IEChangeImageColor(img, colorTarget, duration));
    }
    IEnumerator IEChangeImageColor(Image img, Color colorTarget, float duration)
    {
        float timer = 0;
        Color colorStart = img.color;
        
        while (timer < duration)
        {
            img.gameObject.SetActive(img.color.a > 0);

            timer += Time.deltaTime;
            float t = timer / duration;
            img.color = Color.Lerp(colorStart, colorTarget, t);
            yield return null;
        }
        img.color = colorTarget;
        img.gameObject.SetActive(img.color.a > 0);
    }

    public void ShowSettingPopup()
    {
        Popup.ShowPopup(Popup.SettingPopupName);
    }

    public void ShowPurchasePopup()
    {
        Popup.ShowPopup(Popup.PurchasePopupName);
    }


}
