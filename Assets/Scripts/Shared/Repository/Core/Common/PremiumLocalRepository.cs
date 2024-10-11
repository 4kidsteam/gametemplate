
public class PremiumLocalRepository : BoolRepositoryImpl
{
    public static readonly PremiumLocalRepository DefaultInstance = new PremiumLocalRepository();

    public PremiumLocalRepository(bool defaultValue = false) : base("IsPremiumEnable", defaultValue)
    {
    }
}