using Monbsoft.MachineMonitor.Views;
using System;
using System.Windows;

namespace Monbsoft.MachineMonitor
{
    /// <summary>
    /// Logique d'interaction pour App.xaml
    /// </summary>
    public partial class App : Application
    {
        private MainWindow _mainWindow;

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            _mainWindow = new MainWindow();

            var value = SystemParameters.VirtualScreenLeft;
            if (value >= 0)
            {
                value = SystemParameters.VirtualScreenWidth - SystemParameters.PrimaryScreenWidth;
            }
            else
            {
                value = -_mainWindow.Width;
            }

            _mainWindow.Left = value;
            _mainWindow.Top = 0;
            _mainWindow.Show();
        }

    }
}
