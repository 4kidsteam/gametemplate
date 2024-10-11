using Shared.Utils;

namespace Shared.Repository
{
    public class RVShowFailedCountLocalRepository : IntRepositoryImpl
    {
        const string KEY = "rv_show_failed";
        public RVShowFailedCountLocalRepository(int defaultValue = 0) : base(KEY, defaultValue)
        {
        }

        public static readonly RVShowFailedCountLocalRepository DefaultInstance = new RVShowFailedCountLocalRepository();
        public readonly static IInstanceManager<IIntRepository> InstanceManager = new IntRepositoryInstanceManager(keyPrefix: KEY);
    }
}