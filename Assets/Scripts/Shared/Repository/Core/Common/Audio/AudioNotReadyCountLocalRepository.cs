using Shared.Utils;

namespace Shared.Repository
{
    public class AudioNotReadyCountLocalRepository : IntRepositoryImpl
    {
        const string KEY = "au_not_ready";
        public AudioNotReadyCountLocalRepository(int defaultValue = 0) : base(KEY, defaultValue)
        {
        }

        public static readonly AudioNotReadyCountLocalRepository DefaultInstance = new AudioNotReadyCountLocalRepository();
        public readonly static IInstanceManager<IIntRepository> InstanceManager = new IntRepositoryInstanceManager(keyPrefix: KEY);
    }
}