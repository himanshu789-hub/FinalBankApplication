using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalBankApplication.Models
{
    class FundsTransfer : Transaction
    {
       public string destinationBankId { get; }
        public string destinationAccId { get; }

        public override string ToString()
        {
            return this.Id + " " + this.Amountflow + " "  + " " + this.destinationAccId + " " + this.destinationBankId;
        }

        public FundsTransfer(string accId, string bankId, float amt, string recieverAccId, string recieverBankId) : base(accId, bankId, amt)
        {
            this.destinationAccId = recieverAccId;
            this.destinationBankId = recieverBankId;
        }
       
    }
}
