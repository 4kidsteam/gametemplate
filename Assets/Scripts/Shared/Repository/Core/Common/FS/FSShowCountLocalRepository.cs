namespace Shared.Repository
{
    public class FSShowCountLocalRepository : IntRepositoryImpl
    {
        public static readonly FSShowCountLocalRepository DefaultInstance = new FSShowCountLocalRepository();

        public FSShowCountLocalRepository(int defaultValue = 0) : base("fs_show", defaultValue)
        {
        }
    }
}