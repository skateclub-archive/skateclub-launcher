using DiscordRPC;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace skateclub
{
    public static class DiscordPresence
    {
        static DiscordRpcClient client;

        public static void Init()
        {
            client = new DiscordRpcClient("REMOVED");
            client.Initialize();

            client.SetPresence(new RichPresence()
            {
                Details = "In the launcher",
                State = "https://skateclub.xyz/",
                Assets = new Assets()
                {
                    LargeImageKey = new Random().Next(0, 1000) == 500 ? "susclub" : "skateclub"
                },
                Buttons = new Button[]
                {
                    new Button() { Label = "Join Discord", Url = "https://discord.gg/skateclub/",},
                    new Button() { Label = "Visit Website", Url = "https://skateclub.xyz",},
                }
            });

            Application.Current.Exit += (s, a) => client.Deinitialize();
        }
    }
}
