namespace Shared.Repository
{
    public class BannerAdLoadCountLocalRepository : IntRepositoryImpl
    {
        public static readonly BannerAdLoadCountLocalRepository DefaultInstance = new BannerAdLoadCountLocalRepository();

        public BannerAdLoadCountLocalRepository(int defaultValue = 0) : base("bn_load", defaultValue)
        {
        }
    }
}