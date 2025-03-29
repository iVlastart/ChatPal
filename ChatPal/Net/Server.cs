using ChatPal.Net.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChatPal.Net
{
    public class Server
    {
        TcpClient client;

        public Server()
        {
            client = new TcpClient();
        }

        public void connect(string username)
        {
            if (!client.Connected)
            {
                client.Connect("127.0.0.1", 6969);
                var connectPacket = new PacketBuilder();
                connectPacket.writeOpCode(0);
                connectPacket.writeString(username);
                client.Client.Send(connectPacket.getPacketBytes());
            }
        }
    }
}
