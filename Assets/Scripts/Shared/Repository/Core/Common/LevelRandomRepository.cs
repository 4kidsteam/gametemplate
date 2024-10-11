using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelRandomRepository : IntRepositoryImpl
{
    public static readonly LevelRandomRepository DefaultInstance = new LevelRandomRepository();

    public LevelRandomRepository(int defaultValue = 1) : base("LEVEL_RANDOM", defaultValue)
    {
    }
}

