using Shared.Utils;

namespace Shared.Repository
{
    public class FSShowSucceededCountLocalRepository : IntRepositoryImpl
    {
        const string KEY = "fs_show_succeeded";
        public FSShowSucceededCountLocalRepository(int defaultValue = 0) : base(KEY, defaultValue)
        {
        }

        public static readonly FSShowSucceededCountLocalRepository DefaultInstance = new FSShowSucceededCountLocalRepository();
        public readonly static IInstanceManager<IIntRepository> InstanceManager = new IntRepositoryInstanceManager(keyPrefix: KEY);
    }
}