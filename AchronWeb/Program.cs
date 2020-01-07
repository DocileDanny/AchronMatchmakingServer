using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Networking;

namespace AchronWebtest
{
    class Program
    {
        static void Main(string[] args)
        {
            WebServer wsf = new WebServer(80,false,false);

            /*
            features.achronGame game = new features.achronGame();
            game.currentPlayers = 0;
            game.maxPlayers = 0;
            game.host = "127.0.0.1";
            game.portA = 7014;
            game.portB = 7013;
            game.level = @"./levels/MULTI (2) - Rooftop Showdown v1.3.0.tsc";
            game.gameID = 1;
            game.gameName = "Hello World!";
            game.gamePlayerHost = "My";

            features.consts.gameList.Add(game.gameID, game);

            game = new features.achronGame();
            game.currentPlayers = 0;
            game.maxPlayers = 0;
            game.host = "127.0.0.1";
            game.portA = 7014;
            game.portB = 7013;
            game.level = @"./levels/MULTI (2) - Rooftop Showdown v1.3.0.tsc";
            game.gameID = 2;
            game.gameName = "This is a test.";
            game.gamePlayerHost = "Names";
            game.Progress = 1;

            features.consts.gameList.Add(game.gameID, game);

            game = new features.achronGame();
            game.currentPlayers = 0;
            game.maxPlayers = 0;
            game.host = "127.0.0.1";
            game.portA = 7014;
            game.portB = 7013;
            game.level = @"./levels/MULTI (2) - Rooftop Showdown v1.3.0.tsc";
            game.gameID = 3;
            game.gameName = "See this? Test worked!";
            game.gamePlayerHost = "Tom";

            features.consts.gameList.Add(game.gameID, game);
            */

            while (true)
            {
                System.Threading.Thread.Sleep(0);
            }
        }
    }
}
