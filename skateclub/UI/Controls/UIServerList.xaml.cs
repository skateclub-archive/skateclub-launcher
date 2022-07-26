using skateclub.Core;
using skateclub.Core.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace skateclub.UI
{
    /// <summary>
    /// Interaction logic for ServerList.xaml
    /// </summary>
    public partial class UIServerList : UserControl
    {
        ServerInfo[] servers;

        string[] OfficalIps = { "45.76.235.243", "144.202.75.13", "54.39.130.229", "skate.axxonte.xyz" };

        ServerInfo[] forcedServers = new ServerInfo[]
        {
            new ServerInfo()
            {
                name = "[SKATECLUB OFFICIAL] North America #1",
       //         ip = "45.76.235.243",
                official = true
            },
            new ServerInfo()
            {
                name = "[SKATECLUB OFFICIAL] North America #2",
            //    ip = "144.202.75.13",
                official = true
            },
            new ServerInfo()
            {
                name = "[SKATECLUB OFFICIAL] North America #3",
           //     ip = "54.39.130.229",
                official = true
            },
            new ServerInfo()
            {
                name = "[SKATECLUB OFFICIAL] Europe #1",
           //     ip = "skate.axxonte.xyz",
                official = true
            }
        };

        List<UIServerListing> uiListings = new List<UIServerListing>();

        Rectangle separator;

        bool showForcedServers = false;

        Timer refreshTimer;

        public UIServerList()
        {
            InitializeComponent();

            Loaded += (sender, args) =>
            {
                serverSearch.TextChanged += (s, a) => UpdateServers();
                sortTypeBox.SelectionChanged += (s, a) => UpdateServers();

                sortTypeBox.Items.Add("Sort by: Name ↑");
                sortTypeBox.Items.Add("Sort by: Name ↓");
                sortTypeBox.Items.Add("Sort by: Player Count ↑");
                sortTypeBox.Items.Add("Sort by: Player Count ↓");

                sortTypeBox.SelectedIndex = 0;
            };
        }

        void UpdateServers()
        {
            items.Items.Remove(separator);

            //foreach (var listing in forcedListings)
            //{
            //    listing.UpdateUIVisibility(serverSearch.Text, items.Items);
            //}

            //items.Items.Add(separator);

            foreach (var listing in uiListings)
            {
                listing.UpdateUIVisibility(serverSearch.Text, items.Items);
            }

            var sorted = Sort(items.Items.Cast<UIServerListing>()).ToList();

            items.Items.Clear();
            for (int i = 0; i < sorted.Count; i++)
            {
                items.Items.Add(sorted[i]);
            }
        }

        IEnumerable<UIServerListing> Sort(IEnumerable<UIServerListing> list)
        {
            var result = list;
            switch(sortTypeBox.SelectedIndex)
            {
                case 0: result = list.OrderBy(x => x.info.name); break;
                case 1: result = list.OrderByDescending(x => x.info.name); break;
                case 2: result = list.OrderBy(x => x.info.players); break;
                case 3: result = list.OrderByDescending(x => x.info.players); break;
            }

            return result;//.OrderByDescending(x => x.Listing.official);
        }

        public void UIRefresh()
        {
            if (DesignerProperties.GetIsInDesignMode(this))
                return;

            statusLabel.Content = "Loading...";

            Refresh();
        }

        async void Refresh()
        {
            servers = await ServerList.GetList();

            UpdateUI();

            //if(ServerList.updateFrequency > 0)
            //{
            //    Task.Run(async () =>
            //    {
            //        await Task.Delay(ServerList.updateFrequency * 1000);

            //        Refresh();
            //    });
            //}
        }

        void SetOfficial(ServerInfo list)
        {
            list.official = true;
        }

        void UpdateUI()
        {
            items.Items.Clear();

            uiListings.Clear();

            statusLabel.Content = servers == null ? "Server list unavailable" : servers.Length == 0 ? "No servers found" : "";

            if (showForcedServers)
            {
                foreach (var server in forcedServers)
                {
                    AddServerListingUI(server, uiListings);
                }
            }

            if (servers != null && servers.Length > 0)
            {
                foreach (var server in servers)
                {
                    AddServerListingUI(server, uiListings);
                }
            }

            ////Set Offical servers to the top first
            //if (servers != null)
            //{
            //    foreach (var listing in servers)
            //    {
            //        if (OfficalIps.Any(Encryption.Decrypt(listing.ip, ServerList.EncryptionKey).Contains))
            //        {
            //            AddServerListingUI(listing, i, uiListings);
            //        }
            //        i++;
            //    }
            //}

            //if (servers != null)
            //{
            //    foreach (var listing in servers)
            //    {
            //        if (!OfficalIps.Contains(Encryption.Decrypt(listing.ip, ServerList.EncryptionKey)))
            //            AddServerListingUI(listing, i, uiListings);
            //        i++;
            //    }
            //}

            UpdateServers();
        }

        void AddServerListingUI(ServerInfo listing, List<UIServerListing> list)
        {
            string search = serverSearch.Text;

            var uiListing = new UIServerListing(this, listing);

            uiListing.Height = 40;

            list.Add(uiListing);
            items.Items.Add(uiListing);

            uiListing.UpdateUIVisibility(search, items.Items);
        }
    }
}
