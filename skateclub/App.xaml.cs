using skateclub.Core.Server;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace skateclub
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        void App_Startup(object sender, StartupEventArgs e)
        {
            for (int i = 0; i != e.Args.Length; ++i) // These are kinda fucky 
            {
                switch (e.Args[i])
                {
                    case "-updateserver":
                        if (e.Args.Length > i)
                            UpdateService.UpdateURL = e.Args[i];
                        break;
                    case "-serverlist":
                        if (e.Args.Length > i)
                            ServerList.ServerListAddress = e.Args[i];
                        break;
                    case "-overrideversion":
                        if (e.Args.Length > i)
                            UpdateService.ClientVersion = new Version(e.Args[i]);
                        break;
                }
            }

            var window = new MainWindow();

            window.Show();
        }
    }
}
