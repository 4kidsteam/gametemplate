namespace Shared.Repository
{
    public class AudioLoadCountLocalRepository : IntRepositoryImpl
    {
        public static readonly AudioLoadCountLocalRepository DefaultInstance = new AudioLoadCountLocalRepository();

        public AudioLoadCountLocalRepository(int defaultValue = 0) : base("au_load", defaultValue)
        {
        }
    }
}