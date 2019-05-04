using Monbsoft.MachineMonitor.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Monbsoft.MachineMonitor.Views
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Champs
        private const double Opaque = 1d;
        private const double Transparency = 0.5d;
        private DispatcherTimer _timer;
        #endregion

        #region Constructeurs
        public MainWindow()
        {
            InitializeComponent();
            DataContext = ViewModel;
            ViewModel.Initialize(this);
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromMilliseconds(500);
            _timer.Tick += Timer_Tick;
        }
        #endregion

        #region Propriétés
        public MainViewModel ViewModel
        {
            get { return ViewModelLocator.Current.Main; }
        }
        #endregion

        #region Méthodes
        public void ActivateTransparency()
        {
            Activated += Window_Activated;
            Deactivated += Window_Deactivated;
            Opacity = Transparency;
        }
        public void DeactiveTransparency()
        {
            Activated -= Window_Activated;
            Deactivated -= Window_Deactivated;
        }
        private void CloseMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void ConflgurationMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new ConfigurationWindow();
            dlg.Owner = this;
            dlg.ShowDialog();
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            ViewModel.Refresh();
        }
        private void Window_Activated(object sender, System.EventArgs e)
        {
            Opacity = Opaque;
        }
        private void Window_Deactivated(object sender, System.EventArgs e)
        {
            Opacity = Transparency;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
            _timer.Start();
        }
        #endregion

    }
}
