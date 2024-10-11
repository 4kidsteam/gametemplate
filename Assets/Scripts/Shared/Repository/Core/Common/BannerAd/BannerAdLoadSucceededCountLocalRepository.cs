namespace Shared.Repository
{
    public class BannerAdLoadSucceededCountLocalRepository : IntRepositoryImpl
    {
        public static readonly BannerAdLoadSucceededCountLocalRepository DefaultInstance = new BannerAdLoadSucceededCountLocalRepository();

        public BannerAdLoadSucceededCountLocalRepository(int defaultValue = 0) : base("bn_load_succeeded", defaultValue)
        {
        }
    }
}