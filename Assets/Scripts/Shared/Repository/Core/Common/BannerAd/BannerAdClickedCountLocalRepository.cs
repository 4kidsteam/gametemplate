namespace Shared.Repository
{
    public class BannerAdClickedCountLocalRepository : IntRepositoryImpl
    {
        public static readonly BannerAdClickedCountLocalRepository DefaultInstance = new BannerAdClickedCountLocalRepository();

        public BannerAdClickedCountLocalRepository(int defaultValue = 0) : base("bn_clicked", defaultValue)
        {
        }
    }
}