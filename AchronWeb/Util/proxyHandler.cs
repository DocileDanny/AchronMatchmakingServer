using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace Networking
{
    class proxyHandler
    {
        static int requestCount = 0;

        TcpClient socket;
        IPEndPoint endPoint;
        long startTime = DateTime.UtcNow.Ticks / 1000;

        //create a new client handler
        public proxyHandler(TcpClient socket)
        {
            this.socket = socket;
            endPoint = (IPEndPoint)socket.Client.RemoteEndPoint;

            Console.WriteLine("new client connected. [" + socket.Client.RemoteEndPoint.ToString() + "]");
        }

        //handle this client
        public void start()
        {

            while (true)
            {
                //if ((DateTime.UtcNow.Ticks / 1000) - startTime > 1000) { this.socket.Close(); }

                TcpClient serv = new TcpClient();
                serv.Connect("52.176.111.39", 80);


                NetworkStream NSserv = serv.GetStream();
                NetworkStream NSclient = socket.GetStream();

                System.Threading.Thread.Sleep(1000);

                while (serv.Connected)
                {
                    System.Threading.Thread.Sleep(0);

                    //data from the server
                    if (serv.Connected && serv.Available != 0)
                    {

                        byte[] dat = new byte[serv.Available];
                        NSserv.Read(dat, 0, serv.Available);

                        string raw = UTF8Encoding.UTF8.GetString(dat, 0, dat.Length);

                        Console.WriteLine("ŽŽŽŽŽŽŽŽŽŽŽŽŽŽŽŽŽŽ" + "\r\n" + "SERVER: " + "\r\n" + "\r\n" + raw + "\r\n" + "\r\n");
                        
                        NSclient.Write(dat, 0, dat.Length);
                    }

                    //data to the client
                    if (socket.Connected && socket.Available != 0)
                    {

                        byte[] dat = new byte[socket.Available];
                        NSclient.Read(dat, 0, socket.Available);

                        string raw = UTF8Encoding.UTF8.GetString(dat, 0, dat.Length);

                        Console.WriteLine("ŽŽŽŽŽŽŽŽŽŽŽŽŽŽŽŽŽŽ" + "\r\n" + "CLIENT: " + "\r\n" + "\r\n" + raw + "\r\n" + "\r\n");

                        NSserv.Write(dat, 0, dat.Length);
                    }

                    if (!socket.Connected || !serv.Connected)
                    {
                        //socket.Close();
                        //serv.Close();
                    }
                }
            }

            /*
            if (uri == "/")
            {
                if (ns.CanWrite)
                {
                    string[] names = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceNames();

                    string answerString = "";

                    foreach (string s in names)
                    {
                        string working = s;
                        //working = s.Replace("/png", ".png");
                        working = s.Replace("Client.Assets.", "");
                        Console.WriteLine(working);
                        answerString += "<p><a href=\"" + "/" + working + "\">" + s + "</a></p>" + "\r\n";
                    }


                    byte[] toSend =
                        System.Text.UTF8Encoding.UTF8.GetBytes
                        (
                            "HTTP/1.1 200 Okey-Day" + "\r\n" +
                            "Server: Faith" + "\r\n" +
                            "\r\n" +

                            "<!DOCTYPE HTML PUBLIC \"-//IETF//DTD HTML 2.0//EN\">" + "\r\n" +
                            "<html>" + "\r\n" +
                            "<head>" + "\r\n" +
                            "   <title>Faith Internal Asset Access</title>" + "\r\n" +
                            "</head>" + "\r\n" +
                            "<body>" + "\r\n" +
                            "   <h1>Faith Internal Asset Access</h1>" + "\r\n" +
                            "   <p>Please select a file to view (" + requestCount + " request(s) have been handled.)</p>" + "\r\n" + answerString +
                            "</body>" + "\r\n" +
                            "</html>" + "\r\n"
                        );

                    ns.Write(toSend, 0, toSend.Length);

                    ns.Close();
                    socket.Close();
                }
            }
            else
            {
                try
                {
                    System.IO.Stream fileStream = null; //Hardware.Manager.GetResource(uri.Substring(1));

                    if (ns.CanWrite && fileStream.CanRead)
                    {

                        byte[] toSend =
                            System.Text.UTF8Encoding.UTF8.GetBytes
                            (
                                "HTTP/1.1 200 OKAY" + "\r\n" +
                                "Server: Faith" + "\r\n" +
                                "Cake: Is a lie" + "\r\n" +
                                "\r\n"
                            );

                        ns.Write(toSend, 0, toSend.Length);

                        byte[] file = new byte[fileStream.Length];
                        fileStream.Read(file, 0, (int)fileStream.Length);
                        ns.Write(file, 0, (int)fileStream.Length);

                        ns.Close();
                        socket.Close();
                    }
                }
                catch //file not found
                {
                    if (ns.CanWrite)
                    {
                        byte[] toSend =
                            System.Text.UTF8Encoding.UTF8.GetBytes
                            (
                                "HTTP/1.1 404 Not Found" + "\r\n" +
                                "Server: Faith" + "\r\n" +
                                "\r\n" +

                                "<!DOCTYPE HTML PUBLIC \"-//IETF//DTD HTML 2.0//EN\">" + "\r\n" +
                                "<html>" + "\r\n" +
                                "<head>" + "\r\n" +
                                "   <title>Faith Internal Asset Access</title>" + "\r\n" +
                                "</head>" + "\r\n" +
                                "<body>" + "\r\n" +
                                "   <h1>404</h1>" + "\r\n" +
                                "   <p>Request asset not found</p>" + "\r\n" +
                                "</body>" + "\r\n" +
                                "</html>" + "\r\n"
                            );

                        ns.Write(toSend, 0, toSend.Length);

                        ns.Close();
                        socket.Close();
                    }
                }


            }
            */
        } } }
                    //no idea what to do!

        /*
                    else
                    {
                        requestCount++;
                        if (ns.CanWrite)
                        {
                            byte[] toSend =
                                UTF8Encoding.UTF8.GetBytes
                                (
                                    "HTTP/1.1 501 Not Implemented" + "\r\n" +
                                    "Server: Faith" + "\r\n" +
                                    "\r\n" +

                                    "<!DOCTYPE HTML PUBLIC \"-//IETF//DTD HTML 2.0//EN\">" + "\r\n" +
                                    "<html>" + "\r\n" +
                                    "<head>" + "\r\n" +
                                    "   <title>501 Not Implemented</title>" + "\r\n" +
                                    "</head>" + "\r\n" +
                                    "<body>" + "\r\n" +
                                    "   <h1>501 Not Implemented</h1>" + "\r\n" +
                                    "   <p>This basic webserver is unable to handle this type of request.</p>" + "\r\n" +
                                    "</body>" + "\r\n" +
                                    "</html>" + "\r\n"
                                );
                            
                            ns.Write(toSend, 0, toSend.Length);
                            
                            ns.Close();
                            socket.Close();
                        }
                    }

                }

            }

        }

    }
}
*/