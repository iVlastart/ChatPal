using System.Net.Sockets;
using System.Net;

namespace ChatPalServer
{
    class Program
    {
        static TcpListener _listener;

        static void Main(string[] args)
        {
            _listener = new TcpListener(IPAddress.Parse("127.0.0.1"), 6969);
            _listener.Start();

            var client = _listener.AcceptTcpClient();
            Console.WriteLine("Client has connected");
        }
    }
}
