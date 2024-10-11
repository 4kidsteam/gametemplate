using Shared.Utils;

namespace Shared.Repository
{
    public class FSShowFailedCountLocalRepository : IntRepositoryImpl
    {
        const string KEY = "fs_show_failed";
        public FSShowFailedCountLocalRepository(int defaultValue = 0) : base(KEY, defaultValue)
        {
        }

        public static readonly FSShowFailedCountLocalRepository DefaultInstance = new FSShowFailedCountLocalRepository();
        public readonly static IInstanceManager<IIntRepository> InstanceManager = new IntRepositoryInstanceManager(keyPrefix: KEY);
    }
}