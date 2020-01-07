using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AchronWebtest.features
{
    /// <summary>
    /// Information about a connected client.
    /// </summary>
    public class achronGame
    {
        /// <summary>
        /// The uniqueID of this game.
        /// </summary>
        public long gameID;

        /// <summary>
        /// When was this game last updated?
        /// </summary>
        public long lastUpdate;

        /// <summary>
        /// Number of players in this game.
        /// </summary>
        public int currentPlayers;

        /// <summary>
        /// Max players in this game.
        /// </summary>
        public int maxPlayers;

        /// <summary>
        /// Current game level.
        /// </summary>
        public string level;

        /// <summary>
        /// Host address
        /// </summary>
        public string host;

        /// <summary>
        /// Name of this game.
        /// </summary>
        public string gameName;

        /// <summary>
        /// The name of the person hosting this game.
        /// </summary>
        public string gamePlayerHost;

        /// <summary>
        /// The second port this server is using.
        /// </summary>
        public long portA;

        /// <summary>
        /// The first port this server is using.
        /// </summary>
        public long portB;

        /// <summary>
        /// 0 = game waiting, 1 = game in progress.
        /// </summary>
        public int Progress = 0;
    }
}