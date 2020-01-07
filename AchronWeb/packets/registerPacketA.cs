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
    public static class registerPacketA
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="xO02O">username</param>
        /// <param name="x7c37">steamID???</param>
        public static byte[] Handle(string xO02O, string x7c37)
        {
            //for now, we can ignore x7c37? assuming it is to do with verifying with steam; which we don't actually care about.

            //create a new client to represent this user.
            achronClient client = new achronClient();
            client.username = xO02O;
            client.firstSeen = GetTime();
            client.lastSeen = client.firstSeen;

            //generate a session ID
            using (SHA256 sha256Hash = SHA256.Create())
            {
                string hash = GetHash(sha256Hash, client.username + client.firstSeen).Substring(0, 32);
                client.SESSID = hash;
            }

            string content = 
                client.SESSID + @"\\" + //SessID / unique ID
                client.username +  //username
                "5e1355173f1786." + GetTime() +  //no idea what this is about
                @"\\1.7.0.0"; //the client version

            consts.clientList.Add(client.SESSID, client);

            string reply = 
                "HTTP/1.1 200 OK" + Environment.NewLine + //OK, we have a valid time
                "Date: Now" + Environment.NewLine + //current datetime
                "Server: AchronWeb/0.0.1 (DocileDanny)" + Environment.NewLine + //server info
                "X-Powered-By: C#/" + Environment.Version.ToString() + Environment.NewLine + //php info
                "Set-Cookie: PHPSESSID=" + client.SESSID + "; path=/" + Environment.NewLine + //set the sessid cookie
                "Expires: Thu, 19 Nov 1981 08:52:00 GMT" + Environment.NewLine + //when the cookie expires
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
