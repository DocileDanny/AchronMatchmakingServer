using System;

namespace Util
{
    /// <summary>
    /// States for this terminals output.
    /// </summary>
    public enum TerminalState
    {
        FAIL = 0,
        WARNING = 1,
        OK = 2,
        MESSAGE = 3
    }

    /// <summary>
    /// This class represents some form of terminal output.
    /// </summary>
    public static class Terminal
    {
        /// <summary>
        /// Types of terminal message
        /// </summary>

        /// <summary>
        /// This object is used as a lock to prevent write conflicts.
        /// </summary>
        static object writeAccess = new object();

        /// <summary>
        /// Output a stylistic message to the console window.
        /// </summary>
        /// <param name="msgType">Which type of message should be output to the console?</param>
        /// <param name="msgOrigin">Which part of the application sent the message?</param>
        /// <param name="msgContent">The message to be printed.</param>
        public static void WriteLine(TerminalState msgType, string msgOrigin, string msgContent)
        {

            //ensure only one instance of terminal can output at once
            lock (writeAccess)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Write("[" + msgOrigin.ToUpper() + "]");

                switch (msgType)
                {
                    case TerminalState.OK:
                        Console.ForegroundColor = ConsoleColor.Green;
                        Write("[OK] ");
                        break;
                    case TerminalState.FAIL:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Write("[FAIL] ");
                        break;
                    case TerminalState.WARNING:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Write("[WARNING] ");
                        break;
                    default:
                        Write(" ");
                        //no state required.
                        break;
                }

                Console.ForegroundColor = ConsoleColor.White;
                Write(msgContent.ToUpper());
                Write(Environment.NewLine);
            }

        }

        private static void Write(string s)
        {
            if (BufferWidth() != 0)
            {
                Console.Write(s);
            }
            else
            {
                System.Diagnostics.Trace.Write(s);
            }
        }

        /// <summary>
        /// Output a terminal titlescreen
        /// </summary>
        /// <param name="Title">A string array containing all the elements for the title, each string must smaller, or equal to Terminal.BufferWidth in length</param>
        public static void WriteTitle(string[] titleText)
        {
            if (BufferWidth() == 0) { return; }

            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine();

            //ensure that no other thread can output to our terminal
            lock (writeAccess)
            {
                Console.Write(partition());

                foreach (string value in titleText)
                {
                    //are we adding a space before, or after the text?
                    bool location = false;
                    string cValue = value;

                    while (cValue.Length != BufferWidth())
                    {
                        if (location)
                        {
                            cValue = cValue + " ";
                        }
                        else
                        {
                            cValue = " " + cValue;
                        }
                        location = !location;
                    }

                    Console.Write(cValue);
                }

                Console.Write(partition());

            }

            Console.WriteLine();
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }

        private static string partition()
        {
            string value = "";
            for (int i = 0; i <
                BufferWidth(); i++)
            {
                value += "-";
            }
            return value;
        }

        /// <summary>
        /// return the size of the text buffer
        /// </summary>
        /// <returns>The width.</returns>
        public static int BufferWidth()
        {
            int width = 0;
            try { width = Console.BufferWidth; }
            catch { }
            return width;
        }

    }
}

