using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shared.Repository
{
    [DisallowMultipleComponent]
    public class SessionCounterLocalRepository : IntRepositoryImpl
    {
        public static readonly SessionCounterLocalRepository DefaultInstance = new SessionCounterLocalRepository();
        public SessionCounterLocalRepository() : base("NAME_SESSION_COUNTER", 0)
        { }
    }
}