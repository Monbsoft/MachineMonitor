/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:MachineMonitor"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;
using Monbsoft.MachineMonitor.Configuration;

namespace Monbsoft.MachineMonitor.ViewModels
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {

        #region Champs
        private static ViewModelLocator _current;
        #endregion

        #region Constructeurs
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);           
            SimpleIoc.Default.Register<ConfigurationStore>();
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<ConfigurationViewModel>();
        }
        #endregion

        #region Propriétés
        public static ViewModelLocator Current => _current ?? (_current = new ViewModelLocator());

        public ConfigurationViewModel Configuration
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ConfigurationViewModel>();
            }
        }

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }
        #endregion

        #region Méthodes
        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
        #endregion        
    }
}