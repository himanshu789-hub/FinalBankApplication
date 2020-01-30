using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalBankApplication.Models
{
    class Bank 
    {
        public List<Customer> customers;
        public List<Transaction> transactions;
        public List<Staff> staffs;
        public string Id { get; set; }
        public string name { get; set; }
        public int IMPS { get; set; }
        public int RTGS { get; set; }
        public int fIMPS { get; set; }
        public int fRTGS { get; set; }
        public string currency = "INR";
        
        public Bank(string name)
        {
            customers = new List<Customer>();
            transactions = new List<Transaction>();
            staffs = new List<Staff>();
            this.Id = name.Substring(0, 3) + DateTime.Now;
            this.name = name;
            staffs.Add(new Staff("admin", "admin", this.Id));
            IMPS = 0;
            fIMPS = 6;
            RTGS = 5;
            fRTGS = 2;
            customers.Add(new Customer("abc", "abc","abc"));
            customers[0].SetAccount();
        }

        public void ChangeRates(float rate)
        {
            for(int i=0;i<customers.Count;i++)
            {
                customers[i].GetAccount().ChangeValueByRate(rate);
            }
        }

        
    }
}
