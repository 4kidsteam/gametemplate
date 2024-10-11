using System.Collections.Generic;
using Shared.Utils;

namespace Shared.Repository
{
    public class AudioClickedCountLocalRepository : IntRepositoryImpl
    {
        const string KEY = "au_clicked";
        
        public AudioClickedCountLocalRepository(int defaultValue = 0) : base(KEY, defaultValue)
        {
        }

        public static readonly AudioClickedCountLocalRepository DefaultInstance = new AudioClickedCountLocalRepository();
        public readonly static IInstanceManager<IIntRepository> InstanceManager = new IntRepositoryInstanceManager(keyPrefix: KEY);
    }
}