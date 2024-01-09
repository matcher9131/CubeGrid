using CubeGrid.Models;
using CubeGrid.Views;
using Prism.Ioc;
using System.Windows;

namespace CubeGrid
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<CubicData>();
            containerRegistry.RegisterSingleton<CrossSectionGridOptions>();
        }
    }
}
