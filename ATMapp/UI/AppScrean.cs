using ATMapp.Domain.Entities;
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

            Utility.PressEnterToContinue();
        }
        internal static UserAccount UserLoginForm()
        {
            UserAccount tempAccount = new UserAccount();
            tempAccount.CardNumber = Validator.Convert<long>(" Card Number");
            tempAccount.CardPin = Utility.GetSecretInput("Enter Your Pin");
            return tempAccount;
        }

        internal static void LoginProgress()
        {
            Console.WriteLine("\nChecking your credentials. Please wait");
            Utility.Timer(5);
        }
        internal static void DisplayAppMenu()
        {
            Console.Clear();
            Console.WriteLine("-------My ATM App Menu-------");
            Console.WriteLine(":                           :");
            Console.WriteLine("1. Account Balance          :");
            Console.WriteLine("2. Cash Deposit             :");
            Console.WriteLine("3. Withdrawal               :");
            Console.WriteLine("4. Transfer                 :");
            Console.WriteLine("5. Transactions             :");
            Console.WriteLine("6. Logout                   :");
        }
        internal static void LogoutProgress()
        {
            Console.WriteLine("Loging out");
            Utility.Timer(10);
            Utility.PrintMessage("Logout Successful. Take your card", true);
        }
        internal static int SelectAmmount()
        {
            Console.WriteLine("Available optins:");
            Console.WriteLine("1.BDT500      5.BDT10,000");
            Console.WriteLine("2.BDT1000     6.BDT15,000");
            Console.WriteLine("3.BDT2000     7.BDT20,000");
            Console.WriteLine("4.BDT5000     8.BDT40,000");
            Console.WriteLine("0.Other");
            Console.WriteLine("");

            int selectedAmmount = Validator.Convert<int>("an option");
            switch (selectedAmmount)
            {
                case 1:
                    return 500;
                    
                case 2:
                    return 1000;
                    
                case 3:
                    return 2000;
                    
                case 4:
                    return 5000;
                    
                case 5:
                    return 10000;
                    
                case 6:
                    return 15000;
                    
                case 7:
                    return 20000;
                    
                case 8:
                    return 40000;
                    
                case 0:
                    return 0;
                    
                default:
                    Utility.PrintMessage("Invalid input. Try again.", false);
                    Console.Clear();
                    return -1;
            }
        }
        internal static InternalTransfer InternalTransferForm()
        {
            InternalTransfer internalTransfer = new InternalTransfer();
            internalTransfer.ReciepeintAccountNumber = Validator.Convert<long>("Reciepeint Account Number");
            internalTransfer.ReciepeintAccountName = Utility.GetUserInput("Reciepeint Account Name");
            internalTransfer.TranferAmmount = Validator.Convert<decimal>("ammount");

            return internalTransfer;
        }
    }
}
