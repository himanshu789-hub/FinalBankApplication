using System;
using System.Collections.Generic;
using FinalBankApplication.Controlller;
using FinalBankApplication.Models;
namespace FinalBankApplication
{
    class Program
    {
        static string _8th(int i)
        {
            if (i == 1)
                return "1st";
            if (i == 2)
                return "2nd";
            if (i == 3)
                return "3rd";
            return i + "th";
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Enter The Number OF Bank To SetUp : ");
            int num = Int32.Parse(Console.ReadLine());
            Bank[] bankArray = new Bank[num];
            for (int i = 0; i < num; i++)
            {
                Console.WriteLine("Enter " + _8th(i + 1) + " Bank Name : ");
                string name = Console.ReadLine().ToUpper();
                bankArray[i] = new Bank(name);
            }
            while (true)
            {
                Console.WriteLine("There Are Following Bank That Exists : ");
                for (int i = 0; i < num; i++)
                    Console.WriteLine(_8th(i + 1) + " Bank : " + bankArray[i].name);
                Console.WriteLine("Please Enter Bank Name You Are Associated With : ");
                string name = Console.ReadLine().ToUpper();
                Bank userBank =null;
                for (int i = 0; i < num; i++)
                {
                    if (name.Equals(bankArray[i].name))
                    {
                        userBank = bankArray[i];
                        break;
                    }
                }
                if (userBank == null)
                {
                    Console.WriteLine("Please, Enter A Valid Bank Name . . .");
                    continue;
                }
                //Enter In A Bank
                while (true)
                {
                    Console.WriteLine("<<< Welcome, To " + userBank.name + " Bank >>>");
                    Console.WriteLine("1. Staff\n2. Customer\nEnter(1/2) : ");
                    char choice = Console.ReadLine()[0];
                    if (choice == '1')
                    {
                        Console.WriteLine("Hello Staff, Enter Your User Id ");
                        string stfId = Console.ReadLine();
                        Console.WriteLine("Enter Your LogIn Password ");
                        string pword = Console.ReadLine();
                        Staff staff = (new BankService()).StaffExistsOrNot(userBank,stfId, pword);
                        if (staff == null)
                        {
                            Console.WriteLine("Please Enter Valid Credentials . . .");
                            continue;
                        }
                        bool staffWish = true;
                        while (staffWish)
                        {
                            Console.WriteLine("1.Add Account\n2.DeleteAccount\n3.Update Account\n4.ChangeCurrency\n5.ChangeServiceCharge\n6.View Transaction History\n7.Revert A Transaction\nE. Exit\nYour Choice . . . ");
                            char options = Console.ReadLine()[0];
                            switch (options)
                            {
                                case '1':
                                    Console.WriteLine("Enter Customer Name : ");
                                    string ename = Console.ReadLine();
                                    Console.WriteLine("Enter Password : ");
                                    string Password = Console.ReadLine();
                                    Customer customer =  new StaffService().AddCustomer(userBank, ename, Password);
                                    if (customer != null)
                                    {
                                        customer.SetAccount();
                                        Console.WriteLine("The User Id is : " + customer.UserId);
                                        Console.WriteLine("The Account Id Is : " + customer.GetAccount().AccountId);
                                    }
                                    else
                                        Console.WriteLine("Account Already Exists . . .");
                                    break;
                                case '2':
                                    Console.WriteLine("Enter Account Id To Delete ->> ");
                                    string accId = Console.ReadLine();
                                    bool doneOrNot = (new StaffService().DeleteAccount(userBank, accId));
                                    if (doneOrNot)
                                        Console.WriteLine("Account Successfully Deleted");
                                    else
                                        Console.WriteLine("Account Do Not Exists");
                                    break;
                                case '3':
                                    Console.WriteLine("Enter The UserId To Update : ");
                                    string UserId = Console.ReadLine();
                                    Console.WriteLine("Enter The New Password : ");
                                    string newPassword = Console.ReadLine();
                                        bool doneOrLeft = new AccountService().UpdateAccount(userBank,UserId,newPassword);
                                    if(doneOrLeft)
                                    Console.WriteLine("<<<-- Password Updated -->>>");
                                    else
                                        Console.WriteLine("No Such User Exists");
                                    break;
                                case '4':
                                    Console.WriteLine("Enter New Currency Name : ");
                                    string currencyName = Console.ReadLine();
                                    Console.WriteLine("Enter Rate : ");
                                    float rate = float.Parse(Console.ReadLine());
                                    new StaffService().ChangeCurrency(userBank, currencyName, rate);
                                    break;
                                case '5':
                                    Console.WriteLine("Enter IMPS and RTGS Rate : ");
                                    int IMPS = Int32.Parse(Console.ReadLine());
                                    int RTGS = Int32.Parse(Console.ReadLine());
                                    Console.WriteLine("Enter Foreign IMPS and RTGS Rate : ");
                                    int fIMPS = Int32.Parse(Console.ReadLine());
                                    int fRTGS = Int32.Parse(Console.ReadLine());
                                    new StaffService().ChangeRates(userBank, IMPS, RTGS, fIMPS, fRTGS);
                                    break;
                                case '6':
                                    {
                                        Console.WriteLine("Enter Account Id For Which Trnsaction History To Look Up -->> ");
                                        string AccountId = Console.ReadLine();
                                        bool recieverExists =false;
                                        for (int i = 0; i < bankArray.Length; i++)
                                        {
                                            if ((new BankService().CustomerExistsOrNotByAccId(bankArray[i], AccountId)) != null)
                                            {
                                                recieverExists = true;
                                                break;
                                            }
                                        }
                                        if (!recieverExists)
                                            Console.WriteLine("Account Do Not Exists . . .");
                                        else
                                        {
                                            List<Transaction> transactions = new StaffService().ViewTransactionHistory(userBank, AccountId);
                                            if(transactions==null)
                                            {
                                                Console.WriteLine("No Transaction Exists . . .");
                                                break;
                                            }
                                            Console.WriteLine("There Are Following Transactions : ");
                                            for (int i = 0; i < transactions.Count; i++)
                                                Console.WriteLine(transactions[i].ToString());
                                        }
                                    }
                                    break;
                                case '7':
                                    {
                                        Console.WriteLine("Enter Account Id For Which Trnsaction History To Look Up -->> ");
                                        string AccountId = Console.ReadLine();
                                        if(new BankService().CustomerExistsOrNotByAccId(userBank,AccountId)==null)
                                        {
                                            Console.WriteLine("No Such Account Exists In This Bank ! ! !");
                                            break;
                                        }
                                        List<Transaction> transactions = new StaffService().ViewTransactionHistory(userBank, AccountId);
                                        if(transactions==null)
                                        {
                                            Console.WriteLine("No Transaction Exists . . .");
                                            break;
                                        }
                                        for (int i = 0; i < transactions.Count; i++)
                                            Console.WriteLine(transactions[i].ToString());
                                        int tchoice = 0;
                                        bool eflag = false;
                                        while (true)
                                        {
                                            try
                                            {
                                                Console.WriteLine("Enter Your Choice = ");
                                                tchoice = Int32.Parse(Console.ReadLine());
                                                if (tchoice > transactions.Count)
                                                {
                                                    Console.WriteLine(">>-- Enter A Valid Choice --<<");
                                                    continue;
                                                }
                                                else
                                                    break;
                                            }
                                            catch (Exception e)
                                            {
                                                Console.WriteLine("Aborting Reverting A Transaction. . .");
                                                eflag = true;
                                                break;
                                            }

                                        }
                                        if (eflag)
                                            continue;
                                        Transaction tType = transactions[tchoice-1];
                                        if ((tType is Deposit) || (tType is WithDraw))
                                        {
                                            if(!tType.alive)
                                            {
                                                Console.WriteLine("Cannot Revert, Aborting Reverting !!!");
                                                break;
                                            }
                                          bool IsReverted =   new StaffService().RevertATransaction(userBank, tType);
                                            if (IsReverted)
                                                Console.WriteLine("SuccessFully Reverted . . .");
                                            else
                                                Console.WriteLine("Insufficient Balance !!!");
                                            tType.alive = false;
                                        }
                                        else
                                        {
                                            if(!tType.alive)
                                            {
                                                Console.WriteLine("Cannot Revert, Aborting Reverting !!!");
                                                break;
                                            }
                                            tType.alive = false;
                                            FundsTransfer fT = tType as FundsTransfer;
                                            string sourceId = fT.SourceId;
                                            string destId = fT.destinationAccId;
                                            string sourceBankId = fT.SourceBankId;
                                            string destBankId = fT.destinationBankId;
                                            Bank bankSource = null, bankDest = null;
                                            for (int i = 0; i < bankArray.Length; i++)
                                            {
                                                if (bankArray[i].Id.Equals(destBankId))
                                                    bankDest = bankArray[i];
                                                if (bankArray[i].Id.Equals(sourceBankId))
                                                    bankSource = bankArray[i];
                                            }
                                            bool value = new StaffService().RevertFundsTransfer(bankSource, bankDest, tType);
                                            if (value)
                                                Console.WriteLine("<<< Transaction Reverted SuccessFully >>>");
                                            else
                                                Console.WriteLine("Cannot Revert, Aborting Reverting . . .");
                                        }
                                    }
                                        break;
                                case 'e':
                                case 'E':
                                    staffWish = false;
                                    break;
                            }
                        }               
                    }
                    else if (choice == '2')
                    {

                        Console.WriteLine("Enter Your Username...");
                        string username = Console.ReadLine();
                        Console.WriteLine("Enter Your Password...");
                        string pword = Console.ReadLine();
                        
                        Customer accHolder = new BankService().CustomerLogIn(userBank, username,pword);
                        char choiceUser = 'e';
                        bool exitFlag = false;
                        while (!exitFlag)
                        {
                            if (accHolder != null)
                            {
                                Console.WriteLine("Enter A Choice\n1.Deposit Amount \n2.Withdraw Amount \n3.Transfer Funds \n4.View Transaction History E.Exit . . .\n");
                                choice = Console.ReadLine()[0];
                            }
                            else
                            {
                                Console.WriteLine("Username Do Not Exists . . .");
                                choice = 'e';
                            }
                            switch (choice)
                            {
                                case '1':
                                    {
                                        Console.WriteLine("Enter Amount To Deposit : ");
                                        float amount = float.Parse(Console.ReadLine());
                                        
                                       Transaction transaction =  new CustomerService().DepositMoney(userBank, accHolder,amount);
                                       
                                        if (transaction != null)
                                            Console.WriteLine("SuccessFully Depoisted");
                                        else
                                            Console.WriteLine("Not Deposited . . .");
                                        break;
                                    }
                                case '2':
                                    {
                                        Console.WriteLine("Enter Amount To Withdraw : ");
                                        float amtF = float.Parse(Console.ReadLine());
                                       Transaction transaction =  new CustomerService().WithDrawAmount(userBank, accHolder,amtF);
                                        if (transaction!=null)
                                            Console.WriteLine("SuccessFully WithDrawed");
                                        else
                                            Console.WriteLine("Insufficient Balance <<<< ");
                                        break;
                                    }
                                case '4':
                                    List<Transaction> transactions = new StaffService().ViewTransactionHistory(userBank, accHolder.GetAccount().AccountId);
                                    if(transactions==null)
                                    {
                                        Console.WriteLine("No Transactions Exists . . .");
                                        break;
                                    }
                                    for (int i = 0; i < transactions.Count; i++)
                                        Console.WriteLine(transactions[i].ToString());
                                    break;
                                case '3':
                                    while (true)
                                    {
                                        Console.WriteLine("Enter Account Id Of The Reciever : ");
                                        string destAccountId = Console.ReadLine();
                                        bool recieverExists = false;
                                        Bank receiverBank = null;
                                        Console.WriteLine("Enter Amount To Transfer -->> ");
                                        float amt = float.Parse(Console.ReadLine());
                                     
                                        Customer reciever = null;
                                        for (int i = 0; i < bankArray.Length; i++)
                                        {
                                            if ((reciever=new BankService().CustomerExistsOrNotByAccId(bankArray[i], destAccountId)) != null)
                                            {
                                                receiverBank = bankArray[i];
                                                recieverExists = true;
                                                break;
                                            }
                                        }
                                        
                                        if (recieverExists)
                                        {
                                            if(accHolder==reciever)
                                            {
                                                Console.WriteLine("Please, Enter Others Account Id . . .");
                                                    continue;
                                            }
                                          bool IsTransfer =new StaffService().FundsTransfer(userBank, receiverBank, amt, accHolder, reciever);
                                            if (IsTransfer)
                                                Console.WriteLine("<<<< Transfer SuccessFully >>>>");
                                            else
                                                Console.WriteLine(">>>> InSufficient Balance <<<<");

                                            break;
                                        }
                                        else
                                        {
                                            Console.WriteLine("Reciver Do Not Exists!!!");
                                            break;
                                        }
                                    }

                                    break;
                                case 'e':
                                case 'E':
                                    exitFlag = true;
                                    break;

                            }
                        }
                    }
                    else if (choice == 'e')
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Please Enter A Valid Choice . . .");
                        continue;
                    }
                }
            }
        }
    }
}
