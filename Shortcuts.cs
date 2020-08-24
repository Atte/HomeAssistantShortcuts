using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace HomeAssistantShortcuts
{
    class Shortcut
    {
        public string Key { get; set; }
        public string Path { get; set; }
        public string Payload { get; set; }
    }

    [SettingsSerializeAs(SettingsSerializeAs.Xml)]
    class Shortcuts
    {
        public List<Shortcut> Items { get; set; } = new List<Shortcut>();

        public Shortcuts() { }
    }
}
