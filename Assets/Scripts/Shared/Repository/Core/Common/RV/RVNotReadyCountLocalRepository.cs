using Shared.Utils;

namespace Shared.Repository
{
    public class RVNotReadyCountLocalRepository : IntRepositoryImpl
    {
        const string KEY = "rv_not_ready";
        public RVNotReadyCountLocalRepository(int defaultValue = 0) : base(KEY, defaultValue)
        {
        }

        public static readonly RVNotReadyCountLocalRepository DefaultInstance = new RVNotReadyCountLocalRepository();
        public readonly static IInstanceManager<IIntRepository> InstanceManager = new IntRepositoryInstanceManager(keyPrefix: KEY);
    }
}