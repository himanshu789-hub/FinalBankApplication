using FinalBankApplication.Models;
namespace FinalBankApplication.Controlller
{
    class AccountService
    {
        public Account AddAccount(Customer customer)
        {
            customer.SetAccount();
           return customer.GetAccount();
        }

        public bool UpdateAccount(Bank bank,string userID, string newPassword)
        {
            Customer customer = bank.customers.Find(element => element.UserId.Equals(userID));
            if (customer != null)
            {
                customer.SetPassword(newPassword);
                return true;
            }
            return false;
        }
    }
}
