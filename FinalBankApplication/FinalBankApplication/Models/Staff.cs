using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalBankApplication.Models
{
    class Staff
    {
        public string UserId { get; }
        public string BankId { get; set; }
        string Password;

        public Staff(string name, string pssword, string bId)
        {
            this.UserId = name;
            this.Password = pssword;
            this.BankId = bId;
        }
        public bool ValidatePassword(string password)
        {
            return this.Password.Equals(password);
        }

        public bool IsValidOrNot(string userId,string password)
        {
        return     this.UserId.Equals(userId) && this.Password.Equals(password);
        }
    }
}
