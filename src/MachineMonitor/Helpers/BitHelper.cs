using System.Collections.Generic;

namespace Monbsoft.MachineMonitor.Helpers
{
    public static class BitHelper
    {
        private static readonly Dictionary<int, string> byteUnits = new Dictionary<int, string>
        {
            { 0, "bits/s" },
            { 1, "Kbits/s" },
            { 2, "Mbits/s" },
            { 3, "Gbits/s" },
            { 4, "Tbits/s" }
        };

        public static string DisplayByte(double bytes)
        {
            int index = 0;
            bool find = false;

            double rate = bytes * 8;
            double next;
            while (!find && index < 4)
            {
                next = rate / 1000;
                if (next >= 1.0d)
                {
                    index++;
                    rate = next;
                }
                else
                {
                    find = true;
                }
            }
            return $"{rate.ToString("F1")} {byteUnits[index]}";
        }
    }
}