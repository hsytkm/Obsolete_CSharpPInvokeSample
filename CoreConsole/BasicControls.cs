using System;
using System.Runtime.InteropServices;
using System.Text;

namespace CoreConsole
{
    class BasicControls
    {
        private const string DllFilePath = "NativeWinLib.dll";  // Windows
        //private const string DllFilePath = "NativeLinuxLib.so"; // Linux

        #region GetIntFromLib()

        [DllImport(DllFilePath)]
        private extern static int GetIntFromLib();

        #endregion

        #region GetStringFromLib()

        [DllImport(DllFilePath)]
        private extern unsafe static bool GetStringFromLib(byte* data, int length);

        private string GetString(int length = 11)
        {
            //length = 0;  // for error test
            string str;
            unsafe
            {
                var bs = new byte[length];
                fixed (byte* ptr = bs)
                {
                    bool success = GetStringFromLib(ptr, bs.Length);
                    var ret = Encoding.UTF8.GetString(bs);

                    if (success) str = $"{success} {ret}";
                    else str = "Error"; // GetErrorMessage();
                }
            }
            return str;
        }

        #endregion

        public void Test()
        {
            Console.WriteLine("--- BasicControls ---");

            /*
             * Get integer form lib
             */
            Console.WriteLine($"GetInt: {GetIntFromLib()}");

            /*
             * Get string form lib
             */
            Console.WriteLine($"GetString: {GetString()}");
        }

    }
}
