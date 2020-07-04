using System;
using System.Collections.Generic;
using System.Text;

namespace Hardware.Networking
{
    public interface SocketServer
    {
        GameSocket getSocket();
    }
}