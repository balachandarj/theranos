using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Practices.Prism;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Prism.UnityExtensions;
using Microsoft.Practices.Prism.Modularity;



namespace Reservation.UI
{
    public class Bootstrapper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            MainWindowView shell = Container.Resolve<MainWindowView>();
            shell.Show();

            return shell;
        }

        protected override void InitializeModules()
        {
            IModule module = Container.Resolve<ModuleConfigurator>();
            module.Initialize(); ;
        }
    }
}
