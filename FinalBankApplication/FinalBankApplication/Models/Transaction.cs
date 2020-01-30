using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalBankApplication.Models
{
   abstract class Transaction
    {
        protected Transaction(string accId, string bankId, float amt)
        {

            this.SourceId = accId;
            this.SourceBankId = bankId;
            this.Amountflow = amt;
            this.alive = true;
            Id = "TXN" + " " + accId + " " + bankId + " " + DateTime.UtcNow.ToShortDateString();

        }
        public bool alive { get; set; }
        public string SourceBankId { get; set; }
        public string Id { get; }
        public string SourceId { get; set; }
        public float Amountflow { get; set; }
        
    }
}
