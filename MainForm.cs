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
            InitializeComponent();
#if DEBUG
            // flip tray icon
            using var bitmap = notifyIcon.Icon.ToBitmap();
            bitmap.RotateFlip(System.Drawing.RotateFlipType.RotateNoneFlipY);
            notifyIcon.Icon = System.Drawing.Icon.FromHandle(bitmap.GetHicon());

            var debugSuffix = " (DEBUG)";
            Text += debugSuffix;
            toolStripMenuItemTitle.Text += debugSuffix;
            notifyIcon.Text += debugSuffix;
            notifyIcon.Visible = false;
#endif
            applySettings();
        }

        // hide window by default in release builds
        protected override void SetVisibleCore(bool value)
        {
#if DEBUG
            // prevent warnings about unused variable
            _ = initialShow;
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
            connection.BaseUrl = Properties.Settings.Default.ApiBaseUrl;
            connection.Token = Properties.Settings.Default.ApiAccessToken;
            try
            {
                textBoxServerStatus.Text = await connection.Ping();
            }
            catch (Exception err)
            {
                textBoxServerStatus.Text = err.Message;
            }

            // clear old hotkeys
            deregisterHotkeys();

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
                var hotkey = shortcut.ToHotkey();
                if (!(hotkey is null) && !string.IsNullOrEmpty(shortcut.Path))
                {
                    hotkey.Pressed += async delegate
                    {
                        try
                        {
                            await connection.CallService(shortcut.Path!, shortcut.Payload);
                        }
                        catch (Exception err)
                        {
                            textBoxServerStatus.Text = err.Message;
                        }
                    };
                    hotkey.Register(this);
                    hotkeys.Add(hotkey);
                }
            }
            listShortcuts.EndUpdate();
        }

        private void deregisterHotkeys()
        {
            foreach (var hotkey in hotkeys)
            {
                if (hotkey.Registered)
                {
                    hotkey.Unregister();
                }
            }
            hotkeys.Clear();
        }

        private void buttonSaveServerConnection_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Save();
            applySettings();
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
            dialog.Shown += delegate { deregisterHotkeys(); };
            dialog.FormClosed += delegate { applySettings(); };
            dialog.ShowDialog();
        }

        private void buttonDeleteShortcuts_Click(object sender, EventArgs e)
        {

            Properties.Settings.Default.Shortcuts.RemoveAll(
                shortcut => listShortcuts.SelectedItems.Cast<ListViewItem>().Any(item => item.Tag == shortcut)
            );
            listShortcuts.SelectedItems.Clear();
            Properties.Settings.Default.Save();
            applySettings();
        }

        private void listShortcuts_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            buttonDeleteShortcuts.Enabled = listShortcuts.SelectedItems.Count > 0;
        }

        private void restoreFromTray()
        {
            initialShow = false;
            BringToFront();
            Show();
            WindowState = FormWindowState.Normal;
            notifyIcon.Visible = false;
        }

        private void notifyIcon_DoubleClick(object sender, EventArgs e)
        {
            restoreFromTray();
        }

        private void toolStripMenuItemOpen_Click(object sender, EventArgs e)
        {
            restoreFromTray();
        }

        private void toolStripMenuItemQuit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            deregisterHotkeys();
        }
    }
}
