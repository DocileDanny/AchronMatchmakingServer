using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Networking;

namespace AchronWeb
{
    class Program
    {
        static void Main(string[] args)
        {
            WebServer wsf = new WebServer(80,false,false);

            System.Threading.Thread checkThread = new System.Threading.Thread(features.consts.TimeoutChecker);
            checkThread.Start();

            while (true)
            {
                System.Threading.Thread.Sleep(0);
            }
        }
    }
}
