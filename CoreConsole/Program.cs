using System;
using System.Runtime.InteropServices;
using System.Text;

namespace CoreConsole
{
    class Program
    {
#if true
        // Windows
        [DllImport("NativeWinLib.dll")]
        private extern static int GetInt();

        [DllImport("NativeWinLib.dll")]
        private extern unsafe static bool GetString(byte* data, int length);
#else
        // Linux
        [DllImport("NativeLinuxLib.so")]
        private extern static int GetInt();

        [DllImport("NativeLinuxLib.so")]
        private extern unsafe static bool GetString(byte* data, int length);
#endif

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
            string str;
            unsafe
            {
                var bs = new byte[length];
                fixed (byte* ptr = bs)
                {
                    bool success = GetString(ptr, bs.Length);
                    var ret = Encoding.UTF8.GetString(bs);

                    if (success) str = $"{success} {ret}";
                    else str = "Error"; // GetErrorMessage();
                }
            }
            Console.WriteLine($"GetString: {str}");


        }
    }
}
