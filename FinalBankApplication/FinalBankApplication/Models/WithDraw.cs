using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalBankApplication.Models
{
    class WithDraw : Transaction
    {
        public WithDraw(string accId, string bankId, float amt) : base(accId, bankId, amt)
        {

        }

        public override string ToString()
        {
            return this.Id + " " + this.Amountflow + " ";
        }
    }
}
