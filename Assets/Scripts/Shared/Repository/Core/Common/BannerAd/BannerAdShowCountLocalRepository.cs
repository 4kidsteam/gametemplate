namespace Shared.Repository
{
    public class BannerAdShowCountLocalRepository : IntRepositoryImpl
    {
        public static readonly BannerAdShowCountLocalRepository DefaultInstance = new BannerAdShowCountLocalRepository();

        public BannerAdShowCountLocalRepository(int defaultValue = 0) : base("bn_show", defaultValue)
        {
        }
    }
}