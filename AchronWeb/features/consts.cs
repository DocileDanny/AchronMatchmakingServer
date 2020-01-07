using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AchronWeb.features
{
    public static class consts
    {
        /// <summary>
        /// A static random number generator.
        /// </summary>
        public static Random rdm = new Random();

        /// <summary>
        /// A list of clients
        /// </summary>
        public static Dictionary<string, achronClient> clientList = new Dictionary<string, achronClient>();

        /// <summary>
        /// A list of active games.
        /// </summary>
        public static Dictionary<long, achronGame> gameList = new Dictionary<long, achronGame>();

        /// <summary>
        /// The number of games.
        /// </summary>
        public static long gameCount = 0;

        /// <summary>
        /// Get the user with the specified hash.
        /// </summary>
        public static achronClient getUser(string hash)
        {
            lock (clientList)
            {
                foreach (KeyValuePair<string, achronClient> client in clientList)
                {
                    //probably the same user, tbh this is not done "correctly", but who cares really?
                    //the odds of the hash having the exact same string of characters and not being the same user is tiny.
                    //and for a userbase this small, who cares?
                    if (hash.Contains(client.Value.SESSID))
                    {
                        client.Value.lastSeen = GetTime();
                        return client.Value;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Time out timer.
        /// </summary>
        public static void TimeoutChecker()
        {
            //do this forever
            while (true)
            {
                System.Threading.Thread.Sleep(10000);

                lock (clientList) //time out clients
                {
                    Queue<string> deadUsers = new Queue<string>();

                    foreach (KeyValuePair<string, achronClient> client in clientList)
                    {
                        if (GetTime() - client.Value.lastSeen > (600000*3))
                        {
                            //time out this user after 30 mins
                            deadUsers.Enqueue(client.Key);
                            Console.WriteLine(client.Value.username + " (" + client.Value.SESSID + ") timed out!");
                        }
                    }

                    while (deadUsers.Count > 0)
                    {
                        string hash = deadUsers.Dequeue();
                        clientList.Remove(hash);
                    }
                }

                lock (gameList) //time out games
                {
                    Queue<long> deadGame = new Queue<long>();

                    foreach (KeyValuePair<long, achronGame> game in gameList)
                    {
                        if (GetTime() - game.Value.lastUpdate > (600000 * 3))
                        {
                            //time out this game after 30 mins
                            deadGame.Enqueue(game.Key);
                            Console.WriteLine("Game " + game.Value.gameID + " timed out!");
                        }
                    }

                    while (deadGame.Count > 0)
                    {
                        long id = deadGame.Dequeue();
                        gameList.Remove(id);
                    }
                }
            }
            //600000
        }

        /// <summary>
        /// Get the current UTC time.
        /// </summary>
        /// <returns></returns>
        public static long GetTime()
        {
            return DateTime.UtcNow.Ticks / 10000;
        }
    }
}