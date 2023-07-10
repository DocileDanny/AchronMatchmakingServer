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
            NetworkStream ns = socket.GetStream();

            while (socket.Connected)
            {

                //we have data to handle!
                if (socket.Available != 0)
                {
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
                        Console.WriteLine("[STEAM-CLIENT] User " + argList["OxO02O"] + " connecting..");
                        byte[] reply = AchronWeb.packets.registerPacketA.Handle(argList["OxO02O"], argList["Ox7c37"]);
                        ns.Write(reply, 0, reply.Length);
                        ns.Flush();
                        System.Threading.Thread.Sleep(500);
                        ns.Close();
                        socket.Close();
                    }

                    //registration packet A legacy (OxO02O & OxO02a)
                    else if (argList.ContainsKey("OxO02O") && argList.ContainsKey("OxO02a") && !argList.ContainsKey("OxO04O"))
                    {
                        Console.WriteLine("[WEB-CLIENT] User " + argList["OxO02O"] + " connecting..");
                        byte[] reply = AchronWeb.packets.registerPacketALegacy.Handle(argList["OxO02O"], argList["OxO02a"]);
                        ns.Write(reply, 0, reply.Length);
                        ns.Flush();
                        System.Threading.Thread.Sleep(500);
                        ns.Close();
                        socket.Close();
                    }

                    //registration packet B (OxO02O & OxO02a & OxO04O)
                            else if (argList.ContainsKey("OxO02O") && argList.ContainsKey("OxO02a") && argList.ContainsKey("OxO04O"))
                    {
                        Console.WriteLine("User " + argList["OxO02O"] + " connected.");
                        byte[] reply = AchronWeb.packets.registerPacketB.Handle(argList["OxO02O"], argList["OxO02a"], argList["OxO04O"]);
                        ns.Write(reply, 0, reply.Length);
                        ns.Flush();
                        System.Threading.Thread.Sleep(500);
                        ns.Close();
                        socket.Close();
                    }

                    //registration packet B (OxO02O & OxO02a & OxO04O)
                    else if (argList.ContainsKey("OxO04O") && argList.Count == 1)
                    {
                        AchronWeb.features.achronClient user = AchronWeb.features.consts.getUser(argList["OxO04O"]);

                        if (user != null)
                        {
                            Console.WriteLine(user.username + " checks the game list..");
                        }
                        else
                        {
                            Console.WriteLine("Check game list.. [unknown user]");
                        }

                        byte[] reply = AchronWeb.packets.viewGamesPacket.Handle(argList["OxO04O"]);
                        ns.Write(reply, 0, reply.Length);
                        ns.Flush();
                        System.Threading.Thread.Sleep(500);
                        ns.Close();
                        socket.Close();
                    }

                    //createGamePacket string OxO181, string OxO2O1, string OxO21O, string OxO39O, string OxO04O
                    else if (argList.ContainsKey("OxO181") && argList.ContainsKey("OxO2O1") && argList.ContainsKey("OxO21O") && argList.ContainsKey("OxO39O") && argList.ContainsKey("OxO04O"))
                    {
                        AchronWeb.features.achronClient user = AchronWeb.features.consts.getUser(argList["OxO04O"]);

                        if (user != null)
                        {
                            Console.WriteLine(user.username + " creates a game..");
                        }
                        else
                        {
                            Console.WriteLine("create game.. [unknown user]?");
                            return;
                        }

                        byte[] reply = AchronWeb.packets.createGamePacket.Handle(argList["OxO181"], argList["OxO2O1"], argList["OxO21O"], argList["OxO39O"], argList["OxO04O"], endPoint.Address.ToString());
                        ns.Write(reply, 0, reply.Length);
                        ns.Flush();
                        System.Threading.Thread.Sleep(500);
                        ns.Close();
                        socket.Close();
                    }

                    //Join Game?
                    else if (argList.ContainsKey("Ox910O") && argList.ContainsKey("OxO04O") && argList.Count == 2)
                    {
                        string gameID = argList["Ox910O"];
                        if (!long.TryParse(gameID, out long a)) { return; }

                        AchronWeb.features.achronClient client = AchronWeb.features.consts.getUser(argList["OxO04O"]);
                        if (client == null) { return; }
                        string user = client.username;
                        
                        //Get the actionID.
                        string action = argList["OxO04O"].Substring(argList["OxO04O"].Length - 7);

                        string prettyAction = "unknown";
                        switch (action)
                        {
                            case "62f9d01": //join game
                                prettyAction = "join game? [62f9d01]";
                                lock (AchronWeb.features.consts.gameList)
                                {
                                    if (AchronWeb.features.consts.gameList.ContainsKey(long.Parse(gameID)))
                                    {
                                        AchronWeb.features.achronGame game = AchronWeb.features.consts.gameList[long.Parse(gameID)];
                                        game.currentPlayers.Add(user);
                                        game.lastUpdate = AchronWeb.features.consts.GetTime();
                                    }
                                }
                                break;
                            case "0000001": //game start?
                                prettyAction = "game start [0000001]";
                                lock (AchronWeb.features.consts.gameList)
                                {
                                    if (AchronWeb.features.consts.gameList.ContainsKey(long.Parse(gameID)))
                                    {
                                        AchronWeb.features.achronGame game = AchronWeb.features.consts.gameList[long.Parse(gameID)];
                                        if (game.ownerSESSID == AchronWeb.features.consts.getUser(argList["OxO04O"]).SESSID)
                                        {
                                            game.Progress = 1;
                                            game.lastUpdate = AchronWeb.features.consts.GetTime();
                                        }
                                    }
                                }
                                break;
                            case "62a870b": //leave a game
                                prettyAction = "leave game [62a870b]";
                                lock (AchronWeb.features.consts.gameList)
                                {
                                    if (AchronWeb.features.consts.gameList.ContainsKey(long.Parse(gameID)))
                                    {
                                        AchronWeb.features.achronGame game = AchronWeb.features.consts.gameList[long.Parse(gameID)];
                                        if (game.ownerSESSID == AchronWeb.features.consts.getUser(argList["OxO04O"]).SESSID)
                                        {
                                            AchronWeb.features.consts.gameList.Remove(long.Parse(gameID));
                                        }
                                        else
                                        {
                                            game.currentPlayers.Remove(user);
                                            game.lastUpdate = AchronWeb.features.consts.GetTime();
                                        }
                                    }
                                }
                                break;
                            case "657b2cf":
                                prettyAction = "game end [657b2cf]";
                                lock (AchronWeb.features.consts.gameList)
                                {
                                    if (AchronWeb.features.consts.gameList.ContainsKey(long.Parse(gameID)))
                                    {
                                        AchronWeb.features.consts.gameList.Remove(long.Parse(gameID));                                        
                                    }
                                }
                                break;
                            case "0000000":
                                prettyAction = "game ping [0000000]"; //game is still open
                                lock (AchronWeb.features.consts.gameList)
                                {
                                    if (AchronWeb.features.consts.gameList.ContainsKey(long.Parse(gameID)))
                                    {
                                        AchronWeb.features.achronGame game = AchronWeb.features.consts.gameList[long.Parse(gameID)];
                                        if (game.ownerSESSID == AchronWeb.features.consts.getUser(argList["OxO04O"]).SESSID)
                                        {
                                            game.lastUpdate = AchronWeb.features.consts.GetTime();
                                        }
                                    }
                                }
                                break;
                        }

                        Console.WriteLine(user + " is doing.. something. (" + prettyAction + "?) #" + gameID);
                        
                        byte[] reply = AchronWeb.packets.okPacket.Handle();
                        ns.Write(reply, 0, reply.Length);
                        ns.Flush();
                        System.Threading.Thread.Sleep(500);
                        ns.Close();
                        socket.Close();
                    }

                    //Leave Game?
                    //Host tells the server a user has left the game, and who it was
                    //probably a failsafe, in case the user left because of a game crash.
                    else if (argList.ContainsKey("Ox910O") && argList.ContainsKey("OxO04O") && argList.ContainsKey("OxO02a") && argList.Count == 3)
                    {

                        string gameID = argList["Ox910O"];
                        if (!long.TryParse(gameID, out long a)) { return; }

                        AchronWeb.features.achronClient client = AchronWeb.features.consts.getUser(argList["OxO04O"]);
                        if (client == null) { return; }
                        string user = client.username;

                        Console.WriteLine(user + " (host) says " + argList["OxO02a"] + " has left game #" + gameID + ".");

                        lock (AchronWeb.features.consts.gameList)
                        {
                            if (AchronWeb.features.consts.gameList.ContainsKey(long.Parse(gameID)))
                            {
                                //remove the player from the game if he is in it.
                                AchronWeb.features.achronGame game = AchronWeb.features.consts.gameList[long.Parse(gameID)];
                                game.currentPlayers.Remove(argList["OxO02a"]);
                                game.lastUpdate = AchronWeb.features.consts.GetTime();
                                
                            }
                        }

                        byte[] reply = AchronWeb.packets.okPacket.Handle();
                        ns.Write(reply, 0, reply.Length);
                        ns.Flush();
                        System.Threading.Thread.Sleep(500);
                        ns.Close();
                        socket.Close();
                    }

                    //End Game?
                    else if (argList.ContainsKey("Ox910O") && //game ID
                        argList.ContainsKey("Ox411c") && //User list
                        argList.ContainsKey("OxO04O") && //user hash + misc data
                        argList.ContainsKey("Ox50fa") && //Who knows, always 21855 apparently or 21853 ; could be win or loss.
                        argList.Count == 4)
                    {
                        string gameID = argList["Ox910O"];
                        if (!long.TryParse(gameID, out long a)) { return; }

                        AchronWeb.features.achronClient client = AchronWeb.features.consts.getUser(argList["OxO04O"]);
                        if (client == null) { return; }
                        string user = client.username;

                        Console.WriteLine(user + " is doing.. something? (end game?) with " + argList["Ox411c"] + " in game #" + gameID + " [" + argList.ContainsKey("Ox50fa") + "]");

                        lock (AchronWeb.features.consts.gameList)
                        {
                            if (AchronWeb.features.consts.gameList.ContainsKey(long.Parse(gameID)))
                            {
                                AchronWeb.features.consts.gameList.Remove(long.Parse(gameID));
                            }
                        }

                        byte[] reply = AchronWeb.packets.okPacket.Handle();
                        ns.Write(reply, 0, reply.Length);
                        ns.Flush();
                        System.Threading.Thread.Sleep(500);
                        ns.Close();
                        socket.Close();
                    }

                    else
                    {
                        try
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

                            ns.Close();
                            socket.Close();
                        }
                        catch
                        {
                            Console.WriteLine("EXCEPTION THROWN [" + endPoint.ToString() + " ]: " + "\r\n" + decode);
                        }
                        finally
                        {
                            //probably a browser
                            if (!decode.Contains("GET /achron_games.php"))
                            {
                                string reply =
                                    "HTTP/1.1 200 OK" + "\r\n" + //OK, we have a valid time
                                    "Date: Now" + "\r\n" + //current datetime
                                    "Server: AchronWeb/0.0.1 (DocileDanny)" + "\r\n" + //server info
                                    "X-Powered-By: C#/" + Environment.Version.ToString() + "\r\n" + //php info
                                    "Cache-Control: no-store, no-cache, must-revalidate, post-check=0, pre-check=0" + "\r\n" + //various info about caching.
                                    "Pragma: no-cache" + "\r\n" + //pragma values
                                    "<!DOCTYPE HTML PUBLIC \"-//IETF//DTD HTML 2.0//EN\">" + "\r\n" +
                                    AchronWeb.features.consts.errorPage + "\r\n"; //the content itself.
                                byte[] toSend = UTF8Encoding.UTF8.GetBytes(reply);
                                ns.Write(toSend, 0, toSend.Length);
                                ns.Flush();
                                System.Threading.Thread.Sleep(500);
                                ns.Close();
                                socket.Close();
                                Console.WriteLine("error page sent to " + endPoint.ToString() + ".");
                            }
                        }
                    }
                }

            }

            Console.WriteLine("[Socket " + endPoint.ToString() + " closed]");
        }

        static int SearchBytes(byte[] haystack, byte[] needle)
        {
            var len = needle.Length;
            var limit = haystack.Length - len;
            for (var i = 0; i <= limit; i++)
            {
                var k = 0;
                for (; k < len; k++)
                {
                    if (needle[k] != haystack[i + k]) break;
                }
                if (k == len) return i;
            }
            return -1;
        }
    }
}
