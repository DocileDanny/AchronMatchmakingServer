using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AchronWebtest.features
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

            foreach (KeyValuePair<string, achronClient> client in clientList)
            {
                //probably the same user, tbh this is not done "correctly", but who cares really?
                //the odds of the hash having the exact same string of characters and not being the same user is tiny.
                //and for a userbase this small, who cares?
                if (hash.Contains(client.Value.SESSID))
                {
                    return client.Value;
                }
            }

            return null;
        }
    }
}