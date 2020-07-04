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
    public static class registerPacketALegacy
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="xO02O">username</param>
        /// <param name="x7c37">steamID???</param>
        public static byte[] Handle(string xO02O, string xO02a)
        {
            //Not sure what xO02a is for at this time.

            //create a new client to represent this user.
            achronClient client = new achronClient();
            client.username = xO02O;
            client.firstSeen = consts.GetTime();
            client.lastSeen = client.firstSeen;

            //generate a session ID
            using (SHA256 sha256Hash = SHA256.Create())
            {
                string hash = GetHash(sha256Hash, client.username + client.firstSeen).Substring(0, 32);
                client.SESSID = hash;
            }

            string content =
                client.SESSID + @"OK"; //the client version

            consts.clientList.Add(client.SESSID, client);

            string reply =
                "HTTP/1.1 200 OK" + "\r\n" + //OK, we have a valid time
                "Date: Now" + "\r\n" + //current datetime
                "Server: AchronWeb/0.0.1 (DocileDanny)" + "\r\n" + //server info
                "X-Powered-By: C#/" + Environment.Version.ToString() + "\r\n" + //php info
                "Set-Cookie: PHPSESSID=" + client.SESSID + "; path=/" + "\r\n" + //set the sessid cookie
                "Expires: Thu, 19 Nov 1981 08:52:00 GMT" + "\r\n" + //when the cookie expires
                "Cache-Control: no-store, no-cache, must-revalidate, post-check=0, pre-check=0" + "\r\n" + //various info about caching.
                "Pragma: no-cache" + "\r\n" + //pragma values
                "Content-Length: " + content.Length.ToString() + "\r\n" + //how long is the content
                "Content-Type: text/plain; charset=UTF-8" + "\r\n" + "\r\n" + //what is the content
                content + "\r\n"; //the content itself.

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
