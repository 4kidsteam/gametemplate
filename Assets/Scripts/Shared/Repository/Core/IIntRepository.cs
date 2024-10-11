using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public interface IIntRepository
{
    [System.Serializable]
    public class OnIntValueUpdated : UnityEvent<int, int> { }

    [System.Serializable]
    public class OnIntValueDecreased : UnityEvent<int, int> { }

    [System.Serializable]
    public class OnIntValueIncreased : UnityEvent<int, int> { }

    public string Name { get; }

    public OnIntValueUpdated onIntValueUpdated { get; }
    public OnIntValueDecreased onIntValueDecreased { get; }
    public OnIntValueIncreased onIntValueIncreased { get; }

    bool InitIfNotExisted(int value);

    int Get();

    void Set(int value);
    bool SetIfLargeThanCurrentValue(int newValue);

    int AddMore(int more);
    int Minus(int less);

    bool IsGreaterThanEqual(int val);
    bool IsGreaterThanZERO();

    void Clear();
}
