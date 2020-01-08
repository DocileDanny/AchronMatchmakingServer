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
                "HTTP/1.1 200 OK" + Environment.NewLine + //OK, we have a valid time
                "Date: Now" + Environment.NewLine + //current datetime
                "Server: AchronWeb/0.0.1 (DocileDanny)" + Environment.NewLine + //server info
                "X-Powered-By: C#/" + Environment.Version.ToString() + Environment.NewLine + //php info
                                                                                             //"Set-Cookie: PHPSESSID=" + client.SESSID + "; path=/" + Environment.NewLine + //set the sessid cookie
                                                                                             //"Expires: Thu, 19 Nov 1981 08:52:00 GMT" + Environment.NewLine + //when the cookie expires
                "Cache-Control: no-store, no-cache, must-revalidate, post-check=0, pre-check=0" + Environment.NewLine + //various info about caching.
                "Pragma: no-cache" + Environment.NewLine + //pragma values
                "Content-Length: 0" + Environment.NewLine + //how long is the content
                "Content-Type: text/plain; charset=UTF-8" + Environment.NewLine + Environment.NewLine; //what is the content

            return UTF8Encoding.UTF8.GetBytes(reply);
        }
    }
}
