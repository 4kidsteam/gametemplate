using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintLocalRepository : IntRepositoryImpl
{
    public static readonly HintLocalRepository DefaultInstance = new HintLocalRepository(0);

    public HintLocalRepository(int defaultValue = 0) : base("HINT", defaultValue)
    {
    }
}

