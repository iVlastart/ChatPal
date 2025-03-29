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
        public PacketReader packetReader;
        public event Action connectedEvent;
        public event Action msgReceivedEvent;
        public event Action disconnectedEvent;


        public Server()
        {
            client = new TcpClient();
        }

        public void connect(string username)
        {
            if (!client.Connected)
            {
                client.Connect("127.0.0.1", 6969);
                packetReader = new PacketReader(client.GetStream());
                if(!string.IsNullOrEmpty(username))
                {
                    var connectPacket = new PacketBuilder();
                    connectPacket.writeOpCode(0);
                    connectPacket.writeString(username);
                    client.Client.Send(connectPacket.getPacketBytes());
                }
                readPackets();
            }
        }

        private void readPackets()
        {
            Task.Run(() =>
            {
                while (true)
                {
                    var opcode = packetReader.ReadByte();
                    switch (opcode)
                    {
                        case 1:
                            connectedEvent?.Invoke();
                            break;
                        case 5:
                            msgReceivedEvent?.Invoke();
                            break;
                        case 10:
                            disconnectedEvent?.Invoke();
                            break;
                        default:
                            Console.WriteLine("");
                            break;
                    }
                }
            });
        }

        public void sendMsgToServer(string msg)
        {
            var msgPacket = new PacketBuilder();
            msgPacket.writeOpCode(5);
            msgPacket.writeString(msg);
            client.Client.Send(msgPacket.getPacketBytes());
        }
    }
}
