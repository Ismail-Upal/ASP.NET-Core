using System;
using System.Security.AccessControl;
using ATMApp.Domain.Entities;
using ATMApp.Domain.Interface;
using ATMApp.UI;

namespace ATMApp;

public class ATMApp : IUserLogin
{
    private List<UserAccount> userAccountList;
    private UserAccount selectedAccount;
    
    public void InitializeData()
    {
        userAccountList = new List<UserAccount>
        {
            new UserAccount {Id = 1, FullName = "ismail", AccountNumber = 123, cardNumber = 123456, CardPin = 1234, AccountBalance = 5000.00m, IsLocked = false, TotalLogin = 0},
            new UserAccount {Id = 2, FullName = "upal", AccountNumber = 234, cardNumber = 234567, CardPin = 2345, AccountBalance = 3000.00m, IsLocked = false, TotalLogin = 0},
            new UserAccount {Id = 3, FullName = "pathor", AccountNumber = 345, cardNumber = 345678, CardPin = 3456, AccountBalance = 4000.00m, IsLocked = false, TotalLogin = 0}
        };
    }

    public void CheckUserCardNumAndPassword()
    {
        bool isCorrectLogin = false;
        UserAccount tempUserAccount = new UserAccount();
        tempUserAccount.cardNumber = Validator.Convert<long>("your card number: ");
        tempUserAccount.CardPin = ?
    }
}