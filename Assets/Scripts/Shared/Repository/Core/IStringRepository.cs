using UnityEngine.Events;

public interface IStringRepository
{
    [System.Serializable]
    public class OnValueUpdated : UnityEvent<string, string> { }

    public string Name { get; }
    public OnValueUpdated onValueUpdated { get; }

    bool InitIfNotExisted(string value);

    string Get();
    bool Set(string diffValue);
}
