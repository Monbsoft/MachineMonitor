using GalaSoft.MvvmLight;
using Monbsoft.MachineMonitor.Views;
using System;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Windows.Threading;

namespace Monbsoft.MachineMonitor.ViewModels
{
    public class MainViewModel : ViewModelBase
    {

        #region Champs
        private readonly PerformanceCounter _cpuCounter;
        private readonly PerformanceCounter _diskCounter;
        private readonly PerformanceCounter _memoryCounter;
        private double _cpu;
        private double _disk;
        private double _ram;
        private MainWindow _view;
        #endregion

        #region Constructeurs
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            _cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            _memoryCounter = new PerformanceCounter("Memory", "% Committed Bytes In Use");
            _diskCounter = new PerformanceCounter("PhysicalDisk", "% Disk Read Time", "_Total");
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
        /// Gets or sets the percentage of the memory usage.
        /// </summary>
        public double Ram
        {
            get { return _ram; }
            set { Set(ref _ram, value); }
        }
        #endregion

        #region Méthodes
        public void Initialize(MainWindow view)
        {
            _view = view;
        }
        public void Refresh()
        {
            Cpu = _cpuCounter.NextValue();
            Ram = _memoryCounter.NextValue();
            Disk = _diskCounter.NextValue();
            Test();
        }

        private static bool IsCompatibleInterface(NetworkInterfaceType nit)
        {
            switch (nit)
            {
                case NetworkInterfaceType.Loopback:
                case NetworkInterfaceType.HighPerformanceSerialBus:
                case NetworkInterfaceType.Ppp:
                    return false;
                default:
                    return true;
            }
        }

        private void Test()
        {
            var interfaces = NetworkInterface.GetAllNetworkInterfaces()
                .Where(x => x.Speed > 0
                    && x.Supports(NetworkInterfaceComponent.IPv4)
                    && x.Supports(NetworkInterfaceComponent.IPv6)
                    && x.OperationalStatus == OperationalStatus.Up
                    && IsCompatibleInterface(x.NetworkInterfaceType)).ToArray();
        }
        #endregion

    }
}