using Zenject;
using Tools.Repository;
using Tools.View;
using Tools.Controller;
using UnityEngine;

public class PainterInstaller : MonoInstaller
{
    [SerializeField] private ToolsView _toolsView;
    public override void InstallBindings()
    {
        Container.Bind<IToolsView>().To<ToolsView>().FromInstance(_toolsView).AsSingle();
        Container.Bind<IToolsRepository>().To<ToolsRepository>().AsSingle();
        
        Container.Bind<ToolsController>().AsSingle().NonLazy();
    }
}