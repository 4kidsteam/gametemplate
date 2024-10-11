using Shared.Utils;

namespace Shared.Repository
{
    public class FSNotReadyCountLocalRepository : IntRepositoryImpl
    {
        const string KEY = "fs_not_ready";
        public FSNotReadyCountLocalRepository(int defaultValue = 0) : base(KEY, defaultValue)
        {
        }

        public static readonly FSNotReadyCountLocalRepository DefaultInstance = new FSNotReadyCountLocalRepository();
        public readonly static IInstanceManager<IIntRepository> InstanceManager = new IntRepositoryInstanceManager(keyPrefix: KEY);
    }
}