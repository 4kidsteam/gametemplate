using Shared.Utils;
using System.Collections.Generic;

namespace Shared.Repository
{
    public class AudioClosedCountLocalRepository : IntRepositoryImpl
    {
        const string KEY = "au_closed";

        public AudioClosedCountLocalRepository(int defaultValue = 0) : base(KEY, defaultValue)
        {
        }

        public static readonly AudioClosedCountLocalRepository DefaultInstance = new AudioClosedCountLocalRepository();
        public readonly static IInstanceManager<IIntRepository> InstanceManager = new IntRepositoryInstanceManager(keyPrefix: KEY);
    }
}