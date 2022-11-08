using ATMapp.Domain.Entities;
using ATMapp.Domain.Enums;
using ATMapp.Domain.Interfaces;
using ATMapp.UI;
using ConsoleTables;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ATMapp
{
    public class ATMapp : IUserLogin, IUserAccountActions, ITransaction
    {
        private List<UserAccount> userAccountList;
        private List<Transaction> transactionList;
        private UserAccount selectedAccount;
        private const decimal minimumKeptAmount = 500;
        private const int minBalance = 500;

        public void InitData()
        {
            userAccountList = new List<UserAccount>
            {
                new UserAccount{Id=1, FullName = "Obinna Ezeh", AccountNumber=123456,CardNumber =321321, CardPin="123123",AccountBalance=50000.00m,IsLocked=false},
                new UserAccount{Id=2, FullName = "Amaka Hope", AccountNumber=456789,CardNumber =654654, CardPin="456456",AccountBalance=4000.00m,IsLocked=false},
                new UserAccount{Id=3, FullName = "Femi Sunday", AccountNumber=123555,CardNumber =987987, CardPin="789789",AccountBalance=2000.00m,IsLocked=true},
            };
            transactionList = new List<Transaction>();
        }
        public void Run()
        {
            AppScrean.Welcome();
            CheckUserCardNumberAndPassword();
            ProcessMenuOption();
        }
        public void CheckUserCardNumberAndPassword()
        {
            bool isCorrectLogin = false;

            while (!isCorrectLogin)
            {
                UserAccount tempAccount = AppScrean.UserLoginForm();
                AppScrean.LoginProgress();
      
                foreach(UserAccount account in userAccountList)
                {
            
                    if (account.CardNumber==tempAccount.CardNumber)
                    {
                        if(account.IsLocked==true)
                        {
                            Utility.PrintMessage("Account locked. Call helpline", false);
                            return;
                        }
                        account.TotalLogin++;
                        if(account.CardPin==tempAccount.CardPin)
                        {
                     
                            Utility.LogInSuccessMassage(account.FullName);
                            account.TotalLogin = 0;
                            isCorrectLogin = true;
                            selectedAccount = account;
                            Console.Clear();
                            break;
                        }
                        else if (account.TotalLogin == 3)
                        {
                            Utility.PrintMessage("Account locked. Call helpline", false);
                            account.TotalLogin = 0;
                            account.IsLocked = true;
                            isCorrectLogin = true;
                            return;
                        }
                    }
                }
                if (isCorrectLogin == false) Utility.MissmatchMassage();
            }
        }

        public void ProcessMenuOption()
        {
            while (true)
            {
                AppScrean.DisplayAppMenu();
                switch (Validator.Convert<int>("an option"))
                {
                    case (int)AppMenu.CheckBalance:
                        CheckBalance();
                        break;
                    case (int)AppMenu.PlaceDeposit:
                        PlaceDeposite();
                        break;
                    case (int)AppMenu.MakeWithdrawal:
                        MakeWithdrawal();
                        break;
                    case (int)AppMenu.InternalTransfer:
                        var internalTransfer = AppScrean.InternalTransferForm();
                        ProcessInternalTranfer(internalTransfer);
                        break;
                    case (int)AppMenu.ViewTransaction:
                        ViewTransaction();
                        break;
                    case (int)AppMenu.Logout:
                        AppScrean.LogoutProgress();
                        return;
                    default:
                        Utility.PrintMessage("Invalid option", false);
                        break;
                }
            }

        }

        public void CheckBalance()
        {
            Utility.PrintMessage($"Your account balance is: {selectedAccount.AccountBalance}");
        }

        public void PlaceDeposite()
        {
            Console.WriteLine("Only multiple of 500 taka is allowed");
            var transaction_amt = Validator.Convert<int>("amount BDT");

            Console.WriteLine("Checking your notes");
            Utility.Timer(5);

            if (transaction_amt <= 0)
            {
                Utility.PrintMessage("Ammount needs to greater then 0", false);
                return;
            }
            if (transaction_amt % 500 != 0)
            {
                Utility.PrintMessage("Enter multiple of 500 notes", false);
                return;
            }
            if (PreviewBankNotes(transaction_amt) == false)
            {
                Utility.PrintMessage($"You have cancelled your action.", false);
                return;
            }
            InsertTransaction(selectedAccount.Id, TransactionType.Deposit, transaction_amt, "Deposited");

            selectedAccount.AccountBalance += transaction_amt;
            Utility.PrintMessage($"Success deposite of {transaction_amt} BDT\nCurrent balance : {selectedAccount.AccountBalance}", true);
        }

        public void MakeWithdrawal()
        {
            var ammount = 0;
            int selectedAmmount;
            repeat:
            selectedAmmount = AppScrean.SelectAmmount();
            if (selectedAmmount == -1)
            {
                goto repeat;
            }
            if (selectedAmmount == 0)
            {
                ammount = Validator.Convert<int>("ammount");
            }
            else ammount = selectedAmmount;

            if (ammount <= 0) Console.WriteLine("Ammount needs to greater than zero");
            else
            {
                if (ammount % 500 != 0)
                {
                    Utility.PrintMessage("Ammount needs to multiple of 500", false);
                }
                else if (ammount > selectedAccount.AccountBalance) Utility.PrintMessage("Insufficient balance", false);
                else if (selectedAccount.AccountBalance - ammount < minBalance)
                {
                    Utility.PrintMessage("Failed. Your account need to have" + $"minimum {minBalance} BDT", false);
                }
                else
                {
                    InsertTransaction(selectedAccount.Id, TransactionType.Withdrawl, -ammount, "withdrawn");
                    selectedAccount.AccountBalance -= ammount;
                    Utility.PrintMessage("Successfully withdrawn\nCurrent balance: " + $"{selectedAccount.AccountBalance}", true);
                }
            }

        }
        private bool PreviewBankNotes(int ammount)
        {
            int fiveHundredNotesCount = ammount / 500;
            Console.WriteLine($"BDT 500 X {fiveHundredNotesCount} = {500 * fiveHundredNotesCount}");
            Console.WriteLine($"Total amount: {ammount}\n\n");

            int option = Validator.Convert<int>("1 to confirm");
            return option.Equals(1);
        }

        public void InsertTransaction(long _UserBankAccountId, TransactionType _TType, decimal _ammount, string _description)
        {
            var transiction = new Transaction()
            {
                TransactionID = Utility.GetTId(),
                UserBankAccountID = _UserBankAccountId,
                TransactionDate = DateTime.Now,
                TType = _TType,
                Description = _description,
                TransactionAmmount = _ammount
            };
            transactionList.Add(transiction);
        }
        public void ViewTransaction()
        {
            var filteredTransaction = transactionList.Where(t => t.UserBankAccountID == selectedAccount.Id).ToList();
            if (filteredTransaction.Count <= 0)
            {
                Utility.PrintMessage("No transaction to show", true);
            }
            else
            {
                var table = new ConsoleTable("Id", "Transaction Date", "Type", "Descriptions", "Amount BDT");
                foreach(var tran in filteredTransaction)
                {
                    table.AddRow(tran.TransactionID, tran.TransactionDate, tran.TType, tran.Description, tran.TransactionAmmount);

                }
                table.Options.EnableCount = false;
                table.Write();
                Utility.PrintMessage($"You have {filteredTransaction.Count} transaction(s)", true);
            }
        }
        private void ProcessInternalTranfer(InternalTransfer internalTransfer)
        {
            if (internalTransfer.TranferAmmount <= 0)
            {
                Utility.PrintMessage("Ammount need to be greater than 0", false);
                return;
            }
            if (internalTransfer.TranferAmmount > selectedAccount.AccountBalance)
            {
                Utility.PrintMessage("Insufficiet fund", false);
                return;
            }
            if (selectedAccount.AccountBalance - internalTransfer.TranferAmmount < minBalance)
            {
                Utility.PrintMessage("Failed. Your account need to have" + $"minimum {minBalance} BDT", false);
                return;
            }

            UserAccount selectedRecevierAccount = userAccountList.FirstOrDefault(account => (account.AccountNumber == internalTransfer.ReciepeintAccountNumber));

            if (selectedRecevierAccount == null)
            {
                Utility.PrintMessage("No account exist in this AC Number",false);
                return;
            }
            if (selectedRecevierAccount.FullName != internalTransfer.ReciepeintAccountName)
            {
                Utility.PrintMessage("Wrong account name", false);
                return;
            }
            if (selectedRecevierAccount.AccountNumber == selectedAccount.AccountNumber)
            {
                Utility.PrintMessage("Transfaring own account is not possible", false);
                return;
            }

            InsertTransaction(selectedAccount.Id, TransactionType.Transfer, -internalTransfer.TranferAmmount, $"Transfered to {selectedRecevierAccount.AccountNumber} {selectedRecevierAccount.FullName}");
            selectedAccount.AccountBalance -= internalTransfer.TranferAmmount;

            InsertTransaction(selectedRecevierAccount.Id, TransactionType.Transfer, internalTransfer.TranferAmmount, $"Transtared from {selectedAccount.AccountNumber}");
            selectedRecevierAccount.AccountBalance += internalTransfer.TranferAmmount;

            Utility.PrintMessage($"Successfully transfared {internalTransfer.TranferAmmount} BDT from {selectedAccount.AccountNumber} to {selectedRecevierAccount.AccountNumber}\nCurrent Balance : {selectedAccount.AccountBalance}", true);

            

        }
    }
}
