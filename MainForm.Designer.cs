namespace HomeAssistantShortcuts
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.GroupBox groupBoxServerConnection;
            System.Windows.Forms.Label labelServerStatus;
            System.Windows.Forms.Label labelApiAccessToken;
            System.Windows.Forms.Label labelApiBaseUrl;
            System.Windows.Forms.ColumnHeader columnKeyLabel;
            System.Windows.Forms.ColumnHeader columnPath;
            System.Windows.Forms.ColumnHeader columnPayload;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.buttonSaveServerConnection = new System.Windows.Forms.Button();
            this.textBoxServerStatus = new System.Windows.Forms.TextBox();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.listShortcuts = new System.Windows.Forms.ListView();
            this.buttonDeleteShortcuts = new System.Windows.Forms.Button();
            this.buttonAddShortcut = new System.Windows.Forms.Button();
            this.textBoxApiAccessToken = new System.Windows.Forms.TextBox();
            this.textBoxApiBaseUrl = new System.Windows.Forms.TextBox();
            groupBoxServerConnection = new System.Windows.Forms.GroupBox();
            labelServerStatus = new System.Windows.Forms.Label();
            labelApiAccessToken = new System.Windows.Forms.Label();
            labelApiBaseUrl = new System.Windows.Forms.Label();
            columnKeyLabel = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            columnPath = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            columnPayload = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            groupBoxServerConnection.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxServerConnection
            // 
            groupBoxServerConnection.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            groupBoxServerConnection.Controls.Add(labelServerStatus);
            groupBoxServerConnection.Controls.Add(this.buttonSaveServerConnection);
            groupBoxServerConnection.Controls.Add(this.textBoxServerStatus);
            groupBoxServerConnection.Controls.Add(labelApiAccessToken);
            groupBoxServerConnection.Controls.Add(this.textBoxApiAccessToken);
            groupBoxServerConnection.Controls.Add(this.textBoxApiBaseUrl);
            groupBoxServerConnection.Controls.Add(labelApiBaseUrl);
            groupBoxServerConnection.Location = new System.Drawing.Point(13, 13);
            groupBoxServerConnection.Name = "groupBoxServerConnection";
            groupBoxServerConnection.Size = new System.Drawing.Size(580, 99);
            groupBoxServerConnection.TabIndex = 0;
            groupBoxServerConnection.TabStop = false;
            groupBoxServerConnection.Text = "Server Connection";
            // 
            // labelServerStatus
            // 
            labelServerStatus.AutoSize = true;
            labelServerStatus.Location = new System.Drawing.Point(6, 75);
            labelServerStatus.Name = "labelServerStatus";
            labelServerStatus.Size = new System.Drawing.Size(40, 13);
            labelServerStatus.TabIndex = 6;
            labelServerStatus.Text = "Status:";
            // 
            // buttonSaveServerConnection
            // 
            this.buttonSaveServerConnection.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSaveServerConnection.Location = new System.Drawing.Point(499, 70);
            this.buttonSaveServerConnection.Name = "buttonSaveServerConnection";
            this.buttonSaveServerConnection.Size = new System.Drawing.Size(75, 23);
            this.buttonSaveServerConnection.TabIndex = 4;
            this.buttonSaveServerConnection.Text = "Save";
            this.buttonSaveServerConnection.UseVisualStyleBackColor = true;
            this.buttonSaveServerConnection.Click += new System.EventHandler(this.buttonSaveServerConnection_Click);
            // 
            // textBoxServerStatus
            // 
            this.textBoxServerStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxServerStatus.Location = new System.Drawing.Point(91, 72);
            this.textBoxServerStatus.Name = "textBoxServerStatus";
            this.textBoxServerStatus.ReadOnly = true;
            this.textBoxServerStatus.Size = new System.Drawing.Size(401, 20);
            this.textBoxServerStatus.TabIndex = 5;
            // 
            // labelApiAccessToken
            // 
            labelApiAccessToken.AutoSize = true;
            labelApiAccessToken.Location = new System.Drawing.Point(6, 49);
            labelApiAccessToken.Name = "labelApiAccessToken";
            labelApiAccessToken.Size = new System.Drawing.Size(75, 13);
            labelApiAccessToken.TabIndex = 3;
            labelApiAccessToken.Text = "Access token:";
            // 
            // labelApiBaseUrl
            // 
            labelApiBaseUrl.AutoSize = true;
            labelApiBaseUrl.Location = new System.Drawing.Point(6, 22);
            labelApiBaseUrl.Name = "labelApiBaseUrl";
            labelApiBaseUrl.Size = new System.Drawing.Size(78, 13);
            labelApiBaseUrl.TabIndex = 0;
            labelApiBaseUrl.Text = "API base URL:";
            // 
            // columnKeyLabel
            // 
            columnKeyLabel.Text = "Shortcut";
            columnKeyLabel.Width = 120;
            // 
            // columnPath
            // 
            columnPath.Text = "Service";
            columnPath.Width = 200;
            // 
            // columnPayload
            // 
            columnPayload.Text = "Payload";
            columnPayload.Width = 500;
            // 
            // notifyIcon
            // 
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "HomeAssistant Shortcuts";
            this.notifyIcon.Visible = true;
            this.notifyIcon.DoubleClick += new System.EventHandler(this.notifyIcon_DoubleClick);
            // 
            // listShortcuts
            // 
            this.listShortcuts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listShortcuts.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            columnKeyLabel,
            columnPath,
            columnPayload});
            this.listShortcuts.FullRowSelect = true;
            this.listShortcuts.HideSelection = false;
            this.listShortcuts.Location = new System.Drawing.Point(13, 118);
            this.listShortcuts.Name = "listShortcuts";
            this.listShortcuts.ShowGroups = false;
            this.listShortcuts.Size = new System.Drawing.Size(580, 309);
            this.listShortcuts.TabIndex = 1;
            this.listShortcuts.UseCompatibleStateImageBehavior = false;
            this.listShortcuts.View = System.Windows.Forms.View.Details;
            this.listShortcuts.SelectedIndexChanged += new System.EventHandler(this.listShortcuts_SelectedIndexChanged);
            // 
            // buttonDeleteShortcuts
            // 
            this.buttonDeleteShortcuts.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonDeleteShortcuts.Enabled = false;
            this.buttonDeleteShortcuts.Location = new System.Drawing.Point(13, 433);
            this.buttonDeleteShortcuts.Name = "buttonDeleteShortcuts";
            this.buttonDeleteShortcuts.Size = new System.Drawing.Size(125, 23);
            this.buttonDeleteShortcuts.TabIndex = 2;
            this.buttonDeleteShortcuts.Text = "Delete selected";
            this.buttonDeleteShortcuts.UseVisualStyleBackColor = true;
            this.buttonDeleteShortcuts.Click += new System.EventHandler(this.buttonDeleteShortcuts_Click);
            // 
            // buttonAddShortcut
            // 
            this.buttonAddShortcut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAddShortcut.Location = new System.Drawing.Point(518, 433);
            this.buttonAddShortcut.Name = "buttonAddShortcut";
            this.buttonAddShortcut.Size = new System.Drawing.Size(75, 23);
            this.buttonAddShortcut.TabIndex = 3;
            this.buttonAddShortcut.Text = "Add";
            this.buttonAddShortcut.UseVisualStyleBackColor = true;
            this.buttonAddShortcut.Click += new System.EventHandler(this.buttonAddShortcut_Click);
            // 
            // textBoxApiAccessToken
            // 
            this.textBoxApiAccessToken.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxApiAccessToken.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::HomeAssistantShortcuts.Properties.Settings.Default, "ApiAccessToken", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBoxApiAccessToken.Location = new System.Drawing.Point(91, 46);
            this.textBoxApiAccessToken.Name = "textBoxApiAccessToken";
            this.textBoxApiAccessToken.Size = new System.Drawing.Size(483, 20);
            this.textBoxApiAccessToken.TabIndex = 2;
            this.textBoxApiAccessToken.Text = global::HomeAssistantShortcuts.Properties.Settings.Default.ApiAccessToken;
            this.textBoxApiAccessToken.UseSystemPasswordChar = true;
            // 
            // textBoxApiBaseUrl
            // 
            this.textBoxApiBaseUrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxApiBaseUrl.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::HomeAssistantShortcuts.Properties.Settings.Default, "ApiBaseUrl", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBoxApiBaseUrl.Location = new System.Drawing.Point(91, 19);
            this.textBoxApiBaseUrl.Name = "textBoxApiBaseUrl";
            this.textBoxApiBaseUrl.Size = new System.Drawing.Size(483, 20);
            this.textBoxApiBaseUrl.TabIndex = 1;
            this.textBoxApiBaseUrl.Text = global::HomeAssistantShortcuts.Properties.Settings.Default.ApiBaseUrl;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(605, 468);
            this.Controls.Add(this.buttonAddShortcut);
            this.Controls.Add(this.buttonDeleteShortcuts);
            this.Controls.Add(this.listShortcuts);
            this.Controls.Add(groupBoxServerConnection);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "HomeAssistant Shortcuts";
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            groupBoxServerConnection.ResumeLayout(false);
            groupBoxServerConnection.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TextBox textBoxApiBaseUrl;
        private System.Windows.Forms.TextBox textBoxApiAccessToken;
        private System.Windows.Forms.Button buttonSaveServerConnection;
        private System.Windows.Forms.TextBox textBoxServerStatus;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ListView listShortcuts;
        private System.Windows.Forms.Button buttonDeleteShortcuts;
        private System.Windows.Forms.Button buttonAddShortcut;
    }
}

