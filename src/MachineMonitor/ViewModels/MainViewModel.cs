using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using Monbsoft.MachineMonitor.Configuration;
using Monbsoft.MachineMonitor.Messages;
using Monbsoft.MachineMonitor.Views;
using System;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Windows.Threading;
using static Monbsoft.MachineMonitor.Messages.UpdatedConfigurationMessage;

namespace Monbsoft.MachineMonitor.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        #region Champs
        private const double Mega = 1048576d;
        private readonly ConfigurationStore _configuration;
        private readonly PerformanceCounter _cpuCounter;
        private readonly PerformanceCounter _memoryCounter;
        private readonly DispatcherTimer _timer;
        private double _cpu;
        private double _disk;
        private PerformanceCounter _diskCounter;
        private double _memoryTotal;
        private double _network;
        private PerformanceCounter _networkCounter;
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
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            _memoryCounter = new PerformanceCounter("Memory", "Available MBytes");

            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromMilliseconds(500);
            _timer.Tick += Timer_Tick;

            // messages
            Messenger.Default.Register<UpdatedConfigurationMessage>(this, HandleUpdatedConfiguration);

            OnDiskChange();
            OnNetworkChange();
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

        /// <summary>
        /// Gets or sets the percentage of the network usage.
        /// </summary>
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

            // récupère le total de mémoire
            ManagementObjectSearcher searcher =
                new ManagementObjectSearcher(
                    "SELECT * FROM Win32_ComputerSystem");

            foreach(var mobject in searcher.Get())
            {
                _memoryTotal = ToDouble(mobject, "TotalPhysicalMemory", Mega);
            }

        }

        /// <summary>
        /// Starts the counters.
        /// </summary>
        public void Start()
        {
            _timer.Start();
        }
        private static double ToDouble(ManagementBaseObject mo, string name, double divider)
        {
            var value = mo[name];
            return value == null
                ? 0
                : (double.TryParse(value.ToString(), out double result) ? result / divider : 0);
        }
        private double CalculatePercent()
        {
            double free = _memoryCounter.NextValue();
            double use = (_memoryTotal - free);
            double percent = Math.Round((use / _memoryTotal) * 100, 2);
            return percent;
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
            _timer.Stop();
            switch(updatedMessage.Changed)
            {
                case ChangedType.Disk:
                    {
                        OnDiskChange();
                        break;
                    }
                case ChangedType.Network:
                    {
                        OnNetworkChange();
                        break;
                    }
                case ChangedType.Transparent:
                    {
                        OnTransparencyChange(_configuration.Transparent);
                        break;
                    }
                    
                default:
                    {
                        break;
                    }
            }
            _timer.Start();

        }
        private void OnDiskChange()
        {
            _diskCounter = new PerformanceCounter("PhysicalDisk", "% Disk Time",_configuration.Disk);
        }

        private void OnNetworkChange()
        {
            if(string.IsNullOrEmpty(_configuration.Network))
            {
                _networkCounter = null;
                return;
            }
            _networkCounter = new PerformanceCounter("Network Interface", "Bytes Received/sec", _configuration.Network);
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
        private void Timer_Tick(object sender, EventArgs e)
        {
            Cpu = _cpuCounter.NextValue();
            Ram = CalculatePercent();
            Disk = _diskCounter.NextValue();
            Network = GetPercentageNetwork();
        }
        #endregion
    }
}