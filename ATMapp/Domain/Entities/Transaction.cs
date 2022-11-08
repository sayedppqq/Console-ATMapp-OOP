using ATMapp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMapp.Domain.Entities
{
    public class Transaction
    {
        public long TransactionID { get; set; }
        public long UserBankAccountID { get; set; }
        public DateTime TransactionDate { get; set; }
        public TransactionType TType { get; set; }
        public string Description { get; set; }
        public decimal TransactionAmmount { get; set; }
    }
}
