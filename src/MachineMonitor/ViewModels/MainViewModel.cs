using GalaSoft.MvvmLight;
using System;
using System.Diagnostics;
using System.Windows.Threading;

namespace Monbsoft.MachineMonitor.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly PerformanceCounter _cpuCounter;
        private readonly PerformanceCounter _memoryCounter;
        private double _cpu;
        private double _ram;
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            _cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            _memoryCounter = new PerformanceCounter("Memory", "% Committed Bytes In Use");
        }

        /// <summary>
        /// Gets or sets the percentage of the cpu.
        /// </summary>
        public double Cpu
        {
            get { return _cpu; }
            set { Set(ref _cpu, value); }
        }

        public double Ram
        {
            get { return _ram; }
            set { Set(ref _ram, value); }
        }

        public void Refresh()
        {
            Cpu = _cpuCounter.NextValue();
            Ram = _memoryCounter.NextValue();
            Console.WriteLine($"CPU = {_cpu} RAM = {_ram}");

            
        }
    }
}