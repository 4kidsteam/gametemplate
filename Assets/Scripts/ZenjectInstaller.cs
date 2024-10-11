using UnityEngine;
using Zenject;

public class ZenjectInstaller : MonoInstaller
{
    const string TAG = "ZenjectInstaller";
    public override void InstallBindings()
    {
#if LOG_INFO
        Debug.LogFormat("{0}->InstallBindings", TAG);
#endif
        this.Container.Bind<IBoolRepository>().WithId("PremiumRepository").FromInstance(PremiumLocalRepository.DefaultInstance).AsSingle();
        //this.Container.Bind<ILevelDataController>().FromInstance(new LevelDataController().Initialize("levels")).AsSingle();
        Shared.Class1 class1 = new Shared.Class1();
        class1.Log();
    }
}