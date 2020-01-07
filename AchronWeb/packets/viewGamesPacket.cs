using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AchronWeb.features;
using System.Security.Cryptography;

namespace AchronWeb.packets
{
    /// <summary>
    /// Respond to a registration request
    /// </summary>
    public static class viewGamesPacket
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="xO02O">username</param>
        /// <param name="x7c37">steamID???</param>
        public static byte[] Handle(string OxO04O)
        {
            //client asking to see a list of games.
            //OxO04O a uniqueID containing the hash we sent before.

            string content = "";

            foreach (KeyValuePair<long, achronGame> game in consts.gameList)
            {
                content +=
                    game.Value.gameID + @"\" +
                    game.Value.currentPlayers + @"\" +
                    game.Value.maxPlayers + @"\" +
                    game.Value.level + @"\" +
                    game.Value.host + @"\" +
                    game.Value.gameName + @"\" +
                    "0" + @"\" + //again, unsure what this value is for; usually seems to be 0.
                    game.Value.portA + @"\" +
                    game.Value.portB + @"\" +
                    "" + @"\" + //not sure what this value is for.
                    game.Value.Progress + @"\" +
                    game.Value.gamePlayerHost + Environment.NewLine;
            }

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
    }
}
