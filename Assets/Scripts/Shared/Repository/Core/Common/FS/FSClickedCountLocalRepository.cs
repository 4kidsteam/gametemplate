using System.Collections.Generic;
using Shared.Utils;

namespace Shared.Repository
{
    public class FSClickedCountLocalRepository : IntRepositoryImpl
    {
        const string KEY = "fs_clicked";
        
        public FSClickedCountLocalRepository(int defaultValue = 0) : base(KEY, defaultValue)
        {
        }

        public static readonly FSClickedCountLocalRepository DefaultInstance = new FSClickedCountLocalRepository();
        public readonly static IInstanceManager<IIntRepository> InstanceManager = new IntRepositoryInstanceManager(keyPrefix: KEY);
    }
}