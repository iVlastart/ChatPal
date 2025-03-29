using ChatPalServer.Net.IO;
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
        PacketReader _packetReader;
        public Clients(TcpClient client)
        {
            this.Client = client;
            this.UID = Guid.NewGuid();
            _packetReader = new PacketReader(Client.GetStream());
            var opcode = _packetReader.ReadByte();
            Username = _packetReader.readMsg();
            Console.WriteLine($"{DateTime.Now}: {Username} has connected!");

            Task.Run(() => process());
        }

        void process()
        {
            while (true)
            {
                try
                {
                    var opcode = _packetReader.ReadByte();
                    switch(opcode)
                    {
                        case 5:
                            var msg = _packetReader.readMsg();
                            Console.WriteLine(msg);
                            Program.broadcastMsg(msg);
                            break;
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine($"{UID} disconnected");
                    Program.broadcastDisconnect(UID.ToString());
                    Client.Close();
                    break;
                }
            }
        }
    }
}
