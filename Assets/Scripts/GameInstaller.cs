using System.ComponentModel;
using Zenject;

public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<GameInput>().AsSingle().NonLazy();

        Container.Bind<InputManager>()
            .FromComponentInHierarchy()
            .AsSingle();

        Container.Bind<PlayerController>()
            .FromComponentInHierarchy()
            .AsSingle();
    }
}