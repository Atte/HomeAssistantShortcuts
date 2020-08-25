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
        private readonly List<Hotkey> hotkeys = new List<Hotkey>();
        private bool initialShow = true;

        public MainForm()
        {
            InitializeComponent();
            applySettings();
        }

        protected override void SetVisibleCore(bool value)
        {
            if (initialShow)
            {
                value = false;
                if (!IsHandleCreated) CreateHandle();
            }
            base.SetVisibleCore(value);
        }

        private async void applySettings()
        {
            // server status
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

            // clear old hotkeys
            foreach (var hotkey in hotkeys)
            {
                if (hotkey.Registered)
                {
                    hotkey.Unregister();
                }
            }
            hotkeys.Clear();

            // add shortcuts to list and register handlers
            listShortcuts.BeginUpdate();
            listShortcuts.Items.Clear();
            foreach (var shortcut in Properties.Settings.Default.Shortcuts)
            {
                // add to list
                var item = new ListViewItem(shortcut.KeyLabel);
                item.SubItems.Add(shortcut.Path);
                item.SubItems.Add(shortcut.Payload);
                item.Tag = shortcut;
                listShortcuts.Items.Add(item);

                // register handler
                if (!(shortcut.KeyCode is null))
                {
                    var hotkey = new Hotkey((Keys)shortcut.KeyCode, shortcut.Shift, shortcut.Control, shortcut.Alt, false);
                    hotkey.Pressed += async delegate { await connection.CallService(shortcut.Path, shortcut.Payload); };
                    hotkey.Register(this);
                }
            }
            listShortcuts.EndUpdate();
            listShortcuts.Update();
        }

        private void buttonSaveServerConnection_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Save();
            applySettings();
        }

        private void notifyIcon_DoubleClick(object sender, EventArgs e)
        {
            initialShow = false;
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
            dialog.FormClosed += delegate { applySettings(); };
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
                Properties.Settings.Default.Shortcuts.Remove(shortcut);
            }
            Properties.Settings.Default.Save();
            applySettings();
        }

        private void listShortcuts_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonDeleteShortcuts.Enabled = listShortcuts.SelectedItems.Count > 0;
        }
    }
}
