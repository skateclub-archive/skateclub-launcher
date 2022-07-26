using skateclub.Core.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace UI
{
    public partial class ReportServer : Form
    {
        ServerInfo server;

        public ReportServer(ServerInfo server)
        {
            this.server = server;

            InitializeComponent();
        }

        private void ReportServer_Load(object sender, EventArgs e)
        {
            Text = $"Reporting \"{server.name}\"";
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private async void reportButton_Click(object sender, EventArgs e)
        {
            reportButton.Enabled = false;

            var response = await ServerList.ReportServer(server.id, discordBox.Text, reportReason.Text);

            if (response != null && response.IsSuccessStatusCode)
                MessageBox.Show("Server reported. Thank you for helping the skateclub community!", "Thank you", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show($"Failed to send report.\n\n{(response != null ? await response.Content.ReadAsStringAsync() : "")}", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);

            Close();
        }
    }
}
