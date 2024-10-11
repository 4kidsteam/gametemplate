using System.Collections;
using System.Collections.Generic;
using Shared.Utils;
using UnityEngine;

namespace Shared.Repository
{
    public class IntRepositoryInstanceManager : IInstanceManager<IIntRepository>
    {
        private Dictionary<string, IIntRepository> _instanceCache = new Dictionary<string, IIntRepository>();

        private string _keyPrefix;

        public IntRepositoryInstanceManager()
        {
            _keyPrefix = string.Empty;
        }

        public IntRepositoryInstanceManager(string keyPrefix)
        {
            _keyPrefix = keyPrefix;
        }

        public IIntRepository Get(string key)
        {
            string kk = key;
            if (!string.IsNullOrEmpty(_keyPrefix)) kk = string.Format("{0}_{1}", _keyPrefix, key);
            if (_instanceCache.ContainsKey(kk)) return _instanceCache[kk];
            var instance = new IntRepositoryImpl(kk);
            _instanceCache[kk] = instance;
            return instance;
        }
    }
}