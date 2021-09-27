using System;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            VLC.Init();
            Networking.Init();
            Networking.Listen();
        }
    }
}


