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
using skateclub;
using skateclub.Properties;
using skateclub.UI;

namespace UI.Views
{
    /// <summary>
    /// Interaction logic for PlayerNameView.xaml
    /// </summary>
    public partial class PlayerNameView : UserControl, IView
    {
        public string ViewName => "playername";

        public ViewManager manager { get; set; }

        public PlayerNameView()
        {
            InitializeComponent();
        }

        void UI_StartSetName()
        {
            Dispatcher.Invoke(() =>
            {
                PlayerNameText.Text = Settings.Default.Playername;
            });
        }

        /// <summary>
        /// Saves changes to player name
        /// </summary>
        void UI_SaveName()
        {
            Settings.Default.Playername = PlayerNameText.Text;
            Settings.Default.Save();

            manager.ShowView("main");

            MainWindow.singleton.SettingButton.Visibility = Visibility.Visible;
        }

        public void Show()
        {
            Dispatcher.Invoke(() =>
            {
                PlayerNameText.Text = Settings.Default.Playername;
            });
        }

        public bool Hide() => true;


        private void SaveName_Click(object sender, RoutedEventArgs e) => UI_SaveName();
    }
}
