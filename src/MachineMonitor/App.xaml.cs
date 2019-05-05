using Monbsoft.MachineMonitor.Views;
using System;
using System.Windows;
using Forms = System.Windows.Forms;


namespace Monbsoft.MachineMonitor
{
    /// <summary>
    /// Logique d'interaction pour App.xaml
    /// </summary>
    public partial class App : Application
    {
        private MainWindow _mainWindow;
        private Forms.NotifyIcon _notifyIcon;

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

            _notifyIcon = new Forms.NotifyIcon();
            _notifyIcon.DoubleClick += (s, args) => ActivateWindow();
            _notifyIcon.Icon = MachineMonitor.Properties.Resource.machineicon;
            _notifyIcon.Visible = true;

            _mainWindow.Left = value;
            _mainWindow.Top = 0;
            _mainWindow.Show();
        }

        private void ActivateWindow()
        {
            _mainWindow.Activate();
        }

    }
}
