using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMapp.UI
{
    public static class AppScrean
    {
        internal static void Welcome()
        {
            Console.Clear();
            Console.Title = "My ATM App";
            Console.WriteLine("Wellcome to ATM App");
            PressEnterToContinue();
        }

        private static void PressEnterToContinue()
        {
            Console.WriteLine("Press enter to continue.....");
            Console.ReadLine();
        }
    }
}
