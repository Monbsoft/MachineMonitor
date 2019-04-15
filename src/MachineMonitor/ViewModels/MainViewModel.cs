using GalaSoft.MvvmLight;
using System.Windows.Threading;

namespace Monbsoft.MachineMonitor.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private DispatcherTimer _timer;
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            _timer = new DispatcherTimer();
        }

        public void Refresh()
        {

        }
    }
}