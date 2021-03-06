﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AchronWeb.features
{
    /// <summary>
    /// Information about a connected client.
    /// </summary>
    public class achronClient
    {
        /// <summary>
        /// The username for this client.
        /// </summary>
        public string username;

        /// <summary>
        /// The unique identifer this client is using.
        /// </summary>
        public string SESSID;

        /// <summary>
        /// When did we last hear from this client?
        /// </summary>
        public long lastSeen;

        /// <summary>
        /// When did we first hear from this client?
        /// </summary>
        public long firstSeen;
    }
}