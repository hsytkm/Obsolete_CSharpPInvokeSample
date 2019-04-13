using System;
using System.Runtime.InteropServices;
using System.Text;

namespace CoreConsole
{
    class BasicControls
    {
        private const string DllFilePath = "NativeWinLib.dll";      // Windows
        //private const string DllFilePath = "NativeLinuxLib.so";   // Linux

        private static readonly Random Random = new Random();

        #region int(In/Out)

        [DllImport(DllFilePath)]
        private extern static int AddIntegerFromLib(int x, int y);

        private static void GetSetIntWrapper()
        {
            var x = Random.Next(0, 10);
            var y = Random.Next(0, 10);
            var ret = AddIntegerFromLib(x, y);
            Console.WriteLine($"int(In/Out)\t\t: {x} + {y} = {ret}");
        }

        #endregion

        #region bool(In/Out)

        [DllImport(DllFilePath)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private extern static bool GetLogicalConjunctionFromLib(
            [MarshalAs(UnmanagedType.Bool)]bool b1,
            [MarshalAs(UnmanagedType.Bool)]bool b2);

        private static void GetSetBoolWrapper()
        {
            // logical conjunction(論理積)
            var b1 = ((Random.Next() & 1) == 1) ? true : false;
            var b2 = ((Random.Next() & 1) == 1) ? true : false;
            var ret = GetLogicalConjunctionFromLib(b1, b2);
            Console.WriteLine($"bool(In/Out)\t\t: {b1} AND {b2} -> {ret}");
        }

        #endregion

        #region string(In/Out)

        [DllImport(DllFilePath, CharSet = CharSet.Unicode)]
        private extern unsafe static void ToUpperFromLib(
            [MarshalAs(UnmanagedType.LPUTF8Str), In] string inText,
            [MarshalAs(UnmanagedType.LPUTF8Str), Out] StringBuilder outText,
            int outLength);

        private static void ToUpperWrapper()
        {
            var source = "aBcDEFghI";
            var buff = new StringBuilder(source.Length);
            ToUpperFromLib(source, buff, buff.Capacity);

            Console.WriteLine($"string(In/Out)\t\t: {source} -> {buff.ToString()}");
        }

        #endregion

        #region string(byte*)

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

        public void Test()
        {
            Console.WriteLine("--- BasicControls ---");

            // Get/Set integer
            GetSetIntWrapper();

            // Get/Set bool
            GetSetBoolWrapper();

            // Get StringBuilder / Set string
            ToUpperWrapper();

            // Get string(byte*)
            GetStringWrapper();


        }

    }
}
