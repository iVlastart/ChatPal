using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;

namespace ChatPal.MVVM.Model
{
    internal class WebSocketClient
    {
        private WebSocket _socket;

        public event Action<string> MsgReceived;
        public event Action Connected;
        public event Action Disconnected;

        private void connect(string uri)
        {
            if (_socket != null && _socket.IsAlive) return;

            _socket = new WebSocket(uri);
            _socket.OnOpen += (s, e) => Connected?.Invoke();
            _socket.OnMessage += (s, e) => MsgReceived?.Invoke(e.Data);
            _socket.OnClose += (s, e) => Disconnected?.Invoke();
            _socket.ConnectAsync();
        }

        public void sendMsg(string msg)
        {
            if(_socket!= null && _socket.IsAlive)
            {
                _socket.Send(msg);
            }
        }

        public void disconnect()
        {
            _socket?.Close();
        }
    }
}
