using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpentHintRepository : IntRepositoryImpl
{
    public static readonly SpentHintRepository DefaultInstance = new SpentHintRepository();

    public SpentHintRepository(int defaultValue = 0) : base("SPENT_HINT", defaultValue)
    {
    }
}

