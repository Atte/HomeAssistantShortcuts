using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Windows.Forms;

namespace HomeAssistantShortcuts
{
    public partial class MainForm : Form
    {
        private readonly ServerConnection connection = new ServerConnection();

        public MainForm()
        {
            InitializeComponent();
            applySettings();
        }

        private async void applySettings()
        {
            connection.BaseUrl = Properties.Settings.Default.ApiBaseUrl;
            connection.Token = Properties.Settings.Default.ApiAccessToken;
            try
            {
                textBoxServerStatus.Text = await connection.Ping();
            }
            catch (Exception err) when (err is HttpRequestException || err is JsonException)
            {
                textBoxServerStatus.Text = err.Message;
            }

            listShortcuts.BeginUpdate();
            listShortcuts.Items.Clear();
            foreach (var shortcut in Properties.Settings.Default.Shortcuts.Items)
            {
                var item = new ListViewItem(shortcut.Key);
                item.SubItems.Add(shortcut.Path);
                item.SubItems.Add(shortcut.Payload);
                item.Tag = shortcut;
                listShortcuts.Items.Add(item);
            }
            listShortcuts.EndUpdate();
        }

        private void buttonSaveServerConnection_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Save();
            applySettings();
        }

        private void notifyIcon_DoubleClick(object sender, EventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
            notifyIcon.Visible = false;
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                Hide();
                notifyIcon.Visible = true;
            }
        }

        private void buttonAddShortcut_Click(object sender, EventArgs e)
        {
            var dialog = new AddDialogForm(connection);
            dialog.ShowDialog();
        }

        private void buttonDeleteShortcuts_Click(object sender, EventArgs e)
        {
            foreach (
                var shortcut in
                from ListViewItem item
                in listShortcuts.SelectedItems
                select item.Tag as Shortcut
            )
            {
                Properties.Settings.Default.Shortcuts.Items.Remove(shortcut);
            }
            Properties.Settings.Default.Save();
            applySettings();
        }
    }
}
