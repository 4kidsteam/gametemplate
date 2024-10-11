using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiggyBankLocalRepository : IntRepositoryImpl
{
    public static readonly PiggyBankLocalRepository DefaultInstance = new PiggyBankLocalRepository();
    public PiggyBankLocalRepository(int defaultValue = 0) : base("PIGGY", defaultValue)
    {
    }
}
