using skateclub;
using skateclub.UI;
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

namespace UI.Views
{
    /// <summary>
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class MainView : UserControl, IView
    {
        public string ViewName => "main";

        public MainView()
        {
            InitializeComponent();
        }

        public ViewManager manager { get; set; }

        public bool Hide() => true;

        public void Show()
        {

        }


        private void Button_Click_2(object sender, RoutedEventArgs e) => manager.ShowView("servers");

        private void Button_Click(object sender, RoutedEventArgs e) => MainWindow.Client_PlaySolo();

        private void Discord_Click(object sender, RoutedEventArgs e) => Process.Start("http://discord.gg/skateclub");
    }
}
