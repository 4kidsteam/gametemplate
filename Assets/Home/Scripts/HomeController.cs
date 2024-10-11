using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;

[DisallowMultipleComponent]
public class HomeController : MAMonoBehaviour
{
    [System.Serializable]
    public class MiniGamePrefabCategory {

        [Serializable]
        public class AssetReferenceInfo
        {
            public string AssetName;
            public AssetReference AssetReference;

        }
        public HomeCategory Category;
        public AssetReferenceInfo[] AssetReferenceInfos;
    }

    public class GameDefine
    {
        private HomeCategory _category;
        public HomeCategory Category => _category;

        private int _index;
        public int Index => _index;

        private string _prefabPath;
        public string PrefabPath => _prefabPath;

        public GameDefine(HomeCategory category, int index, string prefabPath)
        {
            _category = category;
            _index = index;
            _prefabPath = prefabPath;
        }
    }

    public interface IPrefabCache
    {
        void Cache(HomeCategory category, int index, GameObject prefab);

        GameObject Get(HomeCategory category, int index);
    }

    public class PrefabCache : IPrefabCache
    {
        private Dictionary<HomeCategory, Dictionary<int, GameObject>> _cache = new Dictionary<HomeCategory, Dictionary<int, GameObject>>();

        public void Cache(HomeCategory category, int index, GameObject prefab)
        {
            if (_cache.ContainsKey(category))
            {
                if (_cache[category].ContainsKey(index)) _cache[category][index] = prefab; else _cache[category].Add(index, prefab);
            }
            else
            {
                Dictionary<int, GameObject> aa = new Dictionary<int, GameObject>();
                _cache.Add(category, aa);
                _cache[category].Add(index, prefab);
            }
        }

        public GameObject Get(HomeCategory category, int index)
        {
            if (_cache.ContainsKey(category) && _cache[category].ContainsKey(index)) return _cache[category][index];
            else return null;
        }
    }

    const string TAG = "HomeController";
    [SerializeField] private HomeCategoryController[] _categoryControllers;
    [SerializeField] private Transform _gameParent;
    private bool isLoadingMinigame;

    private List<GameDefine> _games = new()
    {
        // Alphabet
        new GameDefine(category: HomeCategory.Alphabet, 1, "alphabet_001"),
        new GameDefine(category: HomeCategory.Alphabet, 2, "alphabet_002"),
        new GameDefine(category: HomeCategory.Alphabet, 3, "alphabet_003"),
        new GameDefine(category: HomeCategory.Alphabet, 4, "alphabet_004"),
        new GameDefine(category: HomeCategory.Alphabet, 5, "alphabet_005"),
        new GameDefine(category: HomeCategory.Alphabet, 6, "alphabet_006"),
        new GameDefine(category: HomeCategory.Alphabet, 7, "alphabet_007"),
        new GameDefine(category: HomeCategory.Alphabet, 8, "alphabet_008"),

        // Color
        new GameDefine(category: HomeCategory.Color, 3, "color_003"),
        new GameDefine(category: HomeCategory.Color, 4, "color_004"),
        new GameDefine(category: HomeCategory.Color, 5, "color_005"),
        new GameDefine(category: HomeCategory.Color, 6, "color_006"),
        new GameDefine(category: HomeCategory.Color, 7, "color_007"),

        // Math
        new GameDefine(category: HomeCategory.Math, 1, "math_001"),
        new GameDefine(category: HomeCategory.Math, 3, "math_003"),
        new GameDefine(category: HomeCategory.Math, 4, "math_004"),
        new GameDefine(category: HomeCategory.Math, 5, "math_005"),
        new GameDefine(category: HomeCategory.Math, 6, "math_006"),
        new GameDefine(category: HomeCategory.Math, 8, "math_008"),

        // Shape
        new GameDefine(category: HomeCategory.Shape, 1, "shape_001"),
        new GameDefine(category: HomeCategory.Shape, 2, "shape_002"),
        new GameDefine(category: HomeCategory.Shape, 3, "shape_003"),
        new GameDefine(category: HomeCategory.Shape, 4, "shape_004"),
        new GameDefine(category: HomeCategory.Shape, 5, "shape_005"),
        new GameDefine(category: HomeCategory.Shape, 6, "shape_006"),
        new GameDefine(category: HomeCategory.Shape, 7, "shape_007"),
        new GameDefine(category: HomeCategory.Shape, 8, "shape_008"),

        //Puzzle
        new GameDefine(category: HomeCategory.Puzzle, 1, "puzzle_001"),
        new GameDefine(category: HomeCategory.Puzzle, 2, "puzzle_002"),
        new GameDefine(category: HomeCategory.Puzzle, 3, "puzzle_003"),
    };

    private Dictionary<HomeCategory, Dictionary<int, GameDefine>> _gameDictionary;
    public Dictionary<HomeCategory, Dictionary<int, GameDefine>> GameDictionary
    {
        get
        {
            if (_gameDictionary == null) _gameDictionary = _CreateGameDictionary();
            return _gameDictionary;
        }
    }

    private Dictionary<HomeCategory, HomeCategoryController> _categoryDict;

    public Dictionary<HomeCategory, HomeCategoryController> CategoryDict
    {
        get
        {
            if (_categoryDict == null)
            {
                _categoryDict = new();
                foreach (var c in _categoryControllers) _categoryDict.Add(c.Category, c);
            }
            return _categoryDict;
        }
    }

    private IPrefabCache _prefabCache = new PrefabCache();

    public MiniGamePrefabCategory[] MiniGamePrefabCategories;

    // Start is called before the first frame update
    void Start()
    {
        SetTargetFrameRate();
        FPSRenderer.Instance.StartRender();
        foreach (var e in _categoryControllers)
        {
            e.OnClick += _OnClickOnGame;
        }
        
        // #if UNITY_EDITOR
        SetAllGameToPremium();
        foreach (var game in _games)
        {
            try
            {
                this.CategoryDict[game.Category].SetFreeGame(game.Index);
            }
            catch {
            }
            
        }
        // #endif
    }

    void SetTargetFrameRate()
    {
#if UNITY_EDITOR
        Application.targetFrameRate = 30;
#else
        Application.targetFrameRate = 60;
#endif
    }

    private void _OnClickOnGame(HomeCategory category, int index)
    {
#if LOG_INFO
        Debug.LogFormat("{0}->_OnClickOnGame: {1} {2}", TAG, category.ToString(), index);
#endif
        MiniGame.ShowSplashScreen(() => StartCoroutine(_LoadGame(category, index)), false );
    }


    private IEnumerator _LoadGame(HomeCategory category, int index)
    {
        if (!isLoadingMinigame)
        {
            GameObject prefab = _prefabCache.Get(category, index);
            if (prefab == null)
            {
                GameDefine gameDefine = GameDictionary[category][index];
                AssetReference minigamePrefabReference = GetMinigamePrefabReference(category, gameDefine.PrefabPath);

                if(minigamePrefabReference != null)
                {
                    isLoadingMinigame = true;
                    //var request = Resources.LoadAsync<GameObject>(gameDefine.PrefabPath);
                    var request = Addressables.LoadAssetAsync<GameObject>(minigamePrefabReference);

                    yield return request;
                    
                    prefab = request.Result;
                    _prefabCache.Cache(category, index, prefab);

                    
                }
                isLoadingMinigame = false;

            }
            if (prefab != null)
            {
                MiniGame.InstantiateMiniGamePrefab(prefab, _gameParent);
#if LOG_INFO
                Debug.Log(" Virtual splash screen done");
#endif

            }

            MiniGame.SplashScreenLoadFinished = true;
        }
    }



    public AssetReference GetMinigamePrefabReference(HomeCategory category, string prefabName)
    {
        var categoryMatch = MiniGamePrefabCategories.FirstOrDefault(c => c.Category == category);

        if (categoryMatch == null)
        {
            Debug.LogError($"Category {category} not found");
            return null;
        }
        var assetReference = categoryMatch.AssetReferenceInfos.FirstOrDefault(ar => ar.AssetName == prefabName).AssetReference;

        if (assetReference == null)
        {
            Debug.LogError($"Can not found asset with name {prefabName} in Category {category}");
            return null;
        }

        return assetReference;
    }


    private Dictionary<HomeCategory, Dictionary<int, GameDefine>> _CreateGameDictionary()
    {
        Dictionary<HomeCategory, Dictionary<int, GameDefine>> dict = new Dictionary<HomeCategory, Dictionary<int, GameDefine>>();
        foreach (var e in _games)
        {
            if (!dict.ContainsKey(e.Category))
            {
                Dictionary<int, GameDefine> aa = new Dictionary<int, GameDefine>();
                dict.Add(e.Category, aa);
            }
            if (!dict[e.Category].ContainsKey(e.Index))
            {
                dict[e.Category].Add(e.Index, e);
            }
        }
        return dict;
    }

    private void SetAllGameToFree()
    {
        foreach (var e in _categoryControllers) e.SetAllGameToFree();
    }

    private void SetAllGameToPremium()
    {
        foreach (var e in _categoryControllers) e.SetAllGameToPremium();
    }
    
#if UNITY_EDITOR
    public override void GUIEditor()
    {
        GUILayout.Space(10);
        GUILayout.Label(string.Format("{0} Editor", TAG));
        if (GUILayout.Button("SetAllGameToFree")) SetAllGameToFree();
        if (GUILayout.Button("SetAllGameToPremium")) SetAllGameToPremium();
        GetMiniGamePrefabReferenceNames();

    }

    void GetMiniGamePrefabReferenceNames()
    {
        foreach (var miniGamePrefabCategory in MiniGamePrefabCategories)
        {
            foreach (var assetReferenceInfo in miniGamePrefabCategory.AssetReferenceInfos)
            {
                try
                {
                    assetReferenceInfo.AssetName = assetReferenceInfo.AssetReference.editorAsset.name;
                }
                catch
                {
                    assetReferenceInfo.AssetName = "";
                }
                
            }
        }
    }
#endif
}
