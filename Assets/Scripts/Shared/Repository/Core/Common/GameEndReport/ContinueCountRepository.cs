using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueCountRepository : IntRepositoryImpl
{
    public static readonly ContinueCountRepository DefaultInstance = new ContinueCountRepository();

    public ContinueCountRepository(int defaultValue = 0) : base("CONTINUE_COUNT", defaultValue)
    {
    }
}

