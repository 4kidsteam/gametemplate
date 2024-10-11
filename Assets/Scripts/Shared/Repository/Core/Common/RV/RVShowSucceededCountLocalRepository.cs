using Shared.Utils;

namespace Shared.Repository
{
    public class RVShowSucceededCountLocalRepository : IntRepositoryImpl
    {
        const string KEY = "rv_show_succeeded";
        public RVShowSucceededCountLocalRepository(int defaultValue = 0) : base(KEY, defaultValue)
        {
        }

        public static readonly RVShowSucceededCountLocalRepository DefaultInstance = new RVShowSucceededCountLocalRepository();
        public readonly static IInstanceManager<IIntRepository> InstanceManager = new IntRepositoryInstanceManager(keyPrefix: KEY);
    }
}