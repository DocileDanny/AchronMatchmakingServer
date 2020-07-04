using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Net;
using System.Text;
using System.Windows.Forms;

using AchronWeb;
using Networking;

using Hardware.Networking;
using Hardware.Server.Networking;

using System.Json;

using Mono.Nat;

namespace AchronMatchmaker
{
    public partial class achronForm : Form
    {
        WebServer wsf;
        System.Threading.Thread checkThread;

        /// <summary>
        /// This server is used to sync listed games.
        /// </summary>
        glServer SyncServer;

        /// <summary>
        // This is a list of connected clients.
        /// </summary>
        List<GameSocket> clientList = new List<GameSocket>();

        /// <summary>
        /// A normal every day sync client.
        /// </summary>
        glSocket SyncClient;

        public achronForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Load the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void achronForm_Load(object sender, EventArgs e)
        {
            try
            {
                btnDetect_Click(sender, e);
                wsf = new WebServer(80, false, false);
                checkThread = new System.Threading.Thread(AchronWeb.features.consts.TimeoutChecker);
                checkThread.Start();

                Util.Terminal.WriteLine(Util.Terminal.partition());
                Util.Terminal.WriteLine("Chronocloned");
                Util.Terminal.WriteLine("The Achron Matchmaking Service");
                Util.Terminal.WriteLine("v0.2.1 [b]");
                Util.Terminal.WriteLine("By DocileDanny");
                Util.Terminal.WriteLine("Special Thanks To: Lumarin");
                Util.Terminal.WriteLine(Util.Terminal.partition());

                Util.Terminal.WriteLine(Util.TerminalState.OK, "Server", "Service Started. [ http://localhost ]");
                Util.Terminal.WriteLine("Please select either join or host before playing.");
                Util.Terminal.WriteLine("Please ensure all players are connected to the matchmaker before opening Achron.");
            }
            catch (Exception ex)
            {
                Util.Terminal.WriteLine(Util.TerminalState.FAIL, "Server", "Unable to start the service: " + ex.ToString());
                btnDetect.Enabled = false;
                btnJoinServer.Enabled = true;
                btnHost.Enabled = false;
            }
        }

        /// <summary>
        /// Detect our external IP Address.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDetect_Click(object sender, EventArgs e)
        {
            try
            {
                WebClient client = new WebClient();
                string downloadString = client.DownloadString("http://bot.whatismyipaddress.com");
                txtExternalIP.Text = downloadString;
            }
            catch
            {
                txtExternalIP.Text = "unknown";
            }
        }

        /// <summary>
        /// Check for external updates.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void outputTimer_Tick(object sender, EventArgs e)
        {
            //Show console messages.
            while (Util.Terminal.output.Count != 0)
            {
                txtConsole.AppendText(Util.Terminal.output.Dequeue());
            }

            if (SyncServer != null)
            {
                //check for new clients.
                {
                    GameSocket gs = SyncServer.getSocket();
                    if (gs != null)
                    {
                        Util.Terminal.WriteLine("New Sync Client Connected [" + ((glSocket)gs).IPAddress + "]");
                        clientList.Add(gs);
                    }
                }

                //check for DC'd clients.
                Queue<GameSocket> toDestroy = new Queue<GameSocket>();
                foreach (GameSocket gs in clientList)
                {
                    if (!gs.isConnected)
                    {
                        toDestroy.Enqueue(gs);
                    }
                }

                //remove DC'd clients.
                while (toDestroy.Count != 0)
                {
                    clientList.Remove(toDestroy.Dequeue());
                }

                //Do we have any games to collect?
                lock (AchronWeb.features.consts.gameSendList)
                {
                    foreach (GameSocket gs in clientList)
                    {
                        if (gs.isConnected)
                        {
                            PacketData data = gs.GetData();
                            if (data != null)
                            {
                                data.beginRead();
                                AchronWeb.features.achronGame remoteGame = jsonToGame(JsonValue.Parse(data.readString()));

                                if (remoteGame.host == "127.0.0.1" || remoteGame.host == "::1")
                                {
                                    remoteGame.host = ((glSocket)gs).IPAddress;
                                }

                                //send the game to other clients too.
                                foreach (GameSocket gsN in clientList)
                                {
                                    if (gsN == gs) { continue; }
                                    else
                                    {
                                        PacketData outData = new PacketData(0x01, 0x02);
                                        outData.writeString(gameToJson(remoteGame).ToString());
                                        gsN.SendData(outData);
                                    }
                                }

                                lock (AchronWeb.features.consts.gameList)
                                {
                                    AchronWeb.features.consts.gameCount++;
                                    remoteGame.lastUpdate = AchronWeb.features.consts.GetTime();
                                    remoteGame.gameID = AchronWeb.features.consts.gameCount;
                                    AchronWeb.features.consts.gameList.Add(remoteGame.gameID, remoteGame);
                                }

                                Util.Terminal.WriteLine("New Remote Game: " + remoteGame.gameName + " / " + remoteGame.level + " / " + remoteGame.host);
                            }
                        }
                    }
                }

                //Do we got any games to send?
                lock (AchronWeb.features.consts.gameSendList)
                {
                    if (AchronWeb.features.consts.gameSendList.Count != 0)
                    {
                        JsonValue toSend = gameToJson(AchronWeb.features.consts.gameSendList.Dequeue());
                        foreach (GameSocket gs in clientList)
                        {
                            if (gs.isConnected)
                            {
                                PacketData pd = new PacketData(0x01, 0x02);
                                pd.writeString(toSend.ToString());
                                gs.SendData(pd);
                            }
                        }
                    }
                }

                txtServerStatus.Text = "Clients: " + clientList.Count;
            }

            if (SyncClient != null)
            {
                if (!SyncClient.isConnected)
                {
                    txtClientStatus.Text = "Connection Lost";
                    SyncClient = null;
                    btnHost.Enabled = true;
                    btnJoinServer.Enabled = true;
                }
                else
                {
                    PacketData data = SyncClient.GetData();
                    if (data != null) //we have a packet.
                    {
                        data.beginRead();
                        AchronWeb.features.achronGame remoteGame = jsonToGame(JsonValue.Parse(data.readString()));

                        if (remoteGame.host == "127.0.0.1" || remoteGame.host == "::1")
                        {
                            remoteGame.host = SyncClient.IPAddress;
                        }

                        lock (AchronWeb.features.consts.gameList)
                        {
                            AchronWeb.features.consts.gameCount++;
                            remoteGame.lastUpdate = AchronWeb.features.consts.GetTime();
                            remoteGame.gameID = AchronWeb.features.consts.gameCount;
                            AchronWeb.features.consts.gameList.Add(remoteGame.gameID, remoteGame);
                        }

                        Util.Terminal.WriteLine("New Remote Game: " + remoteGame.gameName + " / " + remoteGame.level + " / " + remoteGame.host);
                    }

                    //Do we gots a game to send?
                    if (AchronWeb.features.consts.gameSendList.Count != 0)
                    {
                        JsonValue toSend = gameToJson(AchronWeb.features.consts.gameSendList.Dequeue());

                        if (SyncClient.isConnected)
                        {
                            PacketData pd = new PacketData(0x01, 0x02);
                            pd.writeString(toSend.ToString());
                            SyncClient.SendData(pd);
                        }
                    }

                    txtClientStatus.Text = "Connected";
                }
            }
        }

        /// <summary>
        /// Attempt to host a server.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnHost_Click(object sender, EventArgs e)
        {
            try
            {
                btnHost.Enabled = false;
                btnJoinServer.Enabled = false;
                upnpEnabled.Enabled = false;
                txtHostPort.Enabled = false;

                int temp_port;
                if (Int32.TryParse(txtHostPort.Text, out temp_port))
                {
                    Util.Terminal.WriteLine(Util.TerminalState.OK, "SYNC", "Sync Server Started On Port " + temp_port + " - Awaiting Clients.");
                    SyncServer = new glServer(temp_port, false);

                    // uPnP
                    NatUtility.DeviceFound += NatUtility_DeviceFound;
                    NatUtility.StartDiscovery();
                }
                else
                {
                    Util.Terminal.WriteLine(Util.TerminalState.FAIL, "SYNC", "Invalid port [" + txtHostPort.Text + "] - please only enter numbers into this box.");
                    btnHost.Enabled = true;
                    btnJoinServer.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                Util.Terminal.WriteLine(Util.TerminalState.FAIL, "SYNC", "Unable to start sync server - " + ex.ToString());
            }
        }

        /// <summary>
        /// Deal with a found NAT device.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NatUtility_DeviceFound(object sender, DeviceEventArgs e)
        {
            // This is the upnp enabled router
            INatDevice device = e.Device;

            if (upnpEnabled.Checked)
            {
                // Create a mapping to forward the external port
                device.CreatePortMap(new Mapping(Protocol.Tcp, Int32.Parse(txtHostPort.Text), Int32.Parse(txtHostPort.Text)));
            }
            else
            {
                //foreach (Mapping mp in device.GetAllMappings())
                //    device.DeletePortMap(mp);
            }

            /*
            // Retrieve the details for the port map for external port 3000
            Mapping m = device.GetSpecificMapping(Protocol.Tcp, Int32.Parse(txtHostPort.Text));

            // Get all the port mappings on the device and delete them
            foreach (Mapping mp in device.GetAllMappings())
                device.DeletePortMap(mp);

            // Get the external IP address
            IPAddress externalIP = device.GetExternalIP();
            */
        }

        /// <summary>
        /// Attempt to join a server.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnJoinServer_Click(object sender, EventArgs e)
        {
            try
            {
                btnHost.Enabled = false;
                btnJoinServer.Enabled = false;

                int temp_port;
                if (Int32.TryParse(txtJoinPort.Text, out temp_port))
                {
                    Util.Terminal.WriteLine(Util.TerminalState.OK, "SYNC", "Sync client connecting to " + txtIP.Text + " : " + temp_port);
                    SyncClient = new glSocket(txtIP.Text, temp_port);
                }
                else
                {
                    Util.Terminal.WriteLine(Util.TerminalState.FAIL, "SYNC", "Invalid port [" + txtHostPort.Text + "] - please only enter numbers into this box.");
                    btnHost.Enabled = true;
                    btnJoinServer.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                Util.Terminal.WriteLine(Util.TerminalState.FAIL, "SYNC", "Unable to connect to sync server - " + ex.ToString());
            }
        }

        /// <summary>
        /// Convert an achron game into JSON.
        /// </summary>
        /// <returns></returns>
        public JsonValue gameToJson(AchronWeb.features.achronGame game)
        {
            JsonValue jv = JsonValue.Parse("{}");
            jv["playerCount"] = game.currentPlayers;
            jv["gameID"] = game.gameID;
            jv["gameName"] = game.gameName;
            jv["hostName"] = game.gamePlayerHost;
            jv["hostIP"] = game.host;
            jv["lastUpdate"] = game.lastUpdate;
            jv["level"] = game.level;
            jv["maxPlayers"] = game.maxPlayers;
            jv["ownerSESSID"] = game.ownerSESSID;
            jv["portA"] = game.portA;
            jv["portB"] = game.portB;
            jv["progress"] = game.Progress;
            return jv;
        }

        /// <summary>
        /// Convert an achron game into JSON.
        /// </summary>
        /// <returns></returns>
        public AchronWeb.features.achronGame jsonToGame(JsonValue jv)
        {
            AchronWeb.features.achronGame game = new AchronWeb.features.achronGame();
            game.currentPlayers = jv["playerCount"];
            game.gameID = jv["gameID"];
            game.gameName = jv["gameName"];
            game.gamePlayerHost = jv["hostName"];
            game.host = jv["hostIP"];
            game.lastUpdate = jv["lastUpdate"];
            game.level = jv["level"];
            game.maxPlayers = jv["maxPlayers"];
            game.ownerSESSID = jv["ownerSESSID"];
            game.portA = jv["portA"];
            game.portB = jv["portB"];
            game.Progress = jv["progress"];
            return game;
        }

        /// <summary>
        /// Form is closing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void achronForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //End the application.
            Environment.Exit(0);
        }

        private void btnDebug_Click(object sender, EventArgs e)
        {
            txtConsole.Visible = !txtConsole.Visible;
        }

        private void btnPatch_Click(object sender, EventArgs e)
        {
            //A file was selected.
            if (ofdClient.ShowDialog() == DialogResult.OK)
            {
                //could be correct..
                if (ofdClient.FileName.ToLower().Contains(".exe") && ofdClient.FileName.ToLower().Contains("achron") )
                {
                    string name = ofdClient.FileName + ".backup-" + Util.Terminal.GetTime();
                    System.IO.File.Move(ofdClient.FileName, name);

                    System.IO.Stream clientStream = System.IO.File.Open(name, System.IO.FileMode.Open);
                    byte[] data = ToByteArray(clientStream);

                    byte[] target = new byte[] { 0x77, 0x77, 0x77, 0x2E,
                                                 0x61, 0x63, 0x68, 0x72,
                                                 0x6F, 0x6E, 0x67, 0x61,
                                                 0x6D, 0x65, 0x2E, 0x63,
                                                 0x6F, 0x6D };

                    byte[] result = new byte[] { 0x6C, 0x6F, 0x63, 0x61,
                                                 0x6C, 0x68, 0x6F, 0x73, 
                                                 0x74, 0x00, 0x00, 0x00,
                                                 0x00, 0x00, 0x00, 0x00,
                                                 0x00, 0x00 };
                    int found = -1;
                    int replaced = 0;
                    do
                    {
                        found = SearchBytes(data, target);

                        if (found != -1)
                        {
                            //replace the dest with the target
                            for (int x = 0; x < target.GetUpperBound(0) + 1; x++)
                            {
                                data[found + x] = result[x];
                            }
                            replaced++;
                        }

                    } while (found != -1);

                    if (replaced != 0)
                    {
                        System.IO.File.WriteAllBytes(ofdClient.FileName, data);
                        MessageBox.Show("Client patched! (" + replaced + ")");
                    }
                    else
                    {
                        System.IO.File.WriteAllBytes(ofdClient.FileName, data);
                        MessageBox.Show("Client not patched! (Already patched? Wrong file?)");
                    }
                }
                else
                {
                    MessageBox.Show("Please select Achron.exe!");
                }
            }
        }

        static int SearchBytes(byte[] haystack, byte[] needle)
        {
            var len = needle.Length;
            var limit = haystack.Length - len;
            for (var i = 0; i <= limit; i++)
            {
                var k = 0;
                for (; k < len; k++)
                {
                    if (needle[k] != haystack[i + k]) break;
                }
                if (k == len) return i;
            }
            return -1;
        }

        public static byte[] ToByteArray(System.IO.Stream stream)
        {
            stream.Position = 0;
            byte[] buffer = new byte[stream.Length];
            for (int totalBytesCopied = 0; totalBytesCopied < stream.Length;)
                totalBytesCopied += stream.Read(buffer, totalBytesCopied, Convert.ToInt32(stream.Length) - totalBytesCopied);
            return buffer;
        }
    }
}
