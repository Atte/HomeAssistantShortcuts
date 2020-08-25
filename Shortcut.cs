using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Windows.Forms;

namespace HomeAssistantShortcuts
{
    [Serializable]
    public class Shortcut
    {
        public bool Control = false;
        public bool Alt = false;
        public bool Shift = false;
        public Keys? KeyCode = 0;
        public string Path = "";
        public string Payload = "";

        public string KeyLabel {
            get {
                var parts = new List<string>();
                if (Control)
                {
                    parts.Add("Ctrl");
                }
                if (Alt)
                {
                    parts.Add("Alt");
                }
                if (Shift)
                {
                    parts.Add("Shift");
                }
                if (KeyCode is null)
                {
                    parts.Add("...");
                } else { 
                    parts.Add(KeyCode.ToString());
                }
                return string.Join(" + ", parts);
            }
        }

        public Shortcut() { }

        public void SetFromEvent(KeyEventArgs keyEvent)
        {
            Control = keyEvent.Control;
            Alt = keyEvent.Alt;
            Shift = keyEvent.Shift;
            switch (keyEvent.KeyCode)
            {
                case Keys.ControlKey:
                case Keys.LControlKey:
                case Keys.RControlKey:
                case Keys.Alt:
                case Keys.Menu:
                case Keys.LMenu:
                case Keys.RMenu:
                case Keys.ShiftKey:
                case Keys.LShiftKey:
                case Keys.RShiftKey:
                    KeyCode = null;
                    break;
                default:
                    KeyCode = keyEvent.KeyCode;
                    break;
            }
        }
    }
}
