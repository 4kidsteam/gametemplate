using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface ILongRepository
{
    [System.Serializable]
    public class OnValueUpdated : UnityEvent<long, long> { }

    [System.Serializable]
    public class OnValueDecreased : UnityEvent<long, long> { }

    [System.Serializable]
    public class OnValueIncreased : UnityEvent<long, long> { }

    public string Name { get; }

    public OnValueUpdated onValueUpdated { get; }
    public OnValueDecreased onValueDecreased { get; }
    public OnValueIncreased onValueIncreased { get; }

    bool InitIfNotExisted(long value);

    long Get();

    void Set(long value);
    bool SetIfLargeThanCurrentValue(long newValue);

    long AddMore(long more);
    long Minus(long less);

    bool IsGreaterThanEqual(long val);

    void Clear();
}
