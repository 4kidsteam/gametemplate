using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialLocalRepository : BoolRepositoryImpl
{
    public static readonly TutorialLocalRepository DefaultInstance = new TutorialLocalRepository();

    public TutorialLocalRepository(bool defaultValue = false) : base("TUTORIAL", defaultValue)
    {
    }
}


