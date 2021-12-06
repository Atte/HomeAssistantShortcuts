#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
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
            this.InitializeComponent();
#if DEBUG
            // flip tray icon
            using var bitmap = this.notifyIcon.Icon.ToBitmap();
            bitmap.RotateFlip(System.Drawing.RotateFlipType.RotateNoneFlipY);
            this.notifyIcon.Icon = System.Drawing.Icon.FromHandle(bitmap.GetHicon());

            var debugSuffix = " (DEBUG)";
            this.Text += debugSuffix;
            this.toolStripMenuItemTitle.Text += debugSuffix;
            this.notifyIcon.Text += debugSuffix;
            this.notifyIcon.Visible = false;
#endif
            this.applySettings();
        }

        // hide window by default in release builds
        protected override void SetVisibleCore(bool value)
        {
#if DEBUG
            // prevent warnings about unused variable
            _ = this.initialShow;
#else
            if (initialShow)
            {
                value = false;
                if (!IsHandleCreated) CreateHandle();
            }
#endif
            base.SetVisibleCore(value);
        }

        private async void applySettings()
        {
            // server status
            this.connection.BaseUrl = Properties.Settings.Default.ApiBaseUrl;
            this.connection.Token = Properties.Settings.Default.ApiAccessToken;
            try
            {
                this.textBoxServerStatus.Text = await this.connection.Ping();
            }
            catch (Exception err)
            {
                this.textBoxServerStatus.Text = err.Message;
            }

            // clear old hotkeys
            this.deregisterHotkeys();

            // add shortcuts to list and register handlers
            this.listShortcuts.BeginUpdate();
            this.listShortcuts.Items.Clear();
            foreach (var shortcut in Properties.Settings.Default.Shortcuts)
            {
                // add to list
                var item = new ListViewItem(shortcut.KeyLabel);
                item.SubItems.Add(shortcut.Path);
                item.SubItems.Add(shortcut.Payload);
                item.Tag = shortcut;
                this.listShortcuts.Items.Add(item);

                // register handler
                var hotkey = shortcut.ToHotkey();
                if (!(hotkey is null) && !string.IsNullOrEmpty(shortcut.Path))
                {
                    hotkey.Pressed += async delegate
                    {
                        try
                        {
                            await this.connection.CallService(shortcut.Path!, shortcut.Payload);
                        }
                        catch (Exception err)
                        {
                            this.textBoxServerStatus.Text = err.Message;
                        }
                    };
                    hotkey.Register(this);
                    this.hotkeys.Add(hotkey);
                }
            }
            this.listShortcuts.EndUpdate();
        }

        private void deregisterHotkeys()
        {
            foreach (var hotkey in this.hotkeys)
            {
                if (hotkey.Registered)
                {
                    hotkey.Unregister();
                }
            }
            this.hotkeys.Clear();
        }

        private void buttonSaveServerConnection_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Save();
            this.applySettings();
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
                this.notifyIcon.Visible = true;
            }
        }

        private void buttonAddShortcut_Click(object sender, EventArgs e)
        {
            var dialog = new AddDialogForm(this.connection);
            dialog.Shown += delegate { this.deregisterHotkeys(); };
            dialog.FormClosed += delegate { this.applySettings(); };
            dialog.ShowDialog();
        }

        private void buttonDeleteShortcuts_Click(object sender, EventArgs e)
        {

            Properties.Settings.Default.Shortcuts.RemoveAll(
                shortcut => this.listShortcuts.SelectedItems.Cast<ListViewItem>().Any(item => item.Tag == shortcut)
            );
            this.listShortcuts.SelectedItems.Clear();
            Properties.Settings.Default.Save();
            this.applySettings();
        }

        private void listShortcuts_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            this.buttonDeleteShortcuts.Enabled = this.listShortcuts.SelectedItems.Count > 0;
        }

        private void restoreFromTray()
        {
            this.initialShow = false;
            this.BringToFront();
            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.notifyIcon.Visible = false;
        }

        private void notifyIcon_DoubleClick(object sender, EventArgs e)
        {
            this.restoreFromTray();
        }

        private void toolStripMenuItemOpen_Click(object sender, EventArgs e)
        {
            this.restoreFromTray();
        }

        private void toolStripMenuItemQuit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.deregisterHotkeys();
        }
    }
}
