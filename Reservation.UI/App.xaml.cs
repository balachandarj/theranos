using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Reservation.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        IUnityContainer container;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var bootstrapper = new Bootstrapper();
            bootstrapper.Run();

            //ConfigureContainer();
            SetApplicationThemes();
            //Application.Current.MainWindow.Show();
        }        

        //private void ConfigureContainer()
        //{
        //    container = new UnityContainer();
        //    Application.Current.MainWindow = container.Resolve<MainWindowView>();
        //}

        private void SetApplicationThemes()
        {
            Xceed.Wpf.AvalonDock.Themes.MetroTheme theme = new Xceed.Wpf.AvalonDock.Themes.MetroTheme();
            var themeUri = theme.GetResourceUri();
            var dictionary = new ResourceDictionary() { Source = themeUri };
            this.Resources.MergedDictionaries.Add(dictionary);
        }
    }
}
