using SharpCompress.Archives;
using SharpCompress.Archives.Rar;
using SharpCompress.Common;
using skateclub.Properties;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using skateclub.Core;
using AutoUpdaterDotNET;
using skateclub.Core.Game;
using skateclub.Core.Server;
using skateclub.UI;
using skateclub.Utility;
using UI.Views;
using System.Windows.Threading;

namespace skateclub
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ViewManager Views; // Contains all the menus in the app

        public static MainWindow singleton;

        public readonly DispatcherTimer UIClock = new DispatcherTimer();

        public MainWindow()
        {
            Application.Current.Exit += (s, a) => GameDownloader.CleanTempFiles();

            singleton = this;

            UIClock.Interval = TimeSpan.FromMilliseconds(100);
            UIClock.Start();

            InitializeComponent();
        }

        /// <summary>
        /// Generates settings for the game client
        /// </summary>
        /// <returns></returns>
        static GameSettings GenerateGameSettings() => new GameSettings()
        {
            PlayerName = Settings.Default.Playername,

            DX11 = Settings.Default.DX11,
            ShowFPS = Settings.Default.ShowFPS,
            EnableCosmetics = Settings.Default.EnableCosmetic,
            HideWatermark = Settings.Default.HideWatermark,
            DebugRender = Settings.Default.DebugRender,
            RemoveClothing = Settings.Default.RemoveClothes,
            ExperimentalMode = Settings.Default.ExperimentalMode,
            TruckTightness = Settings.Default.TruckTightness,
            FullScreen = Settings.Default.Fullscreen,
            SkipCutscene = Settings.Default.SkipCutscene,
            AmbientOcclusion = Settings.Default.AO,
            AntiAliasing = Settings.Default.AA,
            ShaderQuality = Settings.Default.ShaderQuality
            
        };

        /// <summary>
        /// Initializes the app
        /// </summary>
        private void App_Init()
        {
            /*ServerListView.IsVisibleChanged += (s, a) => UI_LoadServerList();

            serverBrowserCheckbox.Click += (s, a) =>
            {
                ListServerButton.IsEnabled = serverBrowserCheckbox.IsChecked.Value;
                UI_LoadServerList();
            };*/

            Views = new ViewManager(mainGrid,
                new MainView(),
                new InstallView(),
                new PlayerNameView(),
                new ServersView(),
                new SettingsView());

            UpdateService.Start();
            DiscordPresence.Init();

            if (GameDownloader.gameInstalled)
            {
                Views.ShowView("main");

                SettingButton.Visibility = Visibility.Visible;
            }
            else
            {
                Views.ShowView("install");

                SettingButton.Visibility = Visibility.Hidden;

                MessageBox.Show("Welcome to skateclub!" +
                    "\n\nThe client did not find the Skate 4 executable. If you have the game installed, please put the skateclub client in the same folder as your game installation." +
                    "\nIf you'd like to download the game, simply press \"Install\"\n\nHave fun!", "Welcome to skateclub", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        /// <summary>
        /// Closes the app. If an installation is running it will ask the user to confirm closing
        /// </summary>
        void UI_Close()
        {
            if (!GameDownloader.isDownloading || MessageBox.Show("Are you sure you want to cancel the installation?", "Installation running", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                Close();
        }

        /// <summary>
        /// Shows player name menu
        /// </summary>
        void UI_StartSetName()
        {
            Views.ShowView("playername");
        }

        /// <summary>
        /// Shows the settings menu
        /// </summary>
        void UI_ShowSettings()
        {
            Views.ShowView("settings");
        }

        static bool Client_CheckForRunningClients()
        {
            if (GameClient.clientInstances.Length > 0)
            {
                var dialog = MessageBox.Show("An instance of the game is already running, would like to close the running instance?", "Game running", MessageBoxButton.YesNoCancel, MessageBoxImage.Information);

                if (dialog == MessageBoxResult.Cancel)
                    return false;
                else if (dialog == MessageBoxResult.Yes)
                {
                    foreach (var instance in GameClient.clientInstances)
                    {
                        instance.process.Kill();
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Starts a solo session of skate 4
        /// </summary>
        public static async void Client_PlaySolo()
        {
            try
            {
                if (Client_CheckForRunningClients())
                {
                    GameClient.Game_ResetCFG();

                    await GameClient.PlaySolo(GenerateGameSettings());
                }
            }
            catch (Exception e)
            {
                MessageBox.Show($"Failed to launch game\n{e.Message}", "Failed to launch game", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Starts an online session of skate 4
        /// </summary>
        /// <param name="ip"></param>
        public static async void Client_PlayOnline(string ip)
        {
            try
            {
                //       MessageBox.Show(GenerateGameSettings().GenerateParams());
                if (Client_CheckForRunningClients())
                {
                    GameClient.Game_ResetCFG();

                    await GameClient.PlayOnline(ip, GenerateGameSettings());
                }
            } 
            catch(Exception e)
            {
                MessageBox.Show($"Failed to launch game\n{e.Message}", "Failed to launch game", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Starts a skate 4 server
        /// </summary>
        public static async Task<bool> Client_StartServer()
        {
            try
            {
              //  MessageBox.Show(singleton.levelPicker.SelectedValue as string);
                await GameClient.StartServer();
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show($"Failed to launch game\n{e.Message}", "Failed to launch game", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        #region UI Callbacks
        private void Window_Loaded(object sender, RoutedEventArgs e) => App_Init();

        private void DragBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) => DragMove();

        private void Button_Click_1(object sender, RoutedEventArgs e) => UI_Close();

        private void Minimize_Click(object sender, RoutedEventArgs e) => WindowState = WindowState.Minimized;

        private void SettingButton_Click(object sender, RoutedEventArgs e) => UI_ShowSettings();
        #endregion
    }
}
