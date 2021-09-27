using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    class VLC
    {
        private static string[] filePaths = Directory.GetFiles(Environment.CurrentDirectory + "\\..\\..\\..\\Resources\\", "*.mp3");
        private static List<FileInfo> file = new List<FileInfo>();
        public static  uint currentSong = 0;
        private static Vlc.DotNet.Core.VlcMediaPlayer mediaPlayer;
        private static string[] mediaOptions;
        public static void Init()
        {
            foreach (var path in filePaths)
                file.Add(new FileInfo(path));

            var currentDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            var libDirectory = new DirectoryInfo(Path.Combine(currentDirectory, "libvlc", IntPtr.Size == 4 ? "win-x86" : "win-x64"));
            mediaPlayer = new Vlc.DotNet.Core.VlcMediaPlayer(libDirectory);

            mediaOptions = new[]
            {
                ":sout=#rtp{dst=127.0.0.1,port=5004,mux=ts}",
                " :no-sout-all",
                " :sout-keep"
            };
            SetFile(currentSong);
        }
        public static void SetFile(uint fileNumber)
        {
            Pause();
            if (fileNumber > (file.Count * 2))
                fileNumber = (uint)(file.Count) - 1;
            uint songnumber = (uint)(fileNumber % file.Count);
            mediaPlayer.SetMedia(file[(int)(songnumber)], mediaOptions);
            currentSong = songnumber;
            Console.WriteLine(fileNumber);
        }
        public static void Play()
        {
            mediaPlayer.Play();
            Console.WriteLine("dst=127.0.0.1,port=5004,mux=ts");
            
        }
        
        public static string GetSong()
        {
            return file[(int)(currentSong)].Name;
        }
        public static void Pause()
        {
            mediaPlayer.Stop();
        }
        
    }
}
