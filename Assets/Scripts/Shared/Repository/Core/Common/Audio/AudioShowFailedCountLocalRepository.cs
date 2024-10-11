using Shared.Utils;

namespace Shared.Repository
{
    public class AudioShowFailedCountLocalRepository : IntRepositoryImpl
    {
        const string KEY = "au_show_failed";
        public AudioShowFailedCountLocalRepository(int defaultValue = 0) : base(KEY, defaultValue)
        {
        }

        public static readonly AudioShowFailedCountLocalRepository DefaultInstance = new AudioShowFailedCountLocalRepository();
        public readonly static IInstanceManager<IIntRepository> InstanceManager = new IntRepositoryInstanceManager(keyPrefix: KEY);
    }
}