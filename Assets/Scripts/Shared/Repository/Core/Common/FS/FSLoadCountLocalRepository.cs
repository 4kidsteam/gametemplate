namespace Shared.Repository
{
    public class FSLoadCountRepository : IntRepositoryImpl
    {
        public static readonly FSLoadCountRepository DefaultInstance = new FSLoadCountRepository();

        public FSLoadCountRepository(int defaultValue = 0) : base("fs_load", defaultValue)
        {
        }
    }
}