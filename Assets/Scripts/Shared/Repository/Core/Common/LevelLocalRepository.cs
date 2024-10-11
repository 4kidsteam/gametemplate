public class LevelLocalRepository : IntRepositoryImpl
{
    public static readonly LevelLocalRepository DefaultInstance = new LevelLocalRepository();

    public LevelLocalRepository(int defaultValue = 1) : base("LEVEL", defaultValue)
    {
    }
}
