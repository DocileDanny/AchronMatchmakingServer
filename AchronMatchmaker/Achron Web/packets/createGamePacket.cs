using System;
using System.Collections.Generic;
using System.Text;
using AchronWeb.features;
using System.Security.Cryptography;

namespace AchronWeb.packets
{
    /// <summary>
    /// Respond to a registration request
    /// </summary>
    public static class createGamePacket
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="xO02O">username</param>
        /// <param name="x7c37">steamID???</param>
        public static byte[] Handle(string OxO181, string OxO2O1, string OxO21O, string OxO39O, string OxO04O, string endPoint)
        {
            //client creating a new game.
            //OxO181 is the clients internal IP address
            //OxO2O1 is the games name.
            //OxO21O no idea just now; it's 0 in my test case; maybe saying if the game is in progress? not sure.
            //OxO39O this is the current level.
            //xO040 is the UID in this case
            //endPoint is the external IP for this client.

            //get the user who is trying to create the game
            achronClient user = consts.getUser(OxO04O);
            if (user == null) { return new byte[0]; }

            //create a new game
            achronGame game = new achronGame();
            game.currentPlayers = 1;
            game.maxPlayers = 8;
            game.gamePlayerHost = user.username;
            game.portA = 7014; //default, maybe the client will update this later?
            game.portB = 7013; //default, maybe the client will update this later?
            game.gameName = OxO2O1;
            game.host = endPoint;
            game.lastUpdate = consts.GetTime();
            game.level = OxO39O.Replace("%20", " ");
            game.Progress = 0; //lets assume if we are creating a game, the game is yet to start.
            game.ownerSESSID = user.SESSID;

            //A second copy of the game to be sent/processed to a remote server.
            achronGame sgame = new achronGame();
            sgame.currentPlayers = 1;
            sgame.maxPlayers = 8;
            sgame.gamePlayerHost = user.username;
            sgame.portA = 7014; //default, maybe the client will update this later?
            sgame.portB = 7013; //default, maybe the client will update this later?
            sgame.gameName = OxO2O1;
            sgame.host = endPoint;
            sgame.lastUpdate = consts.GetTime();
            sgame.level = OxO39O.Replace("%20", " ");
            sgame.Progress = 0; //lets assume if we are creating a game, the game is yet to start.
            sgame.ownerSESSID = user.SESSID;

            lock (consts.gameList)
            {              
                consts.gameCount++;
                game.gameID = consts.gameCount;
                sgame.gameID = consts.gameCount;
                consts.gameList.Add(game.gameID, game);
            }

            lock (consts.gameSendList)
            {
                consts.gameSendList.Enqueue(sgame);
            }

            string content = game.gameID.ToString(); //tell the client the ID of the game they created.

            string reply =
                "HTTP/1.1 200 OK" + "\r\n" + //OK, we have a valid time
                "Date: Now" + "\r\n" + //current datetime
                "Server: AchronWeb/0.0.1 (DocileDanny)" + "\r\n" + //server info
                "X-Powered-By: C#/" + Environment.Version.ToString() + "\r\n" + //php info
                                                                                             //"Set-Cookie: PHPSESSID=" + client.SESSID + "; path=/" + "\r\n" + //set the sessid cookie
                                                                                             //"Expires: Thu, 19 Nov 1981 08:52:00 GMT" + "\r\n" + //when the cookie expires
                "Cache-Control: no-store, no-cache, must-revalidate, post-check=0, pre-check=0" + "\r\n" + //various info about caching.
                "Pragma: no-cache" + "\r\n" + //pragma values
                "Content-Length: " + content.Length.ToString() + "\r\n" + //how long is the content
                "Content-Type: text/plain; charset=UTF-8" + "\r\n" + "\r\n" + //what is the content
                content + " " + "\r\n"; //the content itself.

            return UTF8Encoding.UTF8.GetBytes(reply);
        }


        private static string GetHash(HashAlgorithm hashAlgorithm, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            var sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

    }
}
