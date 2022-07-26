using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using skateclub;
using skateclub.UI;

namespace UI.Views
{
    /// <summary>
    /// Interaction logic for ServersView.xaml
    /// </summary>
    public partial class ServersView : UserControl, IView
    {
        public string ViewName => "servers";

        public ServersView()
        {
            InitializeComponent();
        }

        public ViewManager manager { get; set; }

        public bool Hide() => true;

        public void Show()
        {
            UI_LoadServerList();
        }

        /// <summary>
        /// Loads the server list
        /// </summary>
        void UI_LoadServerList()
        {
            ServerListView.UIRefresh();
        }

        void UI_ShowListServer()
        {
            Process.Start("skateclubListServer.exe");
        }

        private void ConnectButton_Click(object sender, RoutedEventArgs e) => MainWindow.Client_PlayOnline(IPText.Text);

        private async void HostServerButton_Click(object sender, RoutedEventArgs e)
        {
            if (await MainWindow.Client_StartServer())
            {
                /*if (serverBrowserCheckbox.IsChecked.Value && MessageBox.Show("Would you like to list this server on our server browser?", "Launching server", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    UI_ShowListServer();*/

                if (launchClientCheckbox.IsChecked.Value)
                    MainWindow.Client_PlayOnline("127.0.0.1");
            }
        }

        private void ListServerButton_Click(object sender, RoutedEventArgs e) => UI_ShowListServer();

        private void Refresh_Click(object sender, RoutedEventArgs e) => UI_LoadServerList();

        private void Back_Click(object sender, RoutedEventArgs e) => manager.ShowView("main");
    }
}
