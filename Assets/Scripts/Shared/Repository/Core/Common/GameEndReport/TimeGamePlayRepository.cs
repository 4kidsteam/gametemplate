using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeGamePlayRepository : IntRepositoryImpl
{
    public static readonly TimeGamePlayRepository DefaultInstance = new TimeGamePlayRepository();

    public TimeGamePlayRepository(int defaultValue = 0) : base("TIME_GAME_PLAY", defaultValue)
    {
    }
}
