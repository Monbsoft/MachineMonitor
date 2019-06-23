using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monbsoft.MachineMonitor.Helpers
{
    public static class OctetHelper
    {
        private static readonly Dictionary<int, string> octetUnits = new Dictionary<int, string>
        {
            {0, "Mo" },
            {1, "Go" },
            {2, "To" },
            {3, "Po" }
        };
        public static string DisplayMega(double mega)
        {
            bool find = false;
            double value = mega;
            int index = 0;
            while(!find && index <3)
            {
                double next = value / 1024;
                if(next >= 1.0)
                {
                    value = next;
                }
                else
                {
                    index++;
                    find = true;

                }
            }

            return $"{value.ToString("F1")} {octetUnits[index]}";
        }
    }
}
