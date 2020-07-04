using System;
using System.Collections.Generic;
using System.Text;

namespace Hardware.Networking
{
    public interface GameSocket
    {
        bool isConnected { get; }
        void Disconnect();
        void SendData(PacketData data);
        PacketData GetData();
    }
}
