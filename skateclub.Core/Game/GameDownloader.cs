using SharpCompress.Archives;
using SharpCompress.Archives.Rar;
using SharpCompress.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace skateclub.Core.Game
{
    public static class GameDownloader
    {
        public static bool gameInstalled => File.Exists(GameClient.GameExecutablePath);

        public static bool isDownloading { get; private set; }

        public static string DownloadURL = "https://cdn4.bunkr.is/Skate.-Crack-+Fix-&-MP-4GHwtYkU.rar";
        public static string ContainerPath = AppDomain.CurrentDomain.BaseDirectory + "/skateclub.rar";

        static Dictionary<string, string> dependencies = new Dictionary<string, string>()
        {
            { "https://aka.ms/vs/17/release/vc_redist.x64.exe", AppDomain.CurrentDomain.BaseDirectory + "/deps/vc_redist.x64.exe" },
            { "https://aka.ms/vs/17/release/vc_redist.x86.exe", AppDomain.CurrentDomain.BaseDirectory + "/deps/vc_redist.x86.exe" }
        };

        public delegate void ProgressChanged(long received, long total);
        public delegate void DependencyDownload();
        public delegate void DependencyRunInstaller();
        public delegate void Extract();
        public delegate void ExtractProgressChanged(string entry, int progress, int max);
        public delegate void Completed();

        public static event ProgressChanged OnProgressChanged;
        public static event DependencyDownload OnDependencyDownload;
        public static event DependencyRunInstaller OnDependencyRunInstaller;
        public static event Extract OnExtract;
        public static event ExtractProgressChanged OnExtractProgressChanged;
        public static event Completed OnDownloadCompleted;

        static GameDownloader()
        {
            OnDownloadCompleted += () => isDownloading = false;

            CleanTempFiles();
        }

        public static void CleanTempFiles() 
        {
            try
            {
                if (Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "/skate 4/"))
                    Directory.Delete(AppDomain.CurrentDomain.BaseDirectory + "/skate 4/", true);

                if (File.Exists(ContainerPath))
                    File.Delete(ContainerPath);
            } catch { }
        }

        public static void Download()
        {
            isDownloading = true;

            WebClient webClient = new WebClient();

            webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler((sender, e) => OnProgressChanged?.Invoke(e.BytesReceived, e.TotalBytesToReceive));
            webClient.DownloadFileCompleted += new AsyncCompletedEventHandler((sender, e) =>
            {
                OnDependencyDownload?.Invoke();

                DownloadDependencies();
            });

            webClient.DownloadFileAsync(new Uri(DownloadURL), ContainerPath);
        }

        static void DownloadDependencies()
        {
            if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "/deps/"))
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "/deps/");

            DownloadDependency(0, () => ExtractGameFiles());
        }

        static void DownloadDependency(int iterator, Action OnFinish = null)
        {
            string url = dependencies.Keys.ToArray()[iterator];
            string path = dependencies[url];

            WebClient webClient = new WebClient();

            webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler((sender, e) => OnProgressChanged?.Invoke(e.BytesReceived, e.TotalBytesToReceive));

            // After vc_redist.x86 is downloaded, download vc_redist.x64
            webClient.DownloadFileCompleted += new AsyncCompletedEventHandler((sender, e) =>
            {
            //if (File.Exists(path))
            //{
            //    Process process = new Process();
            //    process.StartInfo.FileName = path;
            //    process.Start();
            //    process.WaitForExit();
            //}

            //OnDependencyRunInstaller?.Invoke();

                if (iterator == dependencies.Count - 1)
                    OnFinish?.Invoke();
                else
                    DownloadDependency(iterator + 1, OnFinish);
            });


            webClient.DownloadFileAsync(new Uri(url), path);
        }

        static async void ExtractGameFiles()
        {
            if (!isDownloading) return;

            OnExtract();

            await Task.Run(() =>
            {
                //Extract RAR
                using (var archive = RarArchive.Open(ContainerPath))
                {
                    int i = 0;
                    foreach (var entry in archive.Entries.Where(entry => !entry.IsDirectory))
                    {
                        entry.WriteToDirectory(@AppDomain.CurrentDomain.BaseDirectory, new ExtractionOptions()
                        {
                            ExtractFullPath = true,
                            Overwrite = true
                        });

                        OnExtractProgressChanged?.Invoke(entry.ToString(), archive.Entries.Count(), i);

                        i++;
                    }
                }

                //Move files from skate 4 folder to base directory
                if (Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "/skate 4/"))
                {
                    foreach (var file in new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + "/skate 4/").GetFiles())
                    {
                        file.MoveTo($@"{AppDomain.CurrentDomain.BaseDirectory}\{file.Name}");
                    }

                    foreach (var dir in new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + "/skate 4/").GetDirectories())
                    {
                        dir.MoveTo($@"{AppDomain.CurrentDomain.BaseDirectory}\{dir.Name}");
                    }
                }
            });

            CleanTempFiles();

            OnDownloadCompleted();
        }
    }
}
