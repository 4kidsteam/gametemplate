using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartLocalRepository : IntRepositoryImpl
{
    public static readonly HeartLocalRepository DefaultInstance = new HeartLocalRepository(3);

    public HeartLocalRepository(int defaultValue = 0) : base("HEART", defaultValue)
    {
    }
}

