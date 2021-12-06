#nullable enable
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HomeAssistantShortcuts
{
    public partial class AddDialogForm : Form
    {
        private readonly ServerConnection connection;
        private readonly Shortcut shortcut;

        public AddDialogForm(ServerConnection connection, Shortcut? shortcut = null)
        {
            this.InitializeComponent();
            this.connection = connection;
            this.shortcut = shortcut ?? new Shortcut();
            this.syncButtonStatuses();
        }

        public async Task LoadServices()
        {
            var services = await this.connection.ListServices();
            this.comboBoxPaths.Items.AddRange(services.ToArray());
            this.comboBoxPaths.Enabled = true;

            if (this.shortcut.Path != "")
            {
                this.comboBoxPaths.SelectedValue = this.shortcut.Path;
            }
            this.syncButtonStatuses();
        }

        private void syncButtonStatuses()
        {
            this.buttonAdd.Enabled = this.shortcut.KeyCode > 0 && !string.IsNullOrEmpty(this.shortcut.Path);
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Shortcuts.Add(this.shortcut);
            Properties.Settings.Default.Save();
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AddDialogForm_Shown(object sender, EventArgs e)
        {
            _ = this.LoadServices();
        }

        private void textBoxShortcut_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
            e.SuppressKeyPress = true;

            this.shortcut.SetFromEvent(e);
            this.renderShortcut();
            this.syncButtonStatuses();
        }

        private void renderShortcut()
        {
            var label = this.shortcut?.KeyLabel;
            if (string.IsNullOrEmpty(label))
            {
                this.textBoxShortcut.Text = "<not set>";
            }
            else
            {
                this.textBoxShortcut.Text = label;
            }
        }

        private void textBoxPayload_TextChanged(object sender, EventArgs e)
        {
            this.shortcut.Payload = this.textBoxPayload.Text;
            this.syncButtonStatuses();
        }

        private void comboBoxPaths_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selected = (Service)this.comboBoxPaths.SelectedItem;
            this.shortcut.Path = selected?.Path;
            this.textBoxPayload.PlaceholderText = selected?.PayloadPlaceholder;
            this.syncButtonStatuses();
        }
    }
}
