using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Models;
using BusinessLogic;


for (; ; )
{
    Console.WriteLine("                                  *******====================*******");
    Console.WriteLine("                                         1.Account Holder");
    Console.WriteLine("                                         2.Bank Staff");
    Console.WriteLine("                                         3.Exit");
    Console.WriteLine("                                         4.Admin");
    string user = Console.ReadLine();
    if (user == "3") break;
    switch (user)
    {
        case "1":
            Console.WriteLine("Enter Bank Name:");
            string bankName = Console.ReadLine();
            Console.WriteLine("Enter UserName:");
            string UserName = Console.ReadLine();
            Console.WriteLine("Enter Password:");
            string password = Console.ReadLine();

            foreach (Bank currentBank in Admin.AllBanks)
            {
                var checkForUser = from name in currentBank.AllAccounts
                                   where name.UserName.Equals(UserName) && name.Password.Equals(password)
                                   select name;
                if (checkForUser.Count() > 0)
                {
                    Console.WriteLine("                             1.Deposit Amount");
                    Console.WriteLine("                             2.Withdraw Amount");
                    Console.WriteLine("                             3.Transfer Funds");
                    Console.WriteLine("                             4.View Transaction History");
                    string AccountHolderChoice = Console.ReadLine();
                    switch (AccountHolderChoice)
                    {
                        case "1":
                            Console.WriteLine("                     Enter amount:");
                            string amount = Console.ReadLine();
                            foreach (AccountHolder currentHolder in checkForUser)
                            {
                                int value = AccountHolderServices.DepositAmount(Convert.ToInt32(amount), currentHolder);
                                if (value == 0) Console.WriteLine("Please enter amount more than 0");
                                else Console.WriteLine("Updated Balance:{0}", currentHolder.Balance);
                            }

                            break;
                        case "2":
                            Console.WriteLine("              Enter the amount you want to withdraw:");
                            string withDrawamount = Console.ReadLine();
                            foreach (AccountHolder currentHolder in checkForUser)
                            {

                                int value = AccountHolderServices.WithDrawAmount(Convert.ToInt32(withDrawamount), currentHolder);
                                if (value == 0) Console.WriteLine("Insufficient Funds..!!");
                                else Console.WriteLine("Updated Balance:{ 0}", currentHolder.Balance);
                            }


                            break;
                        case "3":
                            foreach (AccountHolder currentHolder in checkForUser)
                            {
                                Console.WriteLine("Enter Account ID to which you want to transfer funds:");
                                string transferAccount = Console.ReadLine();
                                Console.WriteLine("Enter amount:");
                                string transferAmount = Console.ReadLine();
                                Console.WriteLine("Enter Bank ID");
                                string transferBankId = Console.ReadLine();

                                int value = AccountHolderServices.TransferFunds(Convert.ToInt32(transferAmount), transferAccount, transferBankId, currentHolder);
                                if (value == 0) Console.WriteLine("Insufficient funds");
                                else Console.WriteLine("Updated Balance:{0}", currentHolder.Balance);
                            }

                            break;
                        case "4":
                            Console.WriteLine("             Transaction History");
                            foreach (AccountHolder name in checkForUser)
                                foreach (Transaction transaction in name.Transactions)
                                {
                                    Console.Write(transaction.SenderAccountId);
                                    Console.Write(transaction.Amount);
                                    Console.WriteLine(transaction.TransactionId);
                                }

                            break;
                    }
                }
                else
                {
                    Console.WriteLine("                             User not found..!!");

                }


            }

            break;

        case "2":
            {
                Console.WriteLine("                 Hi Staff!!");
                Console.WriteLine("Enter Bank name:");
                string staffBank = Console.ReadLine();
                Console.WriteLine("Enter Staff User Name:");
                string staffUserName = Console.ReadLine();
                Console.WriteLine("Enter Password:");
                string staffPassword = Console.ReadLine();


                foreach (Bank currentBank in Admin.AllBanks)
                {

                    if (currentBank.BankName.Equals(staffBank))
                    {

                        var checkForStaff = from name in currentBank.Staff
                                            where name.UserName.Equals(staffUserName) && name.Password.Equals(staffPassword)
                                            select name;
                        if (checkForStaff.Count() == 0)
                        {
                            Console.WriteLine("Staff not found.");
                        }
                        else
                        {
                            foreach (BankStaff currentStaff in checkForStaff)
                            {

                                Console.Write("Your Bank ID is:");
                                Console.WriteLine(currentBank.BankId);
                                Console.WriteLine("--------------------*****------------------------");
                                Console.WriteLine("--------------------*****------------------------");
                                Console.WriteLine("            1.Create new Account");
                                Console.WriteLine("            2.Update/Delete account");
                                Console.WriteLine("            3.Add new accepted currency");
                                Console.WriteLine("            4.Add new service charges for same bank account");
                                Console.WriteLine("            5.Add new service charges for other bank account");
                                Console.WriteLine("            6.View account transaction");
                                Console.WriteLine("            7.Revert transaction");

                                string action = Console.ReadLine();
                                switch (action)
                                {
                                    case "1":
                                        Console.WriteLine("Enter First Name:");
                                        string firstName = Console.ReadLine();
                                        Console.WriteLine("Enter Last Name:");
                                        string lastName = Console.ReadLine();
                                        Console.WriteLine("Enter Email:");
                                        string email = Console.ReadLine();
                                        Console.WriteLine("Enter Password:");
                                        string accountPassword = Console.ReadLine();
                                        Console.WriteLine("Bank ID:");
                                        string bankId = Console.ReadLine();
                                        int returnValue = BankStaffServices.CreateAccount(firstName, lastName, email, accountPassword, bankId, currentStaff);
                                        if (returnValue == 0) Console.WriteLine("Invalid bank Id given.");
                                        else
                                        {
                                            Console.WriteLine("                         *Account created*");
                                            //Console.WriteLine("                   AccountId:{0}", newAccount.accountId);
                                        }
                                        break;
                                    case "2":
                                        Console.WriteLine("Enter account ID that you want to delete:");
                                        string delAccount = Console.ReadLine();
                                        returnValue = BankStaffServices.DeleteAccount(delAccount, currentStaff);
                                        if (returnValue == 1)
                                            Console.WriteLine("Account Deleted..!!");
                                        else Console.WriteLine("Invalid account ID given");
                                        break;
                                    case "6":
                                        Console.WriteLine("Enter account ID to view transaction:");
                                        string viewAccountTransaction = Console.ReadLine();


                                        foreach (Bank bank in Admin.AllBanks)
                                        {
                                            if (bank.BankId == currentStaff.BankId)
                                            {
                                                foreach (AccountHolder currentAccount in bank.AllAccounts)
                                                {

                                                    Console.WriteLine("SenderId                 Amount                 ReceiverID                  Transaction ID");
                                                    foreach (Transaction transaction in currentAccount.Transactions)
                                                    {
                                                        Console.WriteLine(currentAccount.AccountId + "                  " + transaction.Amount + "          " + transaction.ReceiverId + "                      " + transaction.TransactionId);
                                                    }
                                                }

                                            }

                                        }
                                        break;

                                    default:
                                        Console.WriteLine("Invalid choice");
                                        break;

                                }
                                break;
                            }
                        }
                    }



                }
                break;
            }
        case "4":
            Admin newAdmin = new Admin();
            Console.WriteLine("                               *********==========********");
            Console.WriteLine("                                 1.Create Bank");
            Console.WriteLine("                                 2.Create Staff Account");
            Console.WriteLine("                                 3.Change default Currency");
            string adminChoice = Console.ReadLine();
            switch (adminChoice)
            {
                case "1":
                    Console.WriteLine("Enter Bank Name:");
                    string newBankName = Console.ReadLine();
                    Console.WriteLine("Enter Bank Location:");
                    string bankLocation = Console.ReadLine();
                    Console.WriteLine("Enter Bank Currency:");
                    string currency = Console.ReadLine();
                    AdminServices.CreateBank(newBankName, bankLocation, currency);
                    Console.WriteLine("Bank is created successfully");
                    break;
                case "2":
                    Console.WriteLine("Enter Bank Name:");
                    string newStaffBank = Console.ReadLine();
                    Console.WriteLine("Enter user name:");
                    string userName = Console.ReadLine();
                    Console.WriteLine("Enter password:");
                    string staffPassword = Console.ReadLine();
                    int returnValue = AdminServices.CreateStaff(newStaffBank, userName, staffPassword);
                    if (returnValue == 0) Console.Write("Bank doesn't exists");
                    else Console.WriteLine("Staff added.");
                    break;
                case "3":
                    Console.WriteLine("Enter Bank Name");
                    string currencyBank = Console.ReadLine();
                    Console.WriteLine("Enter new currency:");
                    string newCurrency = Console.ReadLine();
                    AdminServices.ChangeCurrency(newCurrency, currencyBank);
                    break;
            }


            break;

        default:
            Console.WriteLine("                                 Invalid Choice");
            break;

    }


}
