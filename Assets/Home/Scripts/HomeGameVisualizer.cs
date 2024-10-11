using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class HomeGameVisualizer : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private GameObject _lockLayer;
    [SerializeField] private GameObject _lock;

    public Button Button => _button;
    
    public void Lock()
    {
        _button.interactable = false;
        _lock.SetActive(true);
        _lockLayer.SetActive(true);
    }

    public void Unlock()
    {
        _button.interactable = true;
        _lock.SetActive(false);
        _lockLayer.SetActive(false);
    }

    public void Open() => Unlock();

#if UNITY_EDITOR
    public void AutoAssign()
    {
        this._button = GetComponent<Button>();
        this._lockLayer = this.transform.Find("LockCover").gameObject;
        this._lockLayer.GetComponent<Image>().type = Image.Type.Sliced;
        this._lock = this._lockLayer.transform.Find("LockIcon").gameObject;
    }
#endif
}
