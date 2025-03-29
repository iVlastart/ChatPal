using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatPalServer.Net.IO
{
    public class PacketBuilder
    {
        MemoryStream _ms;
        public PacketBuilder()
        {
            this._ms = new MemoryStream();
        }

        public void writeOpCode(byte opcode)
        {
            _ms.WriteByte(opcode);
        }

        public void writeString(string msg)
        {
            var msgLenght = msg.Length;
            _ms.Write(BitConverter.GetBytes(msgLenght));
            _ms.Write(Encoding.ASCII.GetBytes(msg));
        }

        public byte[] getPacketBytes()
        {
            return _ms.ToArray();
        }
    }
}