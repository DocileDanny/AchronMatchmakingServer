using System;
using System.Collections.Generic;
using System.Text;

namespace Hardware.Networking
{
    public enum dataType : byte
    {
        nul = 0x00,
        str = 0x01,
        int16 = 0x02,
        int32 = 0x03,
        int64 = 0x04,
        flo32 = 0x05
    }

    public class PacketData
    {
        /// <summary>
        /// The first identifier  (class)
        /// </summary>
        public byte packetIDA
        {
            get;
            private set;
        }

        /// <summary>
        /// The second identifier (method)
        /// </summary>
        public byte packetIDB
        {
            get;
            private set;
        }

        /// <summary>
        /// The raw data itself
        /// </summary>
        byte[] data;

        /// <summary>
        /// The current read position
        /// </summary>
        int ReadPos = 0;

        /// <summary>
        /// Create an empty packet
        /// </summary>
        /// <param name="packetIDA">Packet identifier.</param>
        /// <param name="packetIDB">Packet identifier.</param>
        public PacketData(byte packetIDA, byte packetIDB)
        {
            this.packetIDA = packetIDA;
            this.packetIDB = packetIDB;
            data = new byte[] { packetIDA, packetIDB };
        }

        public PacketData(byte[] dat)
        {
            data = dat;
            this.packetIDA = dat[0];
            this.packetIDB = dat[1];
        }

        /// <summary>
        /// Write the next string value
        /// </summary>
        /// <param name="data">Data.</param>
        public void writeString(string payload)
        {
            //we have the raw data
            byte[] utf8data = Encoding.UTF8.GetBytes(payload);

            //how long is this data (int = 4 bytes)
            byte[] dataLength = BitConverter.GetBytes(utf8data.Length);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(dataLength);
            }

            //create the complete data packet
            byte[] totalData = new byte[utf8data.Length + dataLength.Length];
            Array.Copy(dataLength, totalData, dataLength.Length);
            Array.Copy(utf8data, 0, totalData, dataLength.Length, utf8data.Length);

            //write the bytes to the packet
            writeBytes((byte)dataType.str, totalData);
        }

        /// <summary>
        /// write an short to the packet
        /// </summary>
        /// <param name="payload">Payload.</param>
        public void writeShort(short payload)
        {
            byte[] shortData = BitConverter.GetBytes(payload);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(shortData);
            }

            //write the bytes to the packet
            writeBytes((byte)dataType.int16, shortData);
        }

        /// <summary>
        /// read int from the packet
        /// </summary>
        /// <param name="payload">Payload.</param>
        public short readShort()
        {
            if (nextType() != dataType.int16)
            {
                throw new InvalidOperationException("The next value is not a short.");
            }
            else
            {
                byte[] val = new byte[2];
                Array.Copy(data, ReadPos + 1, val, 0, 2);

                if (BitConverter.IsLittleEndian)
                {
                    Array.Reverse(val);
                }

                short value = BitConverter.ToInt16(val, 0);
                ReadPos += 3;
                return value;
            }
        }

        /// <summary>
        /// write an int to the packet
        /// </summary>
        /// <param name="payload">Payload.</param>
        public void writeInt(int payload)
        {
            byte[] intData = BitConverter.GetBytes(payload);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(intData);
            }

            //write the bytes to the packet
            writeBytes((byte)dataType.int32, intData);
        }

        /// <summary>
        /// read int from the packet
        /// </summary>
        /// <param name="payload">Payload.</param>
        public int readInt()
        {
            if (nextType() != dataType.int32)
            {
                throw new InvalidOperationException("The next value is not a int.");
            }
            else
            {
                byte[] val = new byte[4];
                Array.Copy(data, ReadPos + 1, val, 0, 4);

                if (BitConverter.IsLittleEndian)
                {
                    Array.Reverse(val);
                }

                int value = BitConverter.ToInt32(val, 0);
                ReadPos += 5;
                return value;
            }
        }

        /// <summary>
        /// write an long to the packet
        /// </summary>
        /// <param name="payload">Payload.</param>
        public void writeLong(long payload)
        {
            byte[] longData = BitConverter.GetBytes(payload);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(longData);
            }

            //write the bytes to the packet
            writeBytes((byte)dataType.int64, longData);
        }

        /// <summary>
        /// write an long to the packet
        /// </summary>
        /// <param name="payload">Payload.</param>
        public void writeFloat(float payload)
        {
            byte[] floatData = BitConverter.GetBytes(((double)payload));
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(floatData);
            }

            //write the bytes to the packet
            writeBytes((byte)dataType.flo32, floatData);
        }

        /// <summary>
        /// read long from the packet
        /// </summary>
        /// <param name="payload">Payload.</param>
        public long readLong()
        {
            if (nextType() != dataType.int64)
            {
                throw new InvalidOperationException("The next value is not a long.");
            }
            else
            {
                byte[] val = new byte[8];
                Array.Copy(data, ReadPos + 1, val, 0, 8);

                if (BitConverter.IsLittleEndian)
                {
                    Array.Reverse(val);
                }

                long value = BitConverter.ToInt64(val, 0);
                ReadPos += 9;
                return value;
            }
        }

        /// <summary>
        /// read float from the packet
        /// </summary>
        /// <param name="payload">Payload.</param>
        public float readFloat()
        {
            if (nextType() != dataType.flo32)
            {
                throw new InvalidOperationException("The next value is not a float.");
            }
            else
            {
                byte[] val = new byte[8];
                Array.Copy(data, ReadPos + 1, val, 0, 8);

                if (BitConverter.IsLittleEndian)
                {
                    Array.Reverse(val);
                }

                double value = BitConverter.ToDouble(val, 0);
                ReadPos += 9;
                return (float)value;
            }
        }

        /// <summary>
        /// Reads the next string.
        /// </summary>
        /// <returns>The string.</returns>
        public string readString()
        {
            if (nextType() != dataType.str)
            {
                throw new InvalidOperationException("The next value is not a string.");
            }
            else
            {
                byte[] dataLen = new byte[4];
                Array.Copy(data, ReadPos + 1, dataLen, 0, 4);

                if (BitConverter.IsLittleEndian)
                {
                    Array.Reverse(dataLen);
                }

                int length = BitConverter.ToInt32(dataLen, 0);
                string payload = Encoding.UTF8.GetString(data, ReadPos + 5, length);
                ReadPos += 5 + length;
                return payload;
            }
        }

        /// <summary>
        /// write the provided bytes to the data packet
        /// </summary>
        /// <param name="dataType">Data type.</param>
        /// <param name="payload">Payload.</param>
        private void writeBytes(byte dataType, byte[] payload)
        {
            byte[] totalData = new byte[payload.Length + 1];
            totalData[0] = dataType;
            Array.Copy(payload, 0, totalData, 1, payload.Length);

            byte[] rData = new byte[data.Length + totalData.Length];
            Array.Copy(data, 0, rData, 0, data.Length);
            Array.Copy(totalData, 0, rData, data.Length, totalData.Length);
            data = rData;
        }

        /// <summary>
        /// Reset the data reader
        /// </summary>
        public void beginRead()
        {
            ReadPos = 2;
        }

        /// <summary>
        /// What is the next data type
        /// </summary>
        /// <returns>The type.</returns>
        public dataType nextType()
        {
            if (ReadPos >= data.Length) { return dataType.nul; }
            return (dataType)data[ReadPos];
        }

        /// <summary>
        /// Convert the packet to a byte
        /// </summary>
        /// <returns>The byte.</returns>
        public byte[] ToByte()
        {
            return data;
        }

    }
}
