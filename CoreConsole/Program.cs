using System;
using System.Runtime.InteropServices;
using System.Text;

namespace CoreConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            // 簡単なDLLコール
            new BasicControls().Test();

            // バイナリデータの受け渡し
            new BinaryPayload().Test();

            //Console.WriteLine("WaitKey"); Console.ReadKey();
        }
    }
}
