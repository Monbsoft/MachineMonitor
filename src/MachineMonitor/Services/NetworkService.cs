using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Monbsoft.MachineMonitor.Services
{
    public class NetworkService
    {
        public List<string> GetNetworks()
        {
            List<string> networkList = new List<string>();
            PerformanceCounterCategory category = new PerformanceCounterCategory("Network Interface");
            foreach(var name in category.GetInstanceNames())
            {
                networkList.Add(name);
            }
            return networkList;
        }


    }
}
