using Shared.Utils;

namespace Shared.Repository
{
    public class AudioShowSucceededCountLocalRepository : IntRepositoryImpl
    {
        const string KEY = "au_show_succeeded";
        public AudioShowSucceededCountLocalRepository(int defaultValue = 0) : base(KEY, defaultValue)
        {
        }

        public static readonly AudioShowSucceededCountLocalRepository DefaultInstance = new AudioShowSucceededCountLocalRepository();
        public readonly static IInstanceManager<IIntRepository> InstanceManager = new IntRepositoryInstanceManager(keyPrefix: KEY);
    }
}