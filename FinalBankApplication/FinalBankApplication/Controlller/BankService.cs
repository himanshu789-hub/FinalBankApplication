using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalBankApplication.Models;
namespace FinalBankApplication.Controlller
{
    class BankService
    {


        public Staff StaffExistsOrNot(Bank bank, string userId, string password)
        {
            Staff staff = bank.staffs.Find(element => element.UserId.Equals(userId) && element.ValidatePassword(password));
            return staff;
        }
        

        public Customer RegisterCustomer(Bank bank, string name, string password)
        {
            Customer customer = bank.customers.Find(element =>element.IsValidOrNot(name,password));
            if (customer != null)
                return null;
            bank.customers.Add(new Customer(name, bank.Id,password));
            bank.customers[bank.customers.Count - 1].SetAccount();
            return bank.customers[bank.customers.Count - 1];
        }

        public Customer CustomerExistsOrNotByAccId(Bank bank, string accId)
        {
            Customer customer = bank.customers.Find(element => element.GetAccount().AccountId.Equals(accId));
            return customer;
        }

        public Customer CustomerLogIn(Bank bank, string username,string password)
        {
         return bank.customers.Find(element => element.IsValidOrNot(username,password));
        }


        public bool UnRegisterAUser(Bank bank, string accId)
        {
            Customer customer = this.CustomerExistsOrNotByAccId(bank, accId);
            if (customer != null && customer.active)
            {
                customer.active = false;
                return true;
            }
            return false;
        }

        public void ChangeRates(Bank bank, int newIMPS, int newRTGS, int newFIMPS, int newFRTGS)
        {
            bank.IMPS = newIMPS;
            bank.RTGS = newRTGS;
            bank.fIMPS = newFIMPS;
            bank.fRTGS = newFRTGS;
        }

        public void ChangeCurrency(Bank bank, string currencyName, float rates)
        {
            bank.currency = currencyName;
            bank.ChangeRates(rates);
        }

        public List<Transaction> GetCustomerTransactionHistoryByAccountId(Bank bank, string accountId)
        {
            List<Transaction> transactions = bank.transactions.FindAll(element =>
            {
                if ((element is Deposit || element is WithDraw))
                {
                    if (element.SourceId.Equals(accountId))
                        return true;
                    else
                        return false;
                }
                else if ((element as FundsTransfer).destinationAccId.Equals(accountId) || (element as FundsTransfer).SourceId.Equals(accountId))
                    return true;
                else
                    return false;
            });
            if (transactions.Count == 0)
                return null;
            return transactions;
        }
        
        public bool RevertATransaction(Bank bank, Transaction transaction)
        {
            string transactionId = transaction.Id;
            Customer customer = this.CustomerExistsOrNotByAccId(bank, transaction.SourceId);
                   if (transaction is WithDraw)
                    {
                        WithDraw wD = transaction as WithDraw;
                        float amt = wD.Amountflow;
                        bank.transactions.Add(new Deposit(wD.SourceId, wD.SourceBankId, -1*amt));
                        return true;
                    }
                    else if (transaction is Deposit)
                    {
                        Deposit dp = transaction as Deposit;
                        float amt = dp.Amountflow;
                        if (customer.GetAccount().IsBalanceSufficient(amt))
                        {
                            customer.GetAccount().deductBalance(amt);
                            bank.transactions.Add(new WithDraw(dp.SourceId, dp.SourceBankId,-1*amt));
                            return true;
                        }
                return false;
                        
                    }
             
            return false;
        }
            public bool RevertFundsTransfer(Bank banKSource,Bank bankDest,FundsTransfer transaction)
            {
                string sourceId = transaction.SourceId;
                string destinationId = transaction.destinationAccId;
                Customer sender = null, reciever = null;
                float amountFlow = transaction.Amountflow;
                sender = this.CustomerExistsOrNotByAccId(banKSource, sourceId);
                reciever = this.CustomerExistsOrNotByAccId(banKSource, destinationId);

            if (reciever != null && sender !=null)
            {
                if (reciever.GetAccount().IsBalanceSufficient(-1*amountFlow))
                {
                    sender.AddBalance(-1*amountFlow);
                    reciever.AddBalance(amountFlow);
                    bankDest.transactions.Add(new FundsTransfer(destinationId, bankDest.Id, amountFlow,  sourceId,banKSource.Id));
                    if(sender.bankId!=reciever.bankId)
                banKSource.transactions.Add(new FundsTransfer(destinationId, transaction.destinationBankId, amountFlow*-1 , sourceId, transaction.SourceBankId));

                    return true;
                }
                else
                    return false;
            }
            else
                return false;
        }

        public bool FundsTransfer(Bank bankSource,Bank bankDestination, float amount,Customer source, Customer destination)
        {
                if (amount >= 100000)
                {
                    if (bankSource.Id.Equals(bankDestination.Id))
                        amount = amount * ((100 + ((float)(bankSource.RTGS))) / 100);
                    else
                        amount = amount * ((100 + ((float)(bankSource.fRTGS))) / 100);
                }
                else
                {
                    if (bankSource.Id.Equals(bankDestination.Id))
                        amount = amount * ((100 + ((float)(bankSource.IMPS))) / 100);
                    else
                        amount = amount * ((100 + ((float)(bankSource.fIMPS))) / 100);
                }
                if (source.GetAccount().IsBalanceSufficient(amount))
                {
                    source.AddBalance(-1*amount);
                    destination.AddBalance(amount);
  bankSource.transactions.Add(new FundsTransfer(source.GetAccount().AccountId, bankSource.Id, -1*amount,destination.GetAccount().AccountId, bankDestination.Id));
                if(source.bankId!=destination.bankId)
   bankDestination.transactions.Add(new FundsTransfer(source.GetAccount().AccountId, bankSource.Id, amount, destination.GetAccount().AccountId, bankDestination.Id));

                return true; 
                }
                return false;
    }

        public Transaction DepositMoney(Bank bank, float amount, Customer customer)
        {
            customer.AddBalance(amount);
            bank.transactions.Add(new Deposit(customer.GetAccount().AccountId,bank.Id,amount));
           
            return bank.transactions[bank.transactions.Count - 1];
        }

        /*public Transaction WithDrawMoney(Bank bank, float amount, int accountHolderIndex)
        {
            if(.(accountHolderIndex,amount))
            {
                bank.AddMoney(amount, accountHolderIndex);
                return true;
            }
            return false;
        }*/
    }

    }

