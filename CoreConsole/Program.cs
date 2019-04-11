using System;
using System.Runtime.InteropServices;

namespace CoreConsole
{
    class Program
    {
        [DllImport("NativeWinLib.dll")]
        public extern static int GetInt();


        static void Main(string[] args)
        {
            var x = GetInt();
            Console.WriteLine($"Hello World {x} !");
        }
    }
}
