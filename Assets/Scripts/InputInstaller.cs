using Zenject;

public class InputInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<GameInput>().AsSingle();

        Container.Bind<IScoreService>()
     .To<ScoreService>()
     .AsSingle();
    }
}