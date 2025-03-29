using System.Net.Sockets;
using System.Net;
using ChatPalServer.Net.IO;

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

                broadcastConnection();

            }
        }

        static void broadcastConnection()
        {
            foreach(var user in _users)
            {
                foreach(var usr in _users)
                {
                    var broacastPacket = new PacketBuilder();
                    broacastPacket.writeOpCode(1);
                    broacastPacket.writeString(usr.Username);
                    broacastPacket.writeString(usr.UID.ToString());
                    user.Client.Client.Send(broacastPacket.getPacketBytes());
                }
            }
        }

        public static void broadcastMsg(string msg)
        {
            foreach (var user in _users)
            {
                var msgPacket = new PacketBuilder();
                msgPacket.writeOpCode(5);
                msgPacket.writeString(msg);
                user.Client.Client.Send(msgPacket.getPacketBytes());
            }
        }

        public static void broadcastDisconnect(string UID)
        {
            var disconnectedUser = _users.Where(x=>x.UID.ToString() == UID).FirstOrDefault();
            _users.Remove(disconnectedUser);
            foreach (var user in _users)
            {
                var broadcastPacket = new PacketBuilder();
                broadcastPacket.writeOpCode(10);
                broadcastPacket.writeString(UID);
                user.Client.Client.Send(broadcastPacket.getPacketBytes());
            }
            broadcastMsg($"{disconnectedUser.Username} has disconnected");
        }
    }
}
