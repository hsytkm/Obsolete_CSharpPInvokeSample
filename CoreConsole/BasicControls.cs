using System;
using System.Runtime.InteropServices;
using System.Text;

namespace CoreConsole
{
    class BasicControls
    {
        private const string DllFilePath = "NativeWinLib.dll";  // Windows
        //private const string DllFilePath = "NativeLinuxLib.so"; // Linux

        private static readonly Random Random = new Random();

        #region GetIntFromLib()

        [DllImport(DllFilePath)]
        private extern static int GetIntFromLib();

        #endregion

        #region GetStringFromLib()

        [DllImport(DllFilePath)]
        private extern unsafe static bool GetStringFromLib(byte* data, int length);

        private static void GetStringWrapper()
        {
            int length = Random.Next(0, 17);
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
        private extern static bool GetBoolFromLib(
            [MarshalAs(UnmanagedType.Bool)]bool b1,
            [MarshalAs(UnmanagedType.Bool)]bool b2);

        private static void GetBoolWrapper()
        {
            // logical conjunction(論理積)
            var b1 = ((Random.Next() & 1) == 1) ? true : false;
            var b2 = ((Random.Next() & 1) == 1) ? true : false;
            var ret = GetBoolFromLib(b1, b2);
            Console.WriteLine($"GetBoolFromLib\t\t: {b1} AND {b2} -> {ret}");
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
