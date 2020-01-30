using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalBankApplication.Models;
namespace FinalBankApplication.Controlller
{
    class CustomerService
    {

        public Customer CreateCustomer(Bank bank,string name,string password)
        {
            bank.customers.Add(new Customer(name, bank.Id,password));
             return bank.customers[bank.customers.Count - 1];
        }

       public Transaction DepositMoney(Bank bank,Customer customer,float amount)
        {
            customer.AddBalance(amount);
            bank.transactions.Add(new Deposit(customer.GetAccount().AccountId, bank.Id, amount));
            return bank.transactions[bank.transactions.Count - 1];
        }

        public Transaction WithDrawAmount(Bank bank,Customer customer,float amount)
        {
            if (customer.GetAccount().IsBalanceSufficient(amount))
            {
                customer.GetAccount().deductBalance(amount);
                bank.transactions.Add(new WithDraw(customer.GetAccount().AccountId, bank.Id, -1*amount));
                return bank.transactions[bank.transactions.Count-1];
            }
            return null;
        }

        public void ChangeBalanaceAsPerRate(Customer customer,float rate)
        {
            customer.GetAccount().ChangeValueByRate(rate);
        }

    }
}
