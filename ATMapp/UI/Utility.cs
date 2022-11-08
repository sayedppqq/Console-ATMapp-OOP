using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ATMapp.UI
{
    public static class Utility
    {
        private static CultureInfo culture = new CultureInfo("IG-NG");
        private static long TID = 0;
        public static void LogInSuccessMassage(string fullName)
        {
            PrintMessage("Login successfull. Wellcome back "+fullName, true);
            Console.WriteLine("WellCome back {0}", fullName);
        }
        public static void MissmatchMassage()
        {
            PrintMessage("Account number or pin inccorect. Try again", false);
        }
        public static string GetUserInput(string temp)
        {
            Console.WriteLine("Please Enter {0}", temp);
            return Console.ReadLine();
        }
        public static void PressEnterToContinue()
        {
            Console.WriteLine("Press enter to continue.....");
            Console.ReadLine();
        }
        public static long GetTId()
        {
            return ++TID;
        }
        public static void Timer(int time)
        {
            for(int i = 0; i < time; i++)
            {
                Console.Write(".");
                Thread.Sleep(200);
            }
            Console.Clear();
        }
        public static string GetSecretInput(string massage)
        {
            bool isPrompt = true;
            string pass="";
            while (true)
            {
                if (isPrompt) Console.WriteLine(massage);
                isPrompt = false;

                ConsoleKeyInfo inputKey = Console.ReadKey(true);

                if (inputKey.Key == ConsoleKey.Enter)
                {
                    if (pass.Length == 6)
                    {
                        return pass;
                    }
                    else
                    {
                        Utility.PrintMessage("\nPlease enter 6 digits.", false);
                        isPrompt = true;
                        pass = "";
                    }
                }
                else if (inputKey.Key == ConsoleKey.Backspace)
                {
                    if (pass.Length >= 1)
                    {
                        pass.Remove(pass.Length - 1);
                    }
                }
                else
                {
                    if (pass.Length == 6)
                    {
                        PrintMessage("\nPlease enter 6 digits.", false);
                        isPrompt = true;
                        pass = "";
                    }
                    else
                    {
                        pass += inputKey.KeyChar;
                        Console.Write("*");
                    }
                }
            }
        }
        public static void PrintMessage(string msg, bool success = true)
        {
           
            if (success)
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            Console.WriteLine(msg);
            Console.ForegroundColor = ConsoleColor.White;

            PressEnterToContinue();
        }
        public static string FormatAmount(decimal ammount)
        {
            return String.Format(culture, "{0:C2}", ammount);
        }
    }
}
