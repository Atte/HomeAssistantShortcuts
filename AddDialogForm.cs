using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HomeAssistantShortcuts
{
    public partial class AddDialogForm : Form
    {
        private ServerConnection connection;
        private KeyEventArgs shortcut;

        public AddDialogForm(ServerConnection connection)
        {
            InitializeComponent();
            this.connection = connection;
        }

        public async Task LoadServices()
        {
            var services = await connection.ListServices();
            comboBoxPaths.Items.AddRange(services.ToArray());
            comboBoxPaths.Enabled = true;
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void AddDialogForm_Shown(object sender, EventArgs e)
        {
            _ = LoadServices();
        }

        private void textBoxShortcut_KeyDown(object sender, KeyEventArgs e)
        {
            shortcut = e;
            e.Handled = true;
            e.SuppressKeyPress = true;
            renderShortcut();
        }

        private void renderShortcut()
        {
            if (shortcut == null)
            {
                textBoxShortcut.Text = "<not set>";
                return;
            }

            var parts = new List<string>();
            if (shortcut.Control)
            {
                parts.Add("Ctrl");
            }
            if (shortcut.Alt)
            {
                parts.Add("Alt");
            }
            if (shortcut.Shift)
            {
                parts.Add("Shift");
            }
            switch (shortcut.KeyCode)
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
                    parts.Add("...");
                    break;
                default:
                    parts.Add(shortcut.KeyCode.ToString());
                    break;
            }
            textBoxShortcut.Text = String.Join(" + ", parts);
        }
    }
}
