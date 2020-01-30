using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalBankApplication.Controlller;
namespace FinalBankApplication.Models
{
    class StaffService
    {
        public StaffService() { }
        public Customer AddCustomer(Bank bank,string name,string password)
        {
           return (new BankService()).RegisterCustomer(bank,name, password);
        }

        public bool DeleteAccount(Bank bank, string accId)
        {
         return  new BankService().UnRegisterAUser(bank,accId);
        }

        public void ChangeRates(Bank bank, int newIMPS, int newRTGS, int newFIMPS, int newFRTGS)
        {
            new BankService().ChangeRates(bank, newIMPS, newRTGS, newFIMPS,  newFRTGS);
        }

        public void ChangeCurrency(Bank bank,string currencyName,float rate)
        {
            new BankService().ChangeCurrency(bank,currencyName,rate);
        }

        public List<Transaction> ViewTransactionHistory(Bank bank, string accId)
        {
            return new BankService().GetCustomerTransactionHistoryByAccountId(bank,accId);
        }
        

        public bool RevertATransaction(Bank bank,Transaction transaction)
        {
           return new BankService().RevertATransaction(bank, transaction);
        }

        public bool RevertFundsTransfer(Bank bankSource,Bank bankDest,Transaction transaction)
        {
            return new BankService().RevertFundsTransfer(bankSource, bankDest, transaction as FundsTransfer);
        }

        public bool FundsTransfer(Bank bankSource,Bank destinationBank,float amount,Customer source,Customer destination)
        {
           return new BankService().FundsTransfer(bankSource, destinationBank, amount, source, destination);
        }

        
    }
}
