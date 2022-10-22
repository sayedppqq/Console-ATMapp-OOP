using ATMapp.Domain.Entities;
using ATMapp.Domain.Interfaces;
using ATMapp.UI;
using System;
using System.Collections.Generic;

namespace ATMapp
{
    public class ATMapp : IUserLogin
    {
        private List<UserAccount> userAccountList;
        private UserAccount selectedAccount;

        public void InitData()
        {
            userAccountList = new List<UserAccount>
            {
                new UserAccount{Id=1, FullName = "Obinna Ezeh", AccountNumber=123456,CardNumber =321321, CardPin=123123,AccountBalance=50000.00m,IsLocked=false},
                new UserAccount{Id=2, FullName = "Amaka Hope", AccountNumber=456789,CardNumber =654654, CardPin=456456,AccountBalance=4000.00m,IsLocked=false},
                new UserAccount{Id=3, FullName = "Femi Sunday", AccountNumber=123555,CardNumber =987987, CardPin=789789,AccountBalance=2000.00m,IsLocked=true},
            };
        }
        public void CheckUserCardNumberAndPassword()
        {
            UserAccount tempAccount = new UserAccount();
            bool isCorrectLogin = false;
            tempAccount.CardNumber = Validator.Convert<long>("Your Card Number");
        }
    }
}
