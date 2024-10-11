using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinLocalRepository : IntRepositoryImpl
{
    public static readonly CoinLocalRepository DefaultInstance = new CoinLocalRepository();
    public CoinLocalRepository(int defaultValue = 0) : base("COIN", defaultValue)
    {
    }
}
