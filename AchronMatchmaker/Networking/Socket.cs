using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Net.Sockets;
using Hardware.Networking;
using Util;

namespace Hardware.Server.Networking
{
    /// <summary>
    /// A network connection
    /// </summary>
    public class glSocket : GameSocket
    {
        /// <summary>
        /// The connection itself.
        /// </summary>
        TcpClient socket;

        /// <summary>
        /// The byte buffer.
        /// </summary>
        ArrayList byteBuffer;

        /// <summary>
        /// Packet queue
        /// </summary>
        Queue<PacketData> packetQueue;

        /// <summary>
        /// Get the IP address of this socket.
        /// </summary>
        public string IPAddress
        {
            get
            {
                return ((System.Net.IPEndPoint)socket.Client.RemoteEndPoint).Address.ToString();
            }
        }

        /// <summary>
        /// Is this gamesocket currently connected?
        /// </summary>
        public bool isConnected
        {
            get
            {
                if (socket == null)
                {
                    return false;
                }
                else
                {
                    return socket.Connected;
                }
            }
        }

        /// <summary>
        /// When was the last message sent or recieved.
        /// </summary>
        long lastMsg;

        /// <summary>
        /// attempt to connect to the specified server.
        /// </summary>
        /// <param name="server"></param>
        /// <param name="port"></param>
        public glSocket(string server, int port)
        {
            socket = new TcpClient();
            byteBuffer = new ArrayList();
            packetQueue = new Queue<PacketData>();
            socket.Connect(server, port);
            lastMsg = Terminal.GetTime();

            do
            {
                System.Threading.Thread.Sleep(1);
            }
            while (Terminal.GetTime() - lastMsg < 3000 && !isConnected);

            lastMsg = Terminal.GetTime();

            //if we are connected, start running this socket
            if (isConnected)
            {
                System.Threading.Thread t = new System.Threading.Thread(Run);
                t.Start();
            }
            else
            {
                Disconnect();
            }
        }

        /// <summary>
        /// Create a glSocket from the specified tcp socket.
        /// </summary>
        /// <param name="socket"></param>
        public glSocket(TcpClient InSocket)
        {
            lastMsg = Terminal.GetTime();
            byteBuffer = new ArrayList();
            packetQueue = new Queue<PacketData>();

            this.socket = InSocket;
            System.Threading.Thread t = new System.Threading.Thread(Run);
            t.Start();
        }

        /// <summary>
        /// Run this socket
        /// </summary>
        void Run()
        {
            try
            {
                while (isConnected)
                {
                    System.Threading.Thread.Sleep(1);

                    //there's data available.
                    if (socket.Available > 0)
                    {
                        lock (socket)
                        {
                            NetworkStream ns = socket.GetStream();

                            byte[] bData = new byte[socket.Available];
                            ns.Read(bData, 0, socket.Available);

                            for (int i = 0; i < bData.Length; i++)
                            {
                                byteBuffer.Add(bData[i]);
                            }

                            lastMsg = Terminal.GetTime();
                        }
                    }

                    //there has been no messages for 3 seconds, send a ping
                    if (Terminal.GetTime() - lastMsg > 3000)
                    {
                        //ping
                        PacketData pd = new PacketData(0xFF, 0xFF);
                        SendData(pd);
                    }

                    //we know the next packet length
                    if (byteBuffer.Count > 8)
                    {
                        byte[] rawLong = new byte[]
                        {
                            (byte)byteBuffer[0], (byte)byteBuffer[1],
                            (byte)byteBuffer[2], (byte)byteBuffer[3],
                            (byte)byteBuffer[4], (byte)byteBuffer[5],
                            (byte)byteBuffer[6], (byte)byteBuffer[7],
                        };

                        if (BitConverter.IsLittleEndian)
                        {
                            Array.Reverse(rawLong);
                        }

                        long packLength = BitConverter.ToInt64(rawLong, 0);

                        if (byteBuffer.Count > 8 + packLength)
                        {
                            byte[] packData = new byte[packLength];
                            for (int i = 8; i < 8 + packLength; i++)
                            {
                                packData[i - 8] = (byte)byteBuffer[i];
                            }
                            PacketData packet = new PacketData(packData);
                            if (packet.packetIDA == 0xFF && packet.packetIDB == 0xFF)
                            {
                                /*ping*/
                            }
                            else
                            {
                                packetQueue.Enqueue(packet);
                            }

                            for (int i = 0; i < 8 + packLength; i++)
                            {
                                byteBuffer.RemoveAt(0);
                            }
                        }
                    }

                }

            }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); }
            finally
            {
                Disconnect();
            }
        }

        /// <summary>
        /// Close this socket.
        /// </summary>
        public void Disconnect()
        {
            try
            {
                socket.Close();
            }
            catch (Exception ex) { }
            finally { socket = null; }
        }

        public PacketData GetData()
        {
            if (packetQueue.Count == 0) { return null; }
            else
            {
                return packetQueue.Dequeue();
            }
        }

        /// <summary>
        /// Send the provided packet data
        /// </summary>
        /// <param name="packet"></param>
        public void SendData(PacketData packet)
        {
            lock (socket)
            {
                NetworkStream ns = socket.GetStream();

                byte[] rawData = packet.ToByte();
                byte[] longData = BitConverter.GetBytes(rawData.LongLength);

                if (BitConverter.IsLittleEndian)
                {
                    Array.Reverse(longData);
                }

                byte[] finalData = new byte[rawData.Length + longData.Length];
                Array.Copy(longData, 0, finalData, 0, longData.Length);
                Array.Copy(rawData, 0, finalData, longData.Length, rawData.Length);

                ns.Write(finalData, 0, finalData.Length);
                lastMsg = Terminal.GetTime();
            }
        }
    }
}