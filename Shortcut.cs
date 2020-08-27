#nullable enable
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace HomeAssistantShortcuts
{
    [Serializable]
    public sealed class Shortcut
    {
        public bool Control = false;
        public bool Alt = false;
        public bool Shift = false;
        public Keys? KeyCode = 0;
        public string? Path = null;
        public string Payload = "";

        public string KeyLabel
        {
            get
            {
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
                }
                else if (KeyCode >= Keys.D0 && KeyCode <= Keys.D9)
                {
                    parts.Add(KeyCode.ToString().Substring(1));
                }
                else
                {
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

        public Hotkey? ToHotkey() => KeyCode is null ? null : new Hotkey(KeyCode.Value, Shift, Control, Alt, false);
    }
}
