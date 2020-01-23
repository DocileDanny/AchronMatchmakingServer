using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Networking;
using Util;

namespace AchronWeb
{
    class Program
    {
        static void Main(string[] args)
        {
            Terminal.WriteTitle( new string[] { "Chronocloned", "The Achron Matchmaking Service", "v0.2", "By DocileDanny", "Special Thanks To: Lumarin" } );

            try
            {
                WebServer wsf = new WebServer(80, false, false);

                System.Threading.Thread checkThread = new System.Threading.Thread(features.consts.TimeoutChecker);
                checkThread.Start();

                Terminal.WriteLine(TerminalState.OK, "Server", "Service Started.");
            }
            catch (Exception ex)
            {
                Terminal.WriteLine(TerminalState.FAIL, "Server", "Unable to start the service: " + ex.ToString());
                Console.ReadLine();
                return;
            }

            while (true)
            {
                System.Threading.Thread.Sleep(0);
            }
        }
    }
}
