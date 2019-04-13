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

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        class Buffer
        {
            public readonly IntPtr Data;
            public readonly int Length;

            public Buffer(IntPtr p, int l) { Data = p; Length = l; }
        }

        #region int(In/Out)

        [DllImport(DllFilePath)]
        private extern static int AddIntegerFromLib(int x, int y);

        private static void InOutIntWrapper()
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

        private static void InOutBoolWrapper()
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

        private static void InOutStringWrapper()
        {
            var source = "aBcDeFghI";
            var buff = new StringBuilder(source.Length);
            ToUpperFromLib(source, buff, buff.Capacity);

            Console.WriteLine($"string(In/Out)\t\t: {source} -> {buff.ToString()}");
        }

        #endregion

        #region byte array use unsafe(In)

        [DllImport(DllFilePath)]
        private extern unsafe static int InByteArray1Lib(byte* data, int length);

        private static void InByteArrayUnsafeWrapper()
        {
            // ‭0x075BCD15‬ = ‭123456789‬DEC
            var bs = new byte[] { 0x15, 0xcd, 0x5b, 0x07 };
            int source = BitConverter.ToInt32(bs);
            unsafe
            {
                fixed (byte* ptr = bs)
                {
                    int ret = InByteArray1Lib(ptr, bs.Length);
                    Console.WriteLine($"Byte[]Unsafe(In)\t: 0x{source:X8} = {ret} DEC");
                }
            }
        }

        #endregion

        #region byte array don't use unsafe(In)

        [DllImport(DllFilePath)]
        private extern static int InByteArray2Lib(Buffer buffer);

        private static void InByteArrayIntPtrWrapper()
        {
            // ‭0x‭3ADE68B1‬ = ‭‭987654321‬‬DEC
            var bs = new byte[] { 0xb1, 0x68, 0xde, 0x3a };
            int source = BitConverter.ToInt32(bs);

            IntPtr ptr = Marshal.AllocHGlobal(bs.Length);
            try
            {
                Marshal.Copy(bs, 0, ptr, bs.Length);
                var buffer = new Buffer(ptr, bs.Length);
                int ret = InByteArray2Lib(buffer);
                Console.WriteLine($"Byte[]IntPtr(In)\t: 0x{source:X8} = {ret} DEC");
            }
            finally { Marshal.FreeHGlobal(ptr); }
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

            // int(In/Out)
            InOutIntWrapper();

            // bool(In/Out)
            InOutBoolWrapper();

            // string(In/Out)
            InOutStringWrapper();

            // byte array use unsafe(In)
            InByteArrayUnsafeWrapper();

            // byte array don't use unsafe(In)
            InByteArrayIntPtrWrapper();

            // Get string(byte*)
            GetStringWrapper();


        }

    }
}
