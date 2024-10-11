using Shared.Utils;
using System.Collections.Generic;

namespace Shared.Repository
{
    public class FSClosedCountLocalRepository : IntRepositoryImpl
    {
        const string KEY = "fs_closed";

        public FSClosedCountLocalRepository(int defaultValue = 0) : base(KEY, defaultValue)
        {
        }

        public static readonly FSClickedCountLocalRepository DefaultInstance = new FSClickedCountLocalRepository();
        public readonly static IInstanceManager<IIntRepository> InstanceManager = new IntRepositoryInstanceManager(keyPrefix: KEY);
    }
}