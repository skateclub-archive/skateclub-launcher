using System;
using System.Collections.Generic;
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
using skateclub.Core.Game;
using skateclub.UI;

namespace UI.Views
{
    /// <summary>
    /// Interaction logic for InstallView.xaml
    /// </summary>
    public partial class InstallView : UserControl, IView
    {
        public ViewManager manager { get; set; }

        public string ViewName => "install";

        public InstallView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Begins the download process for the game
        /// </summary>
        void UI_Download()
        {
            InstallBtn.Content = "Downloading...";
            InstallBtn.IsEnabled = false;

            PBar.Visibility = Visibility.Visible;
            PLabel.Visibility = Visibility.Visible;

            try
            {
                GameDownloader.OnProgressChanged += Downloader_DownloadProgressChanged;
                GameDownloader.OnExtract += Downloader_OnExtract;
                GameDownloader.OnExtractProgressChanged += Downloader_OnExtractProgress;
                GameDownloader.OnDownloadCompleted += Downloader_Completed;
                GameDownloader.OnDependencyRunInstaller += Downloader_DependencyInstaller;

                GameDownloader.Download();
            }
            catch (Exception e)
            {
                MessageBox.Show($"Failed to download\n\n{e}", "skateclub Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown(-1);
            }
        }

        /// <summary>
        /// Called when download progress changes
        /// </summary>
        /// <param name="received"></param>
        /// <param name="total"></param>
        void Downloader_DownloadProgressChanged(long received, long total)
        {
            Dispatcher.Invoke(() =>
            {
                double bytesIn = double.Parse(received.ToString());
                double totalBytes = double.Parse(total.ToString());
                double percentage = bytesIn / totalBytes * 100;
                PLabel.Content = "Downloading: " + skateclub.Utility.Utility.ConvertBytesToMegabytes(received).ToString("0.00") + "MB of " + skateclub.Utility.Utility.ConvertBytesToMegabytes(total).ToString("0.00") + "MB";
                PBar.Value = int.Parse(Math.Truncate(percentage).ToString());
            });
        }

        /// <summary>
        /// Called when extracting begins
        /// </summary>
        void Downloader_OnExtract()
        {
            Dispatcher.Invoke(() =>
            {
                InstallBtn.Content = "Extracting...";
            });
        }

        /// <summary>
        /// Called when extraction progress changes
        /// </summary>
        /// <param name="entry"></param>
        /// <param name="progress"></param>
        /// <param name="total"></param>
        void Downloader_OnExtractProgress(string entry, int progress, int total)
        {
            Dispatcher.Invoke(() =>
            {
                PBar.Maximum = total;

                PLabel.Content = "Extracting: " + entry;
                PBar.Value = progress;
            });
        }

        /// <summary>
        /// Called when a dependency installer runs and needs to be installed by the user
        /// </summary>
        void Downloader_DependencyInstaller()
        {
            Dispatcher.Invoke(() =>
            {
                InstallBtn.Content = "Waiting...";
                PLabel.Content = "Please install the Microsoft Visual C++ Redistributables";
            });
        }

        /// <summary>
        /// Called when the download sequence is complete
        /// </summary>
        void Downloader_Completed()
        {
            manager.ShowView("playername");
        }

        private void InstallBtn_Click(object sender, RoutedEventArgs e) => UI_Download();

        public void Show()
        {

        }

        public bool Hide()
        {
            return !GameDownloader.isDownloading ? 
                true : 
                MessageBox.Show("Are you sure you want to cancel the installation?", "Installation running", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes;
        }
    }
}
