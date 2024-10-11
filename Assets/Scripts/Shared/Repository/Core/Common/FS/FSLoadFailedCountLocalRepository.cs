using Shared.Utils;

namespace Shared.Repository
{
    public class FSLoadFailedCountRepository : IntRepositoryImpl
    {
        const string KEY = "fs_load_failed";
        public FSLoadFailedCountRepository(int defaultValue = 0) : base(KEY, defaultValue)
        {
        }

        public static readonly FSLoadFailedCountRepository DefaultInstance = new FSLoadFailedCountRepository();
    }
}