using Shared.Utils;

namespace Shared.Repository
{
    public class RVRewardedCountLocalRepository : IntRepositoryImpl
    {
        const string KEY = "rv_rewarded";
        public RVRewardedCountLocalRepository(int defaultValue = 0) : base(KEY, defaultValue)
        {
        }

        public static readonly RVRewardedCountLocalRepository DefaultInstance = new RVRewardedCountLocalRepository();
        public readonly static IInstanceManager<IIntRepository> InstanceManager = new IntRepositoryInstanceManager(keyPrefix: KEY);
    }
}