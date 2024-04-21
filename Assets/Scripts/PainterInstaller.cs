using PainterCanvas.Controller;
using PainterCanvas.Repository;
using PainterCanvas.View;
using Zenject;
using Tools.Repository;
using Tools.View;
using Tools.Controller;
using UnityEngine;

public class PainterInstaller : MonoInstaller
{
    [SerializeField] private ToolsView _toolsView;
    [SerializeField] private PainterCanvasView _painterCanvasView;
    public override void InstallBindings()
    {
        Container.Bind<IToolsView>().To<ToolsView>().FromInstance(_toolsView).AsSingle();
        Container.Bind<IToolsRepository>().To<ToolsRepository>().AsSingle();
        Container.Bind<IPainterCanvasView>().To<PainterCanvasView>().FromInstance(_painterCanvasView).AsSingle();
        Container.Bind<IPainterCanvasRepository>().To<PainterCanvasRepository>().AsSingle();
        
        Container.Bind<ToolsController>().AsSingle().NonLazy();
        Container.Bind<PainterCanvasController>().AsSingle().NonLazy();
    }
}