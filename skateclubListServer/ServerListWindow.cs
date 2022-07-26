using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using skateclub.Core.Server;
using skateclub.Utility;

namespace skateclubListServer
{
    public partial class ServerListWindow : Form
    {
        public ServerListWindow()
        {
            InitializeComponent();
        }

        public static bool isListing { get; private set; }

        public static int serverUpdateFrequency = 5;

        static ServerListing listing;

        bool listingUpToDate;

        bool windowfound = false;

        Timer UIUpdateClock;
        Timer ListLoopClock;

        private void ServerListWindow_Load(object sender, EventArgs e)
        {
            InitUI();

            FormClosing += ServerListWindow_FormClosing;

            ServerList.OnServerListingUpdate += ServerListUpdate;

            UpdateListingInput();
        }

        private void ServerListWindow_FormClosing(object s, FormClosingEventArgs a)
        {
            if (isListing && MessageBox.Show("Are you sure you want to stop listing your server?", "Are you sure", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                a.Cancel = true;
            else
            {
                StopListing();

                SaveSettings();
            }
        }

        private void listServerButton_Click(object sender, EventArgs e)
        {
            if (!isListing)
                StartListing();
            else
                StopListing();
        }

        private void closeButton_Click(object sender, EventArgs e) => Close();

        void InitUI()
        {
            LoadSettings();

            UIUpdateClock = new Timer();

            UIUpdateClock.Interval = 100;
            UIUpdateClock.Tick += UpdateUILoop;

            UIUpdateClock.Start();

            serverNameBox.TextChanged += (s, a) => UpdateListingInput();
            descriptionBox.TextChanged += (s, a) => UpdateListingInput();
            passwordBox.TextChanged += (s, a) => UpdateListingInput();

            ipBanList.TextChanged += (s, a) => UpdateListingInput();
            ipAllowList.TextChanged += (s, a) => UpdateListingInput();

            UpdateListingInput();

            UpdateUI();
        }

        void UpdateUI()
        {
            listingStatus.Text = isListing ? "Listing server..." : "Server isn't listed";
            listingStatus.ForeColor = Color.Black;

            listServerButton.Text = isListing ? "Stop listing" : "Start listing";
        }

        void UpdateUILoop(object sender, EventArgs args)
        {
            if (hookToServerBox.Checked)
            {
                GetServerDetailsFromWindow();
            }

            serverDetailsLabel.Text = $"{(isListing ? (listingUpToDate ? "Listing up-to-date" : "Updating listing...") : "")}\n" +
                (hookToServerBox.Checked ? (windowfound ? $"Players: {listing.playerCount}/{listing.playerCapacity}, Level: {listing.level}" : "No server window found") : "Hook disabled");
        }

        void ServerListUpdate(bool success, string message)
        {
            Action action = new Action(() =>
            {
                if (!success) // Temporary fix, for some reason the server is getting registered twice and I can't figure out why
                {
                    listingStatus.Text = $"Failed to list - {message}";
                    listingStatus.ForeColor = Color.Red;

                    SystemSounds.Beep.Play();

                    StopListing();

                    if (MessageBox.Show($"Failed to list\n\n{message}", "Failed to list", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == DialogResult.Retry)
                        StartListing();
                }
                else
                {
                    listingStatus.Text = "Server is listed";
                    listingStatus.ForeColor = Color.Black;
                }

                listServerButton.Enabled = true;
            });

            if (InvokeRequired)
                BeginInvoke(action);
            else
                action.Invoke();
        }

        void GetServerDetailsFromWindow()
        {
            var data = WindowHook.GetDataFromWindow("Skate. Server", new string[]
            {
                "PlayerCount",
                "Level"
            });

            windowfound = data != null;

            if(windowfound) 
            {
                foreach (var key in data.Keys)
                {
                    switch (key)
                    {
                        case "PlayerCount":

                            string[] split = data[key].Split(" ");

                            listing.playerCount = int.Parse(Utility.SplitString(split[0], new string[] { "/" })[0]);
                            listing.playerCapacity = int.Parse(split[2].Replace("[", "").Replace("]", ""));

                            break;

                        case "Level":

                            listing.level = data[key];
                            
                            break;
                    }
                }
            }
        }

        void LoadSettings()
        {
            serverNameBox.Text = Properties.Settings.Default.ServerName;
            descriptionBox.Text = Properties.Settings.Default.ServerDesc;
            passwordBox.Text = Properties.Settings.Default.ServerPassword;

            try
            {
                ipBanList.Lines = Properties.Settings.Default.ipBanList.Cast<string>().ToArray();
                ipAllowList.Lines = Properties.Settings.Default.ipAllowList.Cast<string>().ToArray();
            }
            catch { }
        }

        void SaveSettings()
        {
            Properties.Settings.Default.ServerName = serverNameBox.Text;
            Properties.Settings.Default.ServerDesc = descriptionBox.Text;
            Properties.Settings.Default.ServerPassword = passwordBox.Text;

            Properties.Settings.Default.ipBanList = new System.Collections.Specialized.StringCollection();
            Properties.Settings.Default.ipBanList.AddRange(ipBanList.Lines);

            Properties.Settings.Default.ipAllowList = new System.Collections.Specialized.StringCollection();
            Properties.Settings.Default.ipAllowList.AddRange(ipAllowList.Lines);

            Properties.Settings.Default.Save();
        }

        public void StartListing()
        {
            if (string.IsNullOrEmpty(serverNameBox.Text))
            {
                MessageBox.Show("Server name can't be empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            isListing = true;

            listServerButton.Enabled = false;

            UpdateUI();

            List();
            StartListLoop();
        }

        void StartListLoop()
        {
            if(ListLoopClock == null || !ListLoopClock.Enabled)
            {
                ListLoopClock = new Timer();

                ListLoopClock.Interval = serverUpdateFrequency * 1000;
                ListLoopClock.Tick += (s, a) =>
                {
                    if (isListing)
                    {
                        List();
                    }
                    else
                    {
                        ListLoopClock.Stop();
                    }
                };

                ListLoopClock.Start();
            }
        }

        void List()
        {
            UpdateListingInput();

            listingUpToDate = true;

            ServerList.RegisterServerListing(listing);
        }

        void UpdateListingInput()
        {
            listing.name = serverNameBox.Text;
            listing.description = descriptionBox.Text;
            listing.password = passwordBox.Text;

            listing.banlist = ipBanList.Lines;
            listing.allowlist = ipAllowList.Lines;

            listingUpToDate = false;
        }

        public void StopListing()
        {
            if (isListing)
            {
                isListing = false;

                ServerList.RemoveServerListing();

                UpdateUI();
            }
        }
    }
}
