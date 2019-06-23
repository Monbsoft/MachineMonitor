using Monbsoft.MachineMonitor.Properties;

namespace Monbsoft.MachineMonitor.Configuration
{
    public class ConfigurationStore
    {
        public string Disk
        {
            get
            {
                return Settings.Default.Disk;
            }
            set
            {
                Settings.Default.Disk = value;
                Save();
            }
        }

        public string Network
        {
            get
            {
                return Settings.Default.Network;
            }
            set
            {
                Settings.Default.Network = value;
                Save();
            }
        }

        public bool Topmost
        {
            get
            {
                return Settings.Default.Topmost;
            }
            set
            {
                Settings.Default.Topmost = value;
                Save();
            }
        }


        public bool Transparent
        {
            get
            {
                return Settings.Default.Transparent;
            }
            set
            {
                Settings.Default.Transparent = value;
                Save();
            }
        }

        private void Save()
        {
            Settings.Default.Save();
        }
    }
}