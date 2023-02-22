using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace program
{
    class Server
    {
        public EndPoint Ip;
        public int Listen;
        public bool Active;
        private Socket _listener;
        private volatile CancellationTokenSource _cts;


        public Server(int port)
        {
            this.Listen = port;
            this.Ip = new IPEndPoint(Dns.GetHostAddresses(Dns.GetHostName())[0], Listen);
            this._listener = new Socket(AddressFamily.InterNetwork ,SocketType.Stream, ProtocolType.Tcp);
            this._cts = new CancellationTokenSource();
        }
        public Server(string ip, int port)
        {
            this.Listen = port;
            this.Ip = new IPEndPoint(IPAddress.Parse(ip), Listen);
            this._listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            this._cts = new CancellationTokenSource();
        }
        public void Start()
        {
            if (!Active)
            {
                _listener.Bind(Ip);
                _listener.Listen(16);
                Active = true;
                while(Active || !_cts.Token.IsCancellationRequested){
                    try
                    {
                        Socket listenerAccept = _listener.Accept();
                        if(listenerAccept != null)
                        {
                            Task.Run(
                                ()=>ClientThread(listenerAccept),
                                _cts.Token
                            );
                        }
                    }catch{}
                }
            }
            else
            {
                Console.WriteLine("Server was started");
            }
        }
        public void Stop()
        {
            if (Active)
            {
                _cts.Cancel();
                _listener.Close();
                Active = false;
            }
            else
            {
                Console.WriteLine("Server was stopped");
            }
        }
        public void ClientThread(Socket client)
        {
            new Client(client);
        }
    }
}
