using Shared.Utils;

namespace Shared.Repository
{
    public class RVClosedCountLocalRepository : IntRepositoryImpl
    {
        const string KEY = "rv_closed";
        public RVClosedCountLocalRepository(int defaultValue = 0) : base(KEY, defaultValue)
        {
        }

        public static readonly RVClosedCountLocalRepository DefaultInstance = new RVClosedCountLocalRepository();
        public readonly static IInstanceManager<IIntRepository> InstanceManager = new IntRepositoryInstanceManager(keyPrefix: KEY);
    }
}