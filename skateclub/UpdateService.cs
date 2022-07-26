using AutoUpdaterDotNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace skateclub
{
    public static class UpdateService
    {
        public static string UpdateURL = "REMOVED";
        public static Version ClientVersion = new Version(1, 5, 2, 0);

        public static async void Start()
        {
            if(await skateclub.Utility.Utility.CheckForConnection(url: UpdateURL))
            { 
                // Hard coded because VS sucks
                AutoUpdater.InstalledVersion = ClientVersion;

                AutoUpdater.ShowSkipButton = false;
                AutoUpdater.ShowRemindLaterButton = false;
                AutoUpdater.Mandatory = true;
                AutoUpdater.AppTitle = "skateclub";
                AutoUpdater.Start(UpdateURL);
            } 
            else
            {
                MessageBox.Show($"Update server unavailable", "Update error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
