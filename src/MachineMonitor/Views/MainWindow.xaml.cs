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
        private DispatcherTimer _timer;
        #endregion

        #region Constructeurs
        public MainWindow()
        {
            InitializeComponent();
            DataContext = ViewModel;
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
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
            _timer.Start();
        }
        #endregion

    }
}
