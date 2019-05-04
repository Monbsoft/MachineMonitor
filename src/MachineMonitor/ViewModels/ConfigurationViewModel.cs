using GalaSoft.MvvmLight;
using Monbsoft.MachineMonitor.Configuration;
using Monbsoft.MachineMonitor.Services;
using System;
using System.Collections.Generic;

namespace Monbsoft.MachineMonitor.ViewModels
{
    public class ConfigurationViewModel : ViewModelBase
    {
        #region Champs
        private readonly ConfigurationStore _configuration;
        private readonly NetworkService _networkService;
        private string _network;
        private bool _transparent;
        #endregion

        #region Constructeurs
        public ConfigurationViewModel(ConfigurationStore configuration, NetworkService networkService)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _networkService = networkService ?? throw new ArgumentNullException(nameof(networkService));
            Networks = _networkService.GetNetworks();
            SelectedNetwork = _configuration.Network;
            _transparent = _configuration.Transparent;
        }
        #endregion

        #region Propriétés
        public bool Transparent
        {
            get
            {
                return _transparent;
            }
            set
            {
                Set(ref _transparent, value);
                Transparent_Changed();
            }
        }
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

        private void Transparent_Changed()
        {
            _configuration.Transparent = _transparent;
        }
        #endregion
    }
}
