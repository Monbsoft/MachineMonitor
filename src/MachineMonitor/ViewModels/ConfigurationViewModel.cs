using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Monbsoft.MachineMonitor.ViewModels
{
    public class ConfigurationViewModel : ViewModelBase
    {
        #region Champs
        #endregion

        #region Constructeurs
        public ConfigurationViewModel()
        {
            Initialize();
        }
        #endregion

        #region Propriétés
        public List<NetworkInterface> Networks
        {
            get;
            private set;
        }
        #endregion

        #region Méthodes
        private void Initialize()
        {
            Networks = NetworkInterface.GetAllNetworkInterfaces()
                .Where(x => x.Speed > 0
                    && x.Supports(NetworkInterfaceComponent.IPv4)
                    && x.Supports(NetworkInterfaceComponent.IPv6)
                    && x.OperationalStatus == OperationalStatus.Up
                    && IsCompatibleInterface(x.NetworkInterfaceType))
                    .ToList();
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
        #endregion
    }
}
