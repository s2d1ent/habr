using System;
using System.Threading;
using System.Threading.Tasks;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Collections;
using System.Collections.Generic;
using program;

namespace HTTPServer
{
    class Program
    {
        static void Main(string[] args)
        {
            ThreadPool.SetMinThreads(2, 2);
            ThreadPool.SetMinThreads(4, 4);
            Server server = new Server("127.0.0.1", 80);
            server.Start();
        }
    }
}
