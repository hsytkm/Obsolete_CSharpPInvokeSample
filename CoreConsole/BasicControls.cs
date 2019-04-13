using System;
using System.Runtime.InteropServices;
using System.Text;

namespace CoreConsole
{
    class BasicControls
    {
#if true
        // Windows
        private const string DllFilePath = "NativeWinLib.dll";
#else
        // Linux
        private const string DllFilePath = "NativeLinuxLib.so";
#endif
        [DllImport(DllFilePath)]
        private extern static int GetInt();

        [DllImport(DllFilePath)]
        private extern unsafe static bool GetString(byte* data, int length);

        public void Test()
        {
            Console.WriteLine("--- BasicControls ---");

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
