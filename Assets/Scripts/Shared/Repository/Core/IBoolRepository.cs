using UnityEngine.Events;

public interface IBoolRepository
{
    [System.Serializable]
    public class OnValueChanged : UnityEvent<bool> { };

    public OnValueChanged onValueChanged { get; }

    public string Name { get; }

    public void Set(bool val);

    public bool Get();
}
