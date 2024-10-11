namespace Shared.Repository
{
    public class AudioLoadSucceededCountLocalRepository : IntRepositoryImpl
    {
        public static readonly AudioLoadSucceededCountLocalRepository DefaultInstance = new AudioLoadSucceededCountLocalRepository();

        public AudioLoadSucceededCountLocalRepository(int defaultValue = 0) : base("au_load_succeeded", defaultValue)
        {
        }
    }
}