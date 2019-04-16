using GalaSoft.MvvmLight;
using System;
using System.Diagnostics;
using System.Windows.Threading;

namespace Monbsoft.MachineMonitor.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly PerformanceCounter _cpuCounter;
        private readonly PerformanceCounter _diskCounter;
        private readonly PerformanceCounter _memoryCounter;
        private double _cpu;
        private double _disk;
        private double _ram;
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            _cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            _memoryCounter = new PerformanceCounter("Memory", "% Committed Bytes In Use");
            _diskCounter = new PerformanceCounter("PhysicalDisk", "% Disk Read Time", "_Total");
        }

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
        public void Refresh()
        {
            Cpu = _cpuCounter.NextValue();
            Ram = _memoryCounter.NextValue();
            Disk = _diskCounter.NextValue();
            Test();
        }

        private void Test()
        {
            //PerformanceCounter bandwidthCounter = new PerformanceCounter("Network Interface", "Current Bandwidth", networkCard);
            //float bandwidth = bandwidthCounter.NextValue();
        }
    }
}