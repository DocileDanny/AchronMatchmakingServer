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
    public static class okPacket
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="xO02O">username</param>
        /// <param name="x7c37">steamID???</param>
        public static byte[] Handle()
        {
            //client verifying it registered correctly I guess.
            //xO02O appears to be the username again.
            //xO02a appears to be a duplicate of the username?
            //xO040 is probably to do with the steam verification process.

            string reply =
                "HTTP/1.1 200 OK" + "\r\n" + //OK, we have a valid time
                "Date: Now" + "\r\n" + //current datetime
                "Server: AchronWeb/0.0.1 (DocileDanny)" + "\r\n" + //server info
                "X-Powered-By: C#/" + Environment.Version.ToString() + "\r\n" + //php info
                                                                                             //"Set-Cookie: PHPSESSID=" + client.SESSID + "; path=/" + "\r\n" + //set the sessid cookie
                                                                                             //"Expires: Thu, 19 Nov 1981 08:52:00 GMT" + "\r\n" + //when the cookie expires
                "Cache-Control: no-store, no-cache, must-revalidate, post-check=0, pre-check=0" + "\r\n" + //various info about caching.
                "Pragma: no-cache" + "\r\n" + //pragma values
                "Content-Length: 0" + "\r\n" + //how long is the content
                "Content-Type: text/plain; charset=UTF-8" + "\r\n" + "\r\n"; //what is the content

            return UTF8Encoding.UTF8.GetBytes(reply);
        }
    }
}
