using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AchronWebtest.features;
using System.Security.Cryptography;

namespace AchronWebtest.packets
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
            game.lastUpdate = GetTime();
            game.level = OxO39O.Replace("%20", " ");
            game.Progress = 0; //lets assume if we are creating a game, the game is yet to start.

            lock (consts.gameList)
            {
                consts.gameCount++;
                game.gameID = consts.gameCount;
                consts.gameList.Add(game.gameID, game);
            }



            string content = game.gameID.ToString(); //tell the client the ID of the game they created.

            string reply =
                "HTTP/1.1 200 OK" + Environment.NewLine + //OK, we have a valid time
                "Date: Now" + Environment.NewLine + //current datetime
                "Server: AchronWeb/0.0.1 (DocileDanny)" + Environment.NewLine + //server info
                "X-Powered-By: C#/" + Environment.Version.ToString() + Environment.NewLine + //php info
                                                                                             //"Set-Cookie: PHPSESSID=" + client.SESSID + "; path=/" + Environment.NewLine + //set the sessid cookie
                                                                                             //"Expires: Thu, 19 Nov 1981 08:52:00 GMT" + Environment.NewLine + //when the cookie expires
                "Cache-Control: no-store, no-cache, must-revalidate, post-check=0, pre-check=0" + Environment.NewLine + //various info about caching.
                "Pragma: no-cache" + Environment.NewLine + //pragma values
                "Content-Length: " + content.Length.ToString() + Environment.NewLine + //how long is the content
                "Content-Type: text/plain; charset=UTF-8" + Environment.NewLine + Environment.NewLine + //what is the content
                content + Environment.NewLine; //the content itself.

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
        public static long GetTime()
        {
            return DateTime.UtcNow.Ticks / 10000;
        }
    }
}
