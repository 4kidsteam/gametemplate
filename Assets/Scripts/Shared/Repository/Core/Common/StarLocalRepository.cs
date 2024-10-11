using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarLocalRepository : IntRepositoryImpl
{
    public static readonly StarLocalRepository DefaultInstance = new StarLocalRepository();
    public StarLocalRepository(int defaultValue = 0) : base("STAR", defaultValue)
    {
    }
}
