using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using Hardware.Networking;

namespace Hardware.Server.Networking
{
    /// <summary>
    /// A network server
    /// </summary>
    public class glServer : SocketServer
    {
        /// <summary>
        /// The listener
        /// </summary>
        TcpListener server;

        /// <summary>
        /// pending sockets
        /// </summary>
        Queue<GameSocket> pendingSocks;

        /// <summary>
        /// Create a new socket server.
        /// </summary>
        public glServer(int port, bool ipv6)
        {
            pendingSocks = new Queue<GameSocket>();

            if (ipv6)
            {
                server = new TcpListener(IPAddress.IPv6Any, port);
            }
            else
            {
                server = new TcpListener(IPAddress.Any, port);
            }

            server.Start();

            System.Threading.Thread t = new System.Threading.Thread(runServer);
            t.Start();
        }

        void runServer()
        {

            while (true)
            {
                System.Threading.Thread.Sleep(1);

                if (server.Pending())
                {
                    TcpClient rawSock = server.AcceptTcpClient();
                    glSocket client = new glSocket(rawSock);
                    pendingSocks.Enqueue( (GameSocket)client );
                }
            }
        }

        /// <summary>
        /// Get a waiting socket
        /// </summary>
        public GameSocket getSocket()
        {
            if (pendingSocks.Count > 0)
            {
                return pendingSocks.Dequeue();
            }
            return null;
        }

    }
}
