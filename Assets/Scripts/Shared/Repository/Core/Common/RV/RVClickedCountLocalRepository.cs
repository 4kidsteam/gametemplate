using Shared.Utils;

namespace Shared.Repository
{
    public class RVClickedCountLocalRepository : IntRepositoryImpl
    {

        const string KEY = "rv_clicked";
        public RVClickedCountLocalRepository(int defaultValue = 0) : base(KEY, defaultValue)
        {
        }

        public static readonly RVClickedCountLocalRepository DefaultInstance = new RVClickedCountLocalRepository();
        public readonly static IInstanceManager<IIntRepository> InstanceManager = new IntRepositoryInstanceManager(keyPrefix: KEY);
    }
}