using System;
using System.Collections.Generic;
using System.Threading;
using System.Net;
using System.Net.Sockets;

namespace program
{
    class Server
    {
        public EndPoint Ip;
        public int Listen;
        Socket Listener;
        public bool Active;
        public Server(int port)
        {
            this.Listen = port;
            this.Ip = new IPEndPoint(Dns.GetHostAddresses(Dns.GetHostName())[0], Listen);
            Listener = new Socket(AddressFamily.InterNetwork ,SocketType.Stream, ProtocolType.Tcp);
        }
        public Server(string ip, int port)
        {
            this.Listen = port;
            this.Ip = new IPEndPoint(IPAddress.Parse(ip), Listen);
            Listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }
        public void Start()
        {
            if (!Active)
            {
                Listener.Bind(Ip);
                Listener.Listen(16);
                Active = true;

                while (Active)
                {
                    ThreadPool.QueueUserWorkItem(
                            new WaitCallback(ClientThread),
                            Listener.Accept()
                            );
                }
            }
            else
                Console.WriteLine("Server was started");
        }
        public void Stop()
        {
            if (Active)
            {
                Listener.Close();
                Active = false;
            }
            else
                Console.WriteLine("Server was stopped");
        }
        public void ClientThread(object client)
        {
            new Client((Socket)client);
        }
    }
}
