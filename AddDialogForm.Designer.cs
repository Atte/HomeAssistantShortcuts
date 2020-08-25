namespace HomeAssistantShortcuts
{
    partial class AddDialogForm
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
            System.Windows.Forms.Label labelShortcut;
            System.Windows.Forms.Label labelPaths;
            System.Windows.Forms.Button buttonCancel;
            System.Windows.Forms.Label labelPayload;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddDialogForm));
            this.buttonAdd = new System.Windows.Forms.Button();
            this.textBoxShortcut = new System.Windows.Forms.TextBox();
            this.comboBoxPaths = new System.Windows.Forms.ComboBox();
            this.textBoxPayload = new System.Windows.Forms.TextBox();
            labelShortcut = new System.Windows.Forms.Label();
            labelPaths = new System.Windows.Forms.Label();
            buttonCancel = new System.Windows.Forms.Button();
            labelPayload = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelShortcut
            // 
            labelShortcut.AutoSize = true;
            labelShortcut.Location = new System.Drawing.Point(12, 15);
            labelShortcut.Name = "labelShortcut";
            labelShortcut.Size = new System.Drawing.Size(50, 13);
            labelShortcut.TabIndex = 1;
            labelShortcut.Text = "Shortcut:";
            // 
            // labelPaths
            // 
            labelPaths.AutoSize = true;
            labelPaths.Location = new System.Drawing.Point(12, 42);
            labelPaths.Name = "labelPaths";
            labelPaths.Size = new System.Drawing.Size(46, 13);
            labelPaths.TabIndex = 3;
            labelPaths.Text = "Service:";
            // 
            // buttonCancel
            // 
            buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            buttonCancel.Location = new System.Drawing.Point(222, 409);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new System.Drawing.Size(75, 23);
            buttonCancel.TabIndex = 6;
            buttonCancel.Text = "Cancel";
            buttonCancel.UseVisualStyleBackColor = true;
            buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // labelPayload
            // 
            labelPayload.AutoSize = true;
            labelPayload.Location = new System.Drawing.Point(12, 69);
            labelPayload.Name = "labelPayload";
            labelPayload.Size = new System.Drawing.Size(48, 13);
            labelPayload.TabIndex = 7;
            labelPayload.Text = "Payload:";
            // 
            // buttonAdd
            // 
            this.buttonAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAdd.Location = new System.Drawing.Point(303, 409);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(75, 23);
            this.buttonAdd.TabIndex = 5;
            this.buttonAdd.Text = "OK";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // textBoxShortcut
            // 
            this.textBoxShortcut.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxShortcut.Location = new System.Drawing.Point(78, 12);
            this.textBoxShortcut.Name = "textBoxShortcut";
            this.textBoxShortcut.ShortcutsEnabled = false;
            this.textBoxShortcut.Size = new System.Drawing.Size(300, 20);
            this.textBoxShortcut.TabIndex = 0;
            this.textBoxShortcut.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxShortcut_KeyDown);
            // 
            // comboBoxPaths
            // 
            this.comboBoxPaths.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxPaths.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBoxPaths.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxPaths.DisplayMember = "Path";
            this.comboBoxPaths.Enabled = false;
            this.comboBoxPaths.FormattingEnabled = true;
            this.comboBoxPaths.Location = new System.Drawing.Point(78, 39);
            this.comboBoxPaths.Name = "comboBoxPaths";
            this.comboBoxPaths.Size = new System.Drawing.Size(300, 21);
            this.comboBoxPaths.TabIndex = 2;
            this.comboBoxPaths.ValueMember = "Path";
            this.comboBoxPaths.SelectedIndexChanged += new System.EventHandler(this.comboBoxPaths_SelectedIndexChanged);
            // 
            // textBoxPayload
            // 
            this.textBoxPayload.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxPayload.Location = new System.Drawing.Point(15, 85);
            this.textBoxPayload.Multiline = true;
            this.textBoxPayload.Name = "textBoxPayload";
            this.textBoxPayload.Size = new System.Drawing.Size(363, 318);
            this.textBoxPayload.TabIndex = 4;
            this.textBoxPayload.TextChanged += new System.EventHandler(this.textBoxPayload_TextChanged);
            // 
            // AddDialogForm
            // 
            this.AcceptButton = this.buttonAdd;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = buttonCancel;
            this.ClientSize = new System.Drawing.Size(390, 444);
            this.Controls.Add(labelPayload);
            this.Controls.Add(buttonCancel);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.textBoxPayload);
            this.Controls.Add(labelPaths);
            this.Controls.Add(this.comboBoxPaths);
            this.Controls.Add(labelShortcut);
            this.Controls.Add(this.textBoxShortcut);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddDialogForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add Shortcut";
            this.Shown += new System.EventHandler(this.AddDialogForm_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxShortcut;
        private System.Windows.Forms.ComboBox comboBoxPaths;
        private System.Windows.Forms.TextBox textBoxPayload;
        private System.Windows.Forms.Button buttonAdd;
    }
}