using skateclub.Core.Game;
using skateclub.Core.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using skateclub.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UI;
using skateclub.Utility;

namespace skateclub.UI
{
    /// <summary>
    /// Interaction logic for ServerListing.xaml
    /// </summary>
    public partial class UIServerListing : UserControl
    {
        public ServerInfo info;

        UIServerList list;

        public UIServerListing(UIServerList list, ServerInfo serverListing)
        {
            this.info = serverListing;
            this.list = list;

            InitializeComponent();
        }

        public void UpdateUIVisibility(string search, ItemCollection items)
        {
            if (search == ""
                || info.name.ToLower().Contains(search.ToLower())
                || info.country.ToLower().Contains(search.ToLower()))
            {
                if (!items.Contains(this))
                {
                    items.Add(this);
                }

                Visibility = Visibility.Visible;
            }
            else
            {
                if (items.Contains(this))
                    items.Remove(this);

                Visibility = Visibility.Hidden;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            serverName.Content = info.name;

            serverDetails.Content = string.IsNullOrEmpty(info.description) ? "No description" : info.description;

            playerCount.Content = info.players + "/" + info.capacity + $"\nPassword: {info.hasPassword}";

            if (Uri.TryCreate(info.flag, UriKind.Absolute, out var uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps))
            {
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(info.flag, UriKind.Absolute);
                bitmap.EndInit();

                countryflag.Source = bitmap;
            }
        }

        public async void JoinServer()
        {
            list.UIRefresh();

            if(info.hasPassword)
            {
                new JoinServer(async (password) =>
                {
                    return await ConnectToServer(password);
                }).Show();
            } else
            {
                await ConnectToServer("");
            }
        }

        async Task<bool> ConnectToServer(string password)
        {
            var connect = await ServerList.ConnectToServer(info.id, password);

            if (connect != null && connect.IsSuccessStatusCode)
            {
                string ip = await connect.Content.ReadAsStringAsync();

                var decrypt = Encryption.Decrypt(ip, ServerList.EncryptionKey);

                if (decrypt != null)
                    ip = decrypt;

                MainWindow.Client_PlayOnline($"{ip}");
                return true;
            }
            else
            {
                MessageBox.Show($"Failed to join server\n{(connect != null ? await connect.Content.ReadAsStringAsync() : "")}", "Failed to join", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        public void ReportServer()
        {
            new ReportServer(info).Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e) => JoinServer();

        private void reportButton_Click(object sender, RoutedEventArgs e) => ReportServer();
    }
}
