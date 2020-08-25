﻿using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HomeAssistantShortcuts
{
    public partial class AddDialogForm : Form
    {
        private ServerConnection connection;
        private Shortcut shortcut;

        public AddDialogForm(ServerConnection connection, Shortcut shortcut = null)
        {
            InitializeComponent();
            this.connection = connection;
            this.shortcut = shortcut ?? new Shortcut();
            syncButtonStatuses();
        }

        public async Task LoadServices()
        {
            var services = await connection.ListServices();
            comboBoxPaths.Items.AddRange(services.ToArray());
            comboBoxPaths.Enabled = true;

            if (shortcut.Path != "")
            {
                comboBoxPaths.SelectedValue = shortcut.Path;
            }
            syncButtonStatuses();
        }

        private void syncButtonStatuses()
        {
            buttonAdd.Enabled = shortcut.KeyCode > 0 && shortcut.Path != "";
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Shortcuts.Add(shortcut);
            Properties.Settings.Default.Save();
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
            e.Handled = true;
            e.SuppressKeyPress = true;

            shortcut.SetFromEvent(e);
            renderShortcut();
            syncButtonStatuses();
        }

        private void renderShortcut()
        {
            var label = shortcut?.KeyLabel;
            if (label is null || label == "")
            {
                textBoxShortcut.Text = "<not set>";
            }
            else
            {
                textBoxShortcut.Text = label;
            }
        }

        private void textBoxPayload_TextChanged(object sender, EventArgs e)
        {
            shortcut.Payload = textBoxPayload.Text;
            syncButtonStatuses();
        }

        private void comboBoxPaths_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selected = (Service)comboBoxPaths.SelectedItem;
            shortcut.Path = selected?.Path;
            syncButtonStatuses();
        }
    }
}
