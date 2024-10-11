using UnityEngine;

public class FlagLock : MonoBehaviour
{
    const string TAG = "FlagLock";

    private string _name;

    private bool _isLock = false;
    public bool IsLock => _isLock;
    public bool IsNOTLock => !_isLock;

    public FlagLock(string name, bool isLock = false)
    {
        _name = name;
        _isLock = isLock;
    }

    public void Lock()
    {
#if LOG_INFO
        Debug.LogFormat("{0}->Lock({1})", TAG, _name);
#endif
        _isLock = true;
    }

    public void Unlock()
    {
#if LOG_INFO
        Debug.LogFormat("{0}->Unlock({1})", TAG, _name);
#endif
        _isLock = false;
    }

    public override string ToString() => string.Format("[{0} name={1}, isLock={2}]", TAG, _name, _isLock);
}
