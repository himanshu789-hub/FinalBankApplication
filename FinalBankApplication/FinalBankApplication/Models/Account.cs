using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalBankApplication.Models
{
    class Account
    {
        public string AccountId;
        float balance;


        public Account(Customer customer)
        {
            this.AccountId = customer.name.Substring(0,3)+" "+customer.bankId+" "+DateTime.UtcNow;
            this.balance = 0;
        }

        public bool  ValidateAccountId(string accId)
        {
            return AccountId.Equals(AccountId);
        }

        public void deductBalance(float amount)
        {
            this.balance -= amount;
        }
        
        public void AddBalance(float amount)
        {
            this.balance = amount;
        }
            public bool IsBalanceSufficient(float amount)
        {
            if (this.balance >= amount)
                return true;
            return false;
        }
       
        public void ChangeValueByRate(float rate)
        {
            this.balance *= rate;
        }
    }
}
