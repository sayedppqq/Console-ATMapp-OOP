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
            AppScrean.Welcome();
            long cardNumber = Validator.Convert<long>("Your Card Number");
            Console.WriteLine(cardNumber);

            Utility.PressEnterToContinue();
        }
    }
}
