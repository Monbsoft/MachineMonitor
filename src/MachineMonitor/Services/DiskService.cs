using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monbsoft.MachineMonitor.Services
{
    public class DiskService
    {
        public List<string> GetDisks()
        {
            var category = new PerformanceCounterCategory("PhysicalDisk");
            return category.GetInstanceNames().OrderBy(i => i).ToList();
        }
    }
}
