using Shared.Utils;

namespace Shared.Repository
{
    public class AudioLoadFailedCountLocalRepository : IntRepositoryImpl
    {
        const string KEY = "au_load_failed";
        public AudioLoadFailedCountLocalRepository(int defaultValue = 0) : base(KEY, defaultValue)
        {
        }

        public static readonly AudioLoadFailedCountLocalRepository DefaultInstance = new AudioLoadFailedCountLocalRepository();
    }
}