﻿using Monbsoft.MachineMonitor.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monbsoft.MachineMonitor.Configuration
{
    public class ConfigurationStore
    {
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
