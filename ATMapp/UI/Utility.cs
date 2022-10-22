using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMapp.UI
{
    public static class Utility
    {
        public static string GetUserInput(string temp)
        {
            Console.WriteLine("Enter your {0}", temp);
            return Console.ReadLine();
        }
        public static void PressEnterToContinue()
        {
            Console.WriteLine("Press enter to continue.....");
            Console.ReadLine();
        }
    }
}
