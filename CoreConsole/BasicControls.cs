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

        private void GetStringWrapper()
        {
            int length = new Random().Next(0, 17);
            string str;
            unsafe
            {
                var bs = new byte[length];
                fixed (byte* ptr = bs)
                {
                    bool success = GetStringFromLib(ptr, bs.Length);
                    var ret = Encoding.UTF8.GetString(bs);

                    if (success) str = $"{ret}";
                    else str = "**Error**"; // GetErrorMessage();
                }
            }
            Console.WriteLine($"GetStringFromLib\t: Len={length}, Str={str}");
        }

        #endregion

        #region GetBoolFromLib()

        [DllImport(DllFilePath)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private extern static bool GetBoolFromLib(int x);

        private void GetBoolWrapper()
        {
            var val = new Random().Next(0, 10);
            var ret = GetBoolFromLib(val) ? "odd" : "even";
            Console.WriteLine($"GetBoolFromLib\t\t: {val} is {ret}");
        }

        #endregion


        public void Test()
        {
            Console.WriteLine("--- BasicControls ---");

            // Get integer form lib
            Console.WriteLine($"GetIntFromLib\t\t: {GetIntFromLib()}");

            // Get string form lib
            GetStringWrapper();

            // Get bool form lib
            GetBoolWrapper();
        }

    }
}
