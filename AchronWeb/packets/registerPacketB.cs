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
    public static class registerPacketB
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="xO02O">username</param>
        /// <param name="x7c37">steamID???</param>
        public static byte[] Handle(string xO02O, string xO02a, string xO040)
        {
            //client verifying it registered correctly I guess.
            //xO02O appears to be the username again.
            //xO02a appears to be a duplicate of the username?
            //xO040 is probably to do with the steam verification process.


            string content = "OK "; //OK then.

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

            /*
HTTP/1.1 200 OK
Date: Mon, 06 Jan 2020 15:41:13 GMT
Server: Apache/2.4.6 (CentOS) OpenSSL/1.0.2k-fips PHP/5.4.16
X-Powered-By: PHP/5.4.16
Set-Cookie: PHPSESSID=ejqnli5gbc52d5dm6k60893510; path=/
Expires: Thu, 19 Nov 1981 08:52:00 GMT
Cache-Control: no-store, no-cache, must-revalidate, post-check=0, pre-check=0
Pragma: no-cache
Content-Length: 3
Content-Type: text/plain; charset=UTF-8

OK
             */

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
