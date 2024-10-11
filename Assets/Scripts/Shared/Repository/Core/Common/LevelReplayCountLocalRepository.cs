using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shared.Repository
{
    [DisallowMultipleComponent]
    public class LevelReplayCountLocalRepository : IntRepositoryImpl
    {
        public static readonly LevelReplayCountLocalRepository DefaultInstance = new LevelReplayCountLocalRepository();
        public LevelReplayCountLocalRepository() : base("LEVEL_REPLAY", 0)
        { }
    }
}
