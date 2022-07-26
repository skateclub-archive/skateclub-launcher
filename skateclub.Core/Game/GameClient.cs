using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skateclub.Core.Game
{
    public struct GameInstance
    {
        public Process process;
        public bool server;
    }
    public struct GameSettings
    {
        public string PlayerName;

        public bool DX11;
        public bool ShowFPS;
        public bool HideWatermark;
        public bool EnableCosmetics;
        public bool RemoveClothing;
        public bool DebugRender;
        public bool ExperimentalMode;
        public double TruckTightness;
        public bool FullScreen;
        public bool SkipCutscene;
        public string AmbientOcclusion;
        public string AntiAliasing;
        public string ShaderQuality;

        public override string ToString() => GenerateParams();

        public string GenerateParams() // ADD A SPACE AFTER EACH ARGUMENT!!!!!!!!!
        {
            var argsBuilder = new StringBuilder();

            argsBuilder.Append(@"-DelMar.LocalPlayerDebugName " + PlayerName + " -DelMarOnline.Enable false -Online.ClientIsPresenceEnabled false -FBCorePhysics.TruckTightness " + TruckTightness + " ");

            if (DX11)
                argsBuilder.Append("-Render.ForceDx11 true ");

            if (ShowFPS)
                argsBuilder.Append("-DebugRender true ");

            if (HideWatermark)
                argsBuilder.Append("-DelMarUI.EnableWatermark false ");

            //Thanks skate.online
            if (EnableCosmetics)
                argsBuilder.Append("-ItemManager.ForceOwnAll true -Architect.ShowAllCategories true ");

            if (RemoveClothing)
                argsBuilder.Append("-ClothSystem.ClientClothWorldThreadCount 0 ");

            if (DebugRender)
                argsBuilder.Append("-DebugRender true ");

            if (ExperimentalMode)
                argsBuilder.Append("-DelMarExperimentalSettingsEnabled true -DelMarEnableExperimentalFeatures true ");

            if (FullScreen)
                argsBuilder.Append("-ProfileOptions.ForceDefaultFullscreenEnabled true  ");

            if (SkipCutscene)
                argsBuilder.Append("-DelMarGame.AllowBootPrompts false ");

            argsBuilder.Append("-PostProcess.DynamicAOMethod " + AmbientOcclusion + " ");
            argsBuilder.Append("-WorldRender.PostProcessAntiAliasingMode " + AntiAliasing + " ");
            argsBuilder.Append("-ShaderSystem.ShaderQualityLevel " + ShaderQuality + " ");

            return argsBuilder.ToString();
        }
    }
    public static class GameClient
    {
        public static string GameExecutablePath = AppDomain.CurrentDomain.BaseDirectory + @"\Skate.crack.exe";

        public static List<GameInstance> gameInstances = new List<GameInstance>();

        public static GameInstance[] clientInstances => gameInstances.FindAll(x => !x.server).ToArray();
        public static GameInstance[] serverInstances => gameInstances.FindAll(x => x.server).ToArray();

        private static async Task LaunchGameProcess(params string[] args) => await LaunchGameProcess(string.Join(" ", args));
        private static async Task LaunchGameProcess(string args)
        {
            ProcessStartInfo processInfo = new ProcessStartInfo();
            processInfo.FileName = GameExecutablePath;
            processInfo.Arguments = args;

            bool server = args.Contains("-Server");

            var instance = new GameInstance()
            {
                process = Process.Start(processInfo),
                server = server
            };

            gameInstances.Add(instance);

            await Task.Run(() =>
            {
                instance.process.WaitForExit();

                gameInstances.Remove(instance);
            });
        }

        public static async Task PlaySolo(GameSettings settings) => await LaunchGameProcess(settings.GenerateParams());

        public static async Task PlayOnline(string IP, GameSettings settings)
        {
            settings.DebugRender = false;
            await LaunchGameProcess(settings.GenerateParams(), "-Client.ServerIp " + IP);
        }
        public static async Task StartServer() => await LaunchGameProcess($"-Server");//-Online.ClientIsPresenceEnabled false -server.loadRootLevel levels/game/{level}/{level} -Server");

        public static void Game_ResetCFG()
        {
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "/Scripts/Win32Game.cfg"))
            {
                File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "/Scripts/Win32Game.cfg", "-super layout.toc");
            }
        }
    }
}
