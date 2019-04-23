using GalaSoft.MvvmLight;
using Monbsoft.MachineMonitor.Configuration;
using Monbsoft.MachineMonitor.Services;
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
        private readonly ConfigurationStore _configuration;
        private readonly NetworkService _networkService;
        private string _network;
        #endregion

        #region Constructeurs
        public ConfigurationViewModel(ConfigurationStore configuration, NetworkService networkService)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _networkService = networkService ?? throw new ArgumentNullException(nameof(networkService));
            Initialize();
        }
        #endregion

        #region Propriétés
        public List<string> Networks
        {
            get;
            private set;
        }

        public string SelectedNetwork
        {
            get { return _network; }
            set
            {
                Set(ref _network, value);
                Network_Changed();
            }
        }
        #endregion

        #region Méthodes


        private void Network_Changed()
        {
            _configuration.Network = SelectedNetwork;
        }
        private void Initialize()
        {
            Networks = _networkService.GetNetworks();
        }
        #endregion
    }
}
