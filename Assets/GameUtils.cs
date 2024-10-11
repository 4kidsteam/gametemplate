using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUtils
{
    public static int CreateGameId(HomeCategory category, int indexInCategory)
    {
        return ((int)category) * 100 + indexInCategory;
    }
}
