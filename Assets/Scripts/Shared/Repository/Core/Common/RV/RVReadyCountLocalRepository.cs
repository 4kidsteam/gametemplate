using Shared.Utils;

namespace Shared.Repository
{
    public class RVReadyCountLocalRepository : IntRepositoryImpl
    {
        const string KEY = "rv_ready";
        public RVReadyCountLocalRepository(int defaultValue = 0) : base(KEY, defaultValue)
        {
        }

        public static readonly RVReadyCountLocalRepository DefaultInstance = new RVReadyCountLocalRepository();
        public readonly static IInstanceManager<IIntRepository> InstanceManager = new IntRepositoryInstanceManager(keyPrefix: KEY);
    }
}