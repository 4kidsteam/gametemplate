using Shared.Utils;

namespace Shared.Repository
{
    public class FSReadyCountRepository : IntRepositoryImpl
    {
        const string KEY = "fs_ready";
        public FSReadyCountRepository(int defaultValue = 0) : base(KEY, defaultValue)
        {
        }

        public static readonly FSReadyCountRepository DefaultInstance = new FSReadyCountRepository();
        public readonly static IInstanceManager<IIntRepository> InstanceManager = new IntRepositoryInstanceManager(keyPrefix: KEY);
    }
}