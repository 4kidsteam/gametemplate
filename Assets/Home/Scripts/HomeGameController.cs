using UnityEngine;
using Zenject;

[RequireComponent(typeof(HomeGameVisualizer))]
[DisallowMultipleComponent]
public class HomeGameController : MAMonoBehaviour
{
    const string TAG = "HomeGameController";

    [SerializeField] private HomeCategory _category;
    [SerializeField] private int _indexInCategory;
    public int IndexInCategory => _indexInCategory;

    [SerializeField] private HomeGameVisualizer _visualizer;
    [SerializeField] private bool _requirePremium = false;

    [Inject(Id = "PremiumRepository")] private IBoolRepository _premiumRepository;

    public System.Action<HomeCategory, int> OnClick = delegate { };

    void Start()
    {
        if (!_requirePremium)
        {
            _visualizer.Open();
        }
        else
        {
            _premiumRepository.onValueChanged.AddListener(_OnPremiumValueChanged);
            this._OnPremiumValueChanged(_premiumRepository.Get());
        }
        this._visualizer.Button.onClick.AddListener(() =>
        {
#if LOG_INFO
            Debug.LogFormat("{0}->this._visualizer.Button.onClick", TAG);
#endif
            this.OnClick.Invoke(this._category, this._indexInCategory);
        });
    }

    public void Set(HomeCategory category, int index)
    {
        _category = category;
        _indexInCategory = index;
    }

    private void _OnPremiumValueChanged(bool newValue)
    {
        if (newValue) _visualizer.Open(); else _visualizer.Lock();  
    }
    
    public void SetToFree()
    {
        _requirePremium = false;
        _visualizer.Unlock();
    }

    public void SetToPremium()
    {
        _requirePremium = true;
        _visualizer.Lock();
    }

#if UNITY_EDITOR
    public override void GUIEditor()
    {
        GUILayout.Space(10);
        GUILayout.Label("Home Game Controller Editor");
        
        if (GUILayout.Button("Require Premium")) SetToPremium();
        if (GUILayout.Button("Free Game")) SetToFree();

        if (GUILayout.Button("Auto Assign"))
        {
            this._visualizer = GetComponent<HomeGameVisualizer>();
            this._visualizer.AutoAssign();
        }
    }
#endif
}
