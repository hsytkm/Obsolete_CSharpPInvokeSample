using System;
using System.Runtime.InteropServices;
using System.Text;

namespace CoreConsole
{
    class Program
    {
        [DllImport("NativeWinLib.dll")]
        private extern static int GetInt();

        [DllImport("NativeWinLib.dll")]
        private extern unsafe static bool GetString(byte* data, int length);

        static void Main(string[] args)
        {
            /*
             * Get integer form dll
             */
            Console.WriteLine($"GetInt: {GetInt()}");

            /*
             * Get string form dll
             */
            int length = 11;
            //length = 0;  // for error test
            unsafe
            {
                var bs = new byte[length];
                fixed (byte* ptr = bs)
                {
                    bool success = GetString(ptr, bs.Length);
                    var str = Encoding.UTF8.GetString(bs);

                    if (success) Console.WriteLine($"GetString: {success} {str}");
                    else Console.WriteLine($"GetString: {success}");
                }
            }

        }
    }
}
