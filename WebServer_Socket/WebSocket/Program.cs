using program;

namespace HTTPServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Server server = new Server("127.0.0.1", 80);
            server.Start();
        }
    }
}
