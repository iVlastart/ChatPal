using System.Net.Sockets;
using System.Net;

namespace ChatPalServer
{
    class Program
    {
        static List<Clients> _users;
        static TcpListener _listener;

        static void Main(string[] args)
        {
            _users = new List<Clients>();
            _listener = new TcpListener(IPAddress.Parse("127.0.0.1"), 6969);
            _listener.Start();
            while(true)
            {
                var client = new Clients(_listener.AcceptTcpClient());
                _users.Add(client);
            }
        }
    }
}
