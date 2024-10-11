using Shared.Utils;

namespace Shared.Repository
{
    public class AudioReadyCountLocalRepository : IntRepositoryImpl
    {
        const string KEY = "au_ready";
        public AudioReadyCountLocalRepository(int defaultValue = 0) : base(KEY, defaultValue)
        {
        }

        public static readonly AudioReadyCountLocalRepository DefaultInstance = new AudioReadyCountLocalRepository();
        public readonly static IInstanceManager<IIntRepository> InstanceManager = new IntRepositoryInstanceManager(keyPrefix: KEY);
    }
}