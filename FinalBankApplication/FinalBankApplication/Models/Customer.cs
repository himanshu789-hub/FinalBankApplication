using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalBankApplication.Models
{
    class Customer
    {
        public string name { get; set; }
        public string bankId { get; set; }
        public bool active { get; set; }
        Account @Account;
        public string UserId { get; set; }
        string Password;

        public bool ValidatePassword(string password)
        {
            return Password.Equals(password);
        }

        public void SetPassword(string newPassword)
        {
            this.Password = newPassword;
        }

        public Customer(string name, string bankId,string password)
        {
            this.active = true;
            this.name = name;
            this.bankId = bankId;
            this.Password = password;
            this.UserId = name.Substring(0, 3) + DateTime.UtcNow;
        }
        
        public void SetAccount()
        {
            this.Account = new Account(this);
        }

        public Account GetAccount() { return this.Account; }

       public void AddBalance(float amount) { this.Account.AddBalance(amount); }

        public bool IsValidOrNot(string userId, string password)
        {
            return this.UserId.Equals(userId) && this.Password.Equals(password);
        }   
    }
}
