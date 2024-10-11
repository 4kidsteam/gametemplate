using System.Collections;
using System.Collections.Generic;
using Shared.Utils;
using UnityEngine;

namespace Shared.Repository
{
    public class SceneCountLocalRepository : IntRepositoryImpl
    {
        const string KEY = "scene_count";
        public SceneCountLocalRepository(int defaultValue = 0) : base(name: KEY, defaultValue)
        {
        }

        public readonly static IInstanceManager<IIntRepository> InstanceManager = new IntRepositoryInstanceManager(keyPrefix: KEY);
    }
}