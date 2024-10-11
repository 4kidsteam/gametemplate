namespace Shared.Repository
{
    public class BannerAdLoadFailedCountLocalRepository : IntRepositoryImpl
    {
        public static readonly BannerAdLoadFailedCountLocalRepository DefaultInstance = new BannerAdLoadFailedCountLocalRepository();

        public BannerAdLoadFailedCountLocalRepository(int defaultValue = 0) : base("bn_load_failed", defaultValue)
        {
        }
    }
}