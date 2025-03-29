using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChatPalServer
{
    public class Clients
    {
        public string Username { get; set; }
        public Guid UID { get; set; }
        public TcpClient Client { get; set; }
        public Clients(TcpClient client)
        {
            this.Client = client;
            this.UID = Guid.NewGuid();

            Console.WriteLine($"{DateTime.Now}: {Client} has connected!");
        }
    }
}
