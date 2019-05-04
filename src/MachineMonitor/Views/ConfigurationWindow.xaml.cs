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
using System.Windows.Shapes;

namespace Monbsoft.MachineMonitor.Views
{
    /// <summary>
    /// Logique d'interaction pour ConfigurationWindow.xaml
    /// </summary>
    public partial class ConfigurationWindow : Window
    {
        #region Champs
        #endregion

        #region Constructeurs
        public ConfigurationWindow()
        {
            InitializeComponent();
            DataContext = ViewModel;
        }
        #endregion

        #region Propriétés
        public ConfigurationViewModel ViewModel
        {
            get { return ViewModelLocator.Current.Configuration; }
        }
        #endregion

        #region Méthodes
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        #endregion


    }
}
