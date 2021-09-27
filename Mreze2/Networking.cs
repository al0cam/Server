using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading.Tasks;
using System.Threading;

namespace Server
{
    class Networking
    {
        private static TcpListener Listener;
        private static int portNum = 5005;
        private static IPAddress Ip = IPAddress.Parse("127.0.0.1");
        private static bool stopStream = false;
        private static TcpClient Client;
        private static NetworkStream networkStream;
        private static byte[] message;
        private static Task startPlay;
        public static string textGlobal;
        public static void Init()
        {
            Listener = new TcpListener(Ip, portNum);
            message = new byte[1024];
        }
        public static void Listen()
        {
            Listener.Start();
            Console.WriteLine("Listening started");
            Client = Listener.AcceptTcpClient();
            networkStream = Client.GetStream();
            while (!stopStream)
            {
                textGlobal = Read();

                switch(textGlobal)
                {
                    case "Play": Console.WriteLine(textGlobal);  Play(); break;
                    case "Pause": Console.WriteLine(textGlobal);  Pause(); break;
                    case "NextSong": Console.WriteLine(textGlobal);  NextSong(); break;
                    case "PreviousSong": Console.WriteLine(textGlobal);  PreviousSong(); break;
                    case "GetSong": Console.WriteLine(textGlobal); Write(GetSong()); break;
                }
            }
            Listener.Stop();
        }
        public static string Read()
        {
            int read = networkStream.Read(message, 0, message.Length);
            string text = Encoding.ASCII.GetString(message, 0, read);
            return text;
        }
        public static void Write(string whatDoISend)
        {
            message = Encoding.ASCII.GetBytes(whatDoISend);
            networkStream.Write(message, 0, message.Length);
        }

        public static string GetSong()
        {
            return VLC.GetSong();
        }
        public static void NextSong()
        {
            Console.WriteLine("Gon choose song");
            VLC.SetFile(++VLC.currentSong);
            Play();
        }
        public static void  PreviousSong()
        {
            Console.WriteLine("Gon choose song");
            VLC.SetFile(--VLC.currentSong);
            Play();
        }
        public static void Pause()
        {
            VLC.Pause();
        }
        public static void Play()
        {
            VLC.Play();
        }
    }
}
