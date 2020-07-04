using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Networking
{
    class WebServer
    {

        TcpListener socket;
        Thread listenThread;
        bool isProxy;

        public WebServer(int socketID, bool ipV6 = false, bool proxy = false)
        {
            isProxy = proxy;

            if (ipV6)
            {
                //start listening for incomming data
                socket = new TcpListener(IPAddress.IPv6Any, socketID);
            }
            else
            {
                //start listening for incomming data
                socket = new TcpListener(IPAddress.Any, socketID);
            }

            socket.Start();

            //start listening for new clients
            listenThread = new Thread(listen);
            listenThread.Start();
        }

        //the listener thread
        void listen()
        {

            while (true)
            {
                System.Threading.Thread.Sleep(1);

                if (socket.Pending())
                {
                    TcpClient aClient = socket.AcceptTcpClient();
                    Thread HandleThread = null;

                    //create a new handler
                    if (isProxy)
                    {
                        //we are just acting as a man in the middle for viewing data as it flows.
                        proxyHandler ch = new proxyHandler(aClient);
                        HandleThread = new Thread(ch.start);
                    }
                    else
                    {
                        //we are pretending to be the archrongames host server.
                        serverHandler ch = new serverHandler(aClient);
                        HandleThread = new Thread(ch.start);
                    }

                    //start the client handle
                    HandleThread.Start();
                }
            }

        }

    }
}
