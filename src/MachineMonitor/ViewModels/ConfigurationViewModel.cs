using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using Monbsoft.MachineMonitor.Configuration;
using Monbsoft.MachineMonitor.Messages;
using Monbsoft.MachineMonitor.Services;
using System;
using System.Collections.Generic;
using static Monbsoft.MachineMonitor.Messages.UpdatedConfigurationMessage;

namespace Monbsoft.MachineMonitor.ViewModels
{
    public class ConfigurationViewModel : ViewModelBase
    {
        #region Champs
        private readonly ConfigurationStore _configuration;
        private string _disk;
        private string _network;
        private bool _transparent;
        #endregion

        #region Constructeurs
        public ConfigurationViewModel(
            ConfigurationStore configuration,
            NetworkService networkService,
            DiskService diskService)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            if (networkService == null)
            {
                throw new ArgumentNullException(nameof(networkService));
            }
            if (diskService == null)
            {
                throw new ArgumentNullException(nameof(diskService));
            }

            Networks = networkService.GetNetworks();
            Disks = diskService.GetDisks();
            _disk = _configuration.Disk;
            _network = _configuration.Network;
            _transparent = _configuration.Transparent;
        }
        #endregion

        #region Propriétés
        public List<string> Disks
        {
            get;
            private set;
        }
        public List<string> Networks
        {
            get;
            private set;
        }

        public string SelectedDisk
        {
            get
            {
                return _disk;
            }
            set
            {
                Set(ref _disk, value);
                Disk_Changed();
            }
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
        #endregion

        #region Méthodes
        private static void SendMessage(ChangedType type)
        {
            Messenger.Default.Send<UpdatedConfigurationMessage>(new UpdatedConfigurationMessage(type));
        }
        private void Disk_Changed()
        {
            _configuration.Disk = SelectedDisk;
            SendMessage(ChangedType.Disk);
        }
        private void Network_Changed()
        {
            _configuration.Network = SelectedNetwork;
            SendMessage(ChangedType.Network);
        }

        private void Transparent_Changed()
        {
            _configuration.Transparent = _transparent;
            SendMessage(ChangedType.Transparent);
        }
        #endregion
    }
}