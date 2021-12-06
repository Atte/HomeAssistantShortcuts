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
        public Keys? KeyCode = Keys.None;
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

                parts.Add(KeyCode switch
                {
                    null => "...",
                    Keys code when code >= Keys.D0 && code <= Keys.D9 => code.ToString()[1..],
                    Keys code => code.ToString(),
                });

                return string.Join(" + ", parts);
            }
        }

        public Shortcut() { }

        public void SetFromEvent(KeyEventArgs keyEvent)
        {
            Control = keyEvent.Control;
            Alt = keyEvent.Alt;
            Shift = keyEvent.Shift;
            KeyCode = keyEvent.KeyCode switch
            {
                Keys.ControlKey or Keys.LControlKey or Keys.RControlKey or Keys.Alt or Keys.Menu or Keys.LMenu or Keys.RMenu or Keys.ShiftKey or Keys.LShiftKey or Keys.RShiftKey => null,
                Keys code => code,
            };
        }

        public Hotkey? ToHotkey() => KeyCode is null ? null : new Hotkey(KeyCode.Value, Shift, Control, Alt, false);
    }
}
