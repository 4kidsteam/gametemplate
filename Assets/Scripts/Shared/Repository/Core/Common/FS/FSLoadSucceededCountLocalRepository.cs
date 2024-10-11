namespace Shared.Repository
{
    public class FSLoadSucceededCountRepository : IntRepositoryImpl
    {
        public static readonly FSLoadSucceededCountRepository DefaultInstance = new FSLoadSucceededCountRepository();

        public FSLoadSucceededCountRepository(int defaultValue = 0) : base("fs_load_succeeded", defaultValue)
        {
        }
    }
}