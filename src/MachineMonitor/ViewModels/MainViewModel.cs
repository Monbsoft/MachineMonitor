using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using Monbsoft.MachineMonitor.Configuration;
using Monbsoft.MachineMonitor.Messages;
using Monbsoft.MachineMonitor.Views;
using System.Diagnostics;
using static Monbsoft.MachineMonitor.Messages.UpdatedConfigurationMessage;

namespace Monbsoft.MachineMonitor.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        #region Champs
        private readonly ConfigurationStore _configuration;
        private readonly PerformanceCounter _cpuCounter;
        private readonly PerformanceCounter _diskCounter;
        private readonly PerformanceCounter _memoryCounter;
        private readonly PerformanceCounter _networkCounter;
        private double _cpu;
        private double _disk;
        private double _network;
        private double _networkMax;
        private double _ram;
        private MainWindow _view;
        #endregion

        #region Constructeurs
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(ConfigurationStore configuration)
        {
            _cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            _memoryCounter = new PerformanceCounter("Memory", "% Committed Bytes In Use");
            _diskCounter = new PerformanceCounter("PhysicalDisk", "% Disk Read Time", "_Total");

            if (configuration != null && !string.IsNullOrEmpty(configuration.Network))
            {
                _networkCounter = new PerformanceCounter("Network Interface", "Bytes Received/sec", configuration.Network);
            }
            _configuration = configuration;

            // messages
            Messenger.Default.Register<UpdatedConfigurationMessage>(this, HandleUpdatedConfiguration);
        }
        #endregion

        #region Propriétés
        /// <summary>
        /// Gets or sets the percentage of the cpu usage.
        /// </summary>
        public double Cpu
        {
            get { return _cpu; }
            set { Set(ref _cpu, value); }
        }

        /// <summary>
        /// Gets or sets the percentage of the disk usage.
        /// </summary>
        public double Disk
        {
            get { return _disk; }
            set { Set(ref _disk, value); }
        }

        public double Network
        {
            get { return _network; }
            set { Set(ref _network, value); }
        }

        /// <summary>
        /// Gets or sets the percentage of the memory usage.
        /// </summary>
        public double Ram
        {
            get { return _ram; }
            set { Set(ref _ram, value); }
        }
        #endregion

        #region Méthodes
        /// <summary>
        /// Initialise le modèle de vue avec la vue spécifiée.
        /// </summary>
        /// <param name="view"></param>
        public void Initialize(MainWindow view)
        {
            _view = view;
            OnTransparencyChange(_configuration.Transparent);
        }
        public void Refresh()
        {
            Cpu = _cpuCounter.NextValue();
            Ram = _memoryCounter.NextValue();
            Disk = _diskCounter.NextValue();
            Network = GetPercentageNetwork();
        }

        private double GetPercentageNetwork()
        {
            if (_networkCounter == null)
            {
                return 0d;
            }
            double value = ((double)_networkCounter.NextValue() * 8) / 1000000;
            if (_networkMax < value)
            {
                _networkMax = value;
            }
            return value * 100 / _networkMax;
        }
        private void HandleUpdatedConfiguration(UpdatedConfigurationMessage updatedMessage)
        {
            if (updatedMessage.Changed == ChangedType.Transparent)
            {
                OnTransparencyChange(_configuration.Transparent);
            }
        }
        private void OnTransparencyChange(bool transparent)
        {
            if (transparent)
            {
                _view.ActivateTransparency();
            }
            else
            {
                _view.DeactiveTransparency();
            }
        }
        #endregion
    }
}