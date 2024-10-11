using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class HomeCategoryController : MAMonoBehaviour
{
    const string TAG = "HomeCategoryController";

    [SerializeField] private HomeCategory _category;
    public HomeCategory Category => _category;

    [SerializeField] private HomeGameController[] _gameControllers;
    private Dictionary<int, HomeGameController> _gameDict;
    public Dictionary<int, HomeGameController> GameDict
    {
        get
        {
            if (_gameDict == null)
            {
                _gameDict = new();
                foreach (var game in _gameControllers) _gameDict.Add(game.IndexInCategory, game);
            }
            return _gameDict;
        }
    }

    // Proxy action.
    public System.Action<HomeCategory, int> OnClick = delegate { };

    private void Start()
    {
        for (int i = 0; i < _gameControllers.Length; i++)
        {
            _gameControllers[i].OnClick += _OnClick;
        }
    }

    private void _OnClick(HomeCategory category, int index)
    {
#if LOG_INFO
        Debug.LogFormat("{0}->_OnClick: {1} {2}", TAG, category.ToString(), index);
#endif
        this.OnClick.Invoke(category, index);
    }
    
    public void SetFreeGame(int index)
    {
        this.GameDict[index].SetToFree();
    }

    public void SetAllGameToFree()
    {
        foreach (var e in _gameControllers) e.SetToFree();
    }

    public void SetAllGameToPremium()
    {
        foreach (var e in _gameControllers) e.SetToPremium();
    }

#if UNITY_EDITOR
    public override void GUIEditor()
    {
        GUILayout.Space(10);
        GUILayout.Label("HomeCategoryController Editor");
        if (GUILayout.Button("Preset"))
        {
            _gameControllers = this.GetComponentsInChildren<HomeGameController>();
            for (int i = 0; i < _gameControllers.Length; i++)
            {
                _gameControllers[i].Set(_category, i + 1);
            }
        }
        if (GUILayout.Button("UTC 0 ISO"))
        {
            System.DateTime now = System.DateTime.UtcNow;
            Debug.LogFormat("{0} with {1}h: {2}min", now.ToString(), now.Hour, now.Minute);
        }
    }
#endif
}
