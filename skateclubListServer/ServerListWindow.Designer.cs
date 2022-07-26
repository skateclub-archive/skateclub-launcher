namespace skateclubListServer
{
    partial class ServerListWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ServerListWindow));
            this.serverNameBox = new System.Windows.Forms.TextBox();
            this.descriptionBox = new System.Windows.Forms.TextBox();
            this.listingStatus = new System.Windows.Forms.Label();
            this.listServerButton = new System.Windows.Forms.Button();
            this.closeButton = new System.Windows.Forms.Button();
            this.serverDetailsLabel = new System.Windows.Forms.Label();
            this.passwordBox = new System.Windows.Forms.TextBox();
            this.ipBanList = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ipAllowList = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.hookToServerBox = new System.Windows.Forms.CheckBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // serverNameBox
            // 
            this.serverNameBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.serverNameBox.Location = new System.Drawing.Point(6, 6);
            this.serverNameBox.Name = "serverNameBox";
            this.serverNameBox.PlaceholderText = "Server name...";
            this.serverNameBox.Size = new System.Drawing.Size(340, 23);
            this.serverNameBox.TabIndex = 0;
            this.serverNameBox.Text = "skateclub Server";
            // 
            // descriptionBox
            // 
            this.descriptionBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.descriptionBox.Location = new System.Drawing.Point(6, 35);
            this.descriptionBox.Name = "descriptionBox";
            this.descriptionBox.Size = new System.Drawing.Size(340, 23);
            this.descriptionBox.TabIndex = 1;
            this.descriptionBox.Text = "discord.gg/skateclub";
            // 
            // listingStatus
            // 
            this.listingStatus.AutoSize = true;
            this.listingStatus.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.listingStatus.Location = new System.Drawing.Point(12, 9);
            this.listingStatus.Name = "listingStatus";
            this.listingStatus.Size = new System.Drawing.Size(94, 20);
            this.listingStatus.TabIndex = 3;
            this.listingStatus.Text = "Server listed!";
            // 
            // listServerButton
            // 
            this.listServerButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.listServerButton.Location = new System.Drawing.Point(288, 234);
            this.listServerButton.Name = "listServerButton";
            this.listServerButton.Size = new System.Drawing.Size(84, 23);
            this.listServerButton.TabIndex = 4;
            this.listServerButton.Text = "Start Listing";
            this.listServerButton.UseVisualStyleBackColor = true;
            this.listServerButton.Click += new System.EventHandler(this.listServerButton_Click);
            // 
            // closeButton
            // 
            this.closeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.closeButton.Location = new System.Drawing.Point(226, 234);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(56, 23);
            this.closeButton.TabIndex = 5;
            this.closeButton.Text = "Close";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // serverDetailsLabel
            // 
            this.serverDetailsLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.serverDetailsLabel.BackColor = System.Drawing.SystemColors.Control;
            this.serverDetailsLabel.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.serverDetailsLabel.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.serverDetailsLabel.Location = new System.Drawing.Point(16, 227);
            this.serverDetailsLabel.Name = "serverDetailsLabel";
            this.serverDetailsLabel.Size = new System.Drawing.Size(204, 30);
            this.serverDetailsLabel.TabIndex = 6;
            this.serverDetailsLabel.Text = "No window hooked";
            this.serverDetailsLabel.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // passwordBox
            // 
            this.passwordBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.passwordBox.Location = new System.Drawing.Point(6, 64);
            this.passwordBox.Name = "passwordBox";
            this.passwordBox.PasswordChar = '*';
            this.passwordBox.PlaceholderText = "Password (Leave empty for none)";
            this.passwordBox.Size = new System.Drawing.Size(340, 23);
            this.passwordBox.TabIndex = 7;
            // 
            // ipBanList
            // 
            this.ipBanList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ipBanList.Location = new System.Drawing.Point(6, 21);
            this.ipBanList.Name = "ipBanList";
            this.ipBanList.Size = new System.Drawing.Size(340, 60);
            this.ipBanList.TabIndex = 8;
            this.ipBanList.Text = "";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(185, 15);
            this.label1.TabIndex = 9;
            this.label1.Text = "IP Ban list: (Divide with line break)";
            // 
            // ipAllowList
            // 
            this.ipAllowList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ipAllowList.Location = new System.Drawing.Point(6, 102);
            this.ipAllowList.Name = "ipAllowList";
            this.ipAllowList.Size = new System.Drawing.Size(340, 60);
            this.ipAllowList.TabIndex = 10;
            this.ipAllowList.Text = "";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(195, 15);
            this.label2.TabIndex = 11;
            this.label2.Text = "IP Allow list: (Divide with line break)";
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 32);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(360, 196);
            this.tabControl1.TabIndex = 12;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.hookToServerBox);
            this.tabPage1.Controls.Add(this.passwordBox);
            this.tabPage1.Controls.Add(this.serverNameBox);
            this.tabPage1.Controls.Add(this.descriptionBox);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(352, 168);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Details";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // hookToServerBox
            // 
            this.hookToServerBox.AutoSize = true;
            this.hookToServerBox.Checked = true;
            this.hookToServerBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.hookToServerBox.Location = new System.Drawing.Point(6, 93);
            this.hookToServerBox.Name = "hookToServerBox";
            this.hookToServerBox.Size = new System.Drawing.Size(294, 34);
            this.hookToServerBox.TabIndex = 8;
            this.hookToServerBox.Text = "Hook to Server Window\r\n(Used for server details like player count, level, etc.)";
            this.hookToServerBox.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.ipAllowList);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.ipBanList);
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(352, 168);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Advanced";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // ServerListWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 269);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.serverDetailsLabel);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.listServerButton);
            this.Controls.Add(this.listingStatus);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ServerListWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "skateclub Server List";
            this.Load += new System.EventHandler(this.ServerListWindow_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox serverNameBox;
        private System.Windows.Forms.TextBox descriptionBox;
        private System.Windows.Forms.Label listingStatus;
        private System.Windows.Forms.Button listServerButton;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.Label serverDetailsLabel;
        private System.Windows.Forms.TextBox passwordBox;
        private System.Windows.Forms.RichTextBox ipBanList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox ipAllowList;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.CheckBox hookToServerBox;
    }
}