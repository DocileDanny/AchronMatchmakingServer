using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace Networking
{
    class serverHandler
    {
        TcpClient socket;
        IPEndPoint endPoint;
        long startTime = DateTime.UtcNow.Ticks / 1000;

        //create a new client handler
        public serverHandler(TcpClient socket)
        {
            this.socket = socket;
            endPoint = (IPEndPoint)socket.Client.RemoteEndPoint;
            Console.WriteLine("new achron client connected. [" + endPoint.ToString() + "]");
        }

        //handle this client
        public void start()
        {
            while (socket.Connected)
            {

                //we have data to handle!
                if (socket.Available != 0)
                {
                    NetworkStream ns = socket.GetStream();
                    byte[] data = new byte[socket.Available];
                    ns.Read(data, 0, socket.Available);
                    string decode = UTF8Encoding.UTF8.GetString(data, 0, data.Length);
                    string[] DataPackets = decode.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

                    string[] args = DataPackets[0].Split(new char[] {' ', '?', '&' });

                    Dictionary<string, string> argList = new Dictionary<string, string>();

                    foreach (string arg in args)
                    {
                        //it's an arguement!
                        if (arg.Contains("="))
                        {
                            string[] values = arg.Split('=');
                            argList.Add(values[0], values[1]);
                        }
                    }

                    //registration packet A (OxO02O & Ox7c37)
                    if (argList.ContainsKey("OxO02O") && argList.ContainsKey("Ox7c37"))
                    {
                        Console.WriteLine("User " + argList["OxO02O"] + " connecting..");
                        byte[] reply = AchronWebtest.packets.registerPacketA.Handle(argList["OxO02O"], argList["Ox7c37"]);
                        ns.Write(reply, 0, reply.Length);
                        ns.Flush();
                        ns.Close();
                        socket.Close();
                    }

                    //registration packet B (OxO02O & OxO02a & OxO04O)
                    else if (argList.ContainsKey("OxO02O") && argList.ContainsKey("OxO02a") && argList.ContainsKey("OxO04O"))
                    {
                        Console.WriteLine("User " + argList["OxO02O"] + " connected.");
                        byte[] reply = AchronWebtest.packets.registerPacketB.Handle(argList["OxO02O"], argList["OxO02a"], argList["OxO04O"]);
                        ns.Write(reply, 0, reply.Length);
                        ns.Flush();
                        ns.Close();
                        socket.Close();
                    }

                    //registration packet B (OxO02O & OxO02a & OxO04O)
                    else if (argList.ContainsKey("OxO04O") && argList.Count == 1)
                    {
                        AchronWebtest.features.achronClient user = AchronWebtest.features.consts.getUser(argList["OxO04O"]);

                        if (user != null)
                        {
                            Console.WriteLine(user.username + " checks the game list..");
                        }
                        else
                        {
                            Console.WriteLine("Check game list.. [unknown user]");
                        }

                        byte[] reply = AchronWebtest.packets.viewGamesPacket.Handle(argList["OxO04O"]);
                        ns.Write(reply, 0, reply.Length);
                        ns.Flush();
                        ns.Close();
                        socket.Close();
                    }

                    //createGamePacket string OxO181, string OxO2O1, string OxO21O, string OxO39O, string OxO04O
                    else if (argList.ContainsKey("OxO181") && argList.ContainsKey("OxO2O1") && argList.ContainsKey("OxO21O") && argList.ContainsKey("OxO39O") && argList.ContainsKey("OxO04O"))
                    {
                        AchronWebtest.features.achronClient user = AchronWebtest.features.consts.getUser(argList["OxO04O"]);

                        if (user != null)
                        {
                            Console.WriteLine(user.username + " creates a game..");
                        }
                        else
                        {
                            Console.WriteLine("create game.. [unknown user]");
                        }

                        byte[] reply = AchronWebtest.packets.createGamePacket.Handle(argList["OxO181"], argList["OxO2O1"], argList["OxO21O"], argList["OxO39O"], argList["OxO04O"], endPoint.Address.ToString());
                        ns.Write(reply, 0, reply.Length);
                        ns.Flush();
                        ns.Close();
                        socket.Close();
                    }

                    else
                    {
                        string packetName = "";
                        foreach (KeyValuePair<string, string> key in argList)
                        {
                            packetName += key.Key + "&";
                        }
                        packetName = packetName.Substring(0, packetName.Length - 1);
                        Console.WriteLine("Unhandled request: " + packetName);
                        
                        Console.WriteLine("=============");
                        Console.WriteLine(decode);    
                        Console.WriteLine("=============");
                        socket.Close();
                    }
                }

            }
        }
    }
}