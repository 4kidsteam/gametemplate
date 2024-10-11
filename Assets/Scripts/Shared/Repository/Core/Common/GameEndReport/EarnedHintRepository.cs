using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarnedHintRepository : IntRepositoryImpl
{
    public static readonly EarnedHintRepository DefaultInstance = new EarnedHintRepository();

    public EarnedHintRepository(int defaultValue = 0) : base("EARNED_HINT", defaultValue)
    {
    }
}
