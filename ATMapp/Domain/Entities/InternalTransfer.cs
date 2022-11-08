using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMapp.Domain.Entities
{
    public class InternalTransfer
    {
        public decimal TranferAmmount { get; set; }
        public long ReciepeintAccountNumber { get; set; }
        public string ReciepeintAccountName { get; set; }
    }
}
