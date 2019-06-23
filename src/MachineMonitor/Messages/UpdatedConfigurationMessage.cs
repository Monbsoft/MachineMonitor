using System;

namespace Monbsoft.MachineMonitor.Messages
{
    public class UpdatedConfigurationMessage
    {
        public UpdatedConfigurationMessage(ChangedType type)
        {
            Id = Guid.NewGuid();
            Date = DateTime.Now;
            Changed = type;
        }

        public enum ChangedType
        {
            Disk = 0,
            Network = 1,
            Transparent = 2,
            Topmost = 3
        }

        public ChangedType Changed { get; set; }
        public DateTime Date { get; }
        public Guid Id { get; }
    }
}