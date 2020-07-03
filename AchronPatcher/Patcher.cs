using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AchronPatcher
{
    public partial class Patcher : Form
    {
        public Patcher()
        {
            InitializeComponent();
        }

        private void btnPatch_Click(object sender, EventArgs e)
        {
            //A file was selected.
            if (ofdClient.ShowDialog() == DialogResult.OK)
            {
                //could be correct..
                if (ofdClient.FileName.ToLower().Contains(".exe") && ofdClient.FileName.ToLower().Contains("achron"))
                {
                    string name = ofdClient.FileName + ".backup-" + DateTime.Now.Ticks;
                    System.IO.File.Move(ofdClient.FileName, name);

                    System.IO.Stream clientStream = System.IO.File.Open(name, System.IO.FileMode.Open);
                    byte[] data = ToByteArray(clientStream);

                    byte[] target = new byte[] { 0x77, 0x77, 0x77, 0x2E,
                                                 0x61, 0x63, 0x68, 0x72,
                                                 0x6F, 0x6E, 0x67, 0x61,
                                                 0x6D, 0x65, 0x2E, 0x63,
                                                 0x6F, 0x6D };

                    byte[] result = new byte[] { 0x00, 0x00, 0x00, 0x00,
                                                 0x00, 0x00, 0x00, 0x00,
                                                 0x00, 0x00, 0x00, 0x00,
                                                 0x00, 0x00, 0x00, 0x00,
                                                 0x00, 0x00 };

                    byte[] tResult = ASCIIEncoding.ASCII.GetBytes(txtTarget.Text);

                    if (tResult.Length > target.Length)
                    {
                        MessageBox.Show("Target Address Too Long!");
                        return;
                    }

                    tResult.CopyTo(result, 0);

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
