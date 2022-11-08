using ATMapp.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMapp.App
{
    class Entry
    {
        static void Main(string[] args)
        {
            ATMapp atmApp = new ATMapp();
            atmApp.InitData();
            while (true)
            {
                Console.WriteLine("Press 0 to run");
                int decision = Convert.ToInt32(Console.ReadLine());
                if (decision == 0) atmApp.Run();
                else break;
            }
        }
    }
}
