using System;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;

public class CardHolder
{
    string cardNum;
    int pin;
    string firstName;
    string lastName;
    double balance;

    public CardHolder(string cardNum, int pin, string firstName, string lastName, double balance)
    {
        this.cardNum = cardNum;
        this.pin = pin;
        this.firstName = firstName;
        this.lastName = lastName;
        this.balance = balance;
    }

    public string getNum()
    {
        return cardNum;
    }
    public int getPin()
    {
        return pin;
    }
    public string getFirstName()
    {
        return firstName;
    }
    public string getLastName()
    {
        return lastName;
    }
    public double getBalance()
    {
        return balance;
    }

    public void setNum(string newCardNum)
    {
        cardNum = newCardNum;
    }
    public void setPin(int newPin)
    {
        pin = newPin;
    }
    public void setFirstName(string newFirstName)
    {
        firstName = newFirstName;
    }
    public void setLastName(string newLastName)
    {
        lastName = newLastName;
    }
    public void setBalance(double newBalance)
    {
        balance = newBalance;
    }



    public static void Main(String[] args)
    {
        void printOptions()
        {
            Console.WriteLine("Please choose from one of the following options.... ");
            Console.WriteLine("1. Deposite");
            Console.WriteLine("2. Withdraw");
            Console.WriteLine("3. Show Balance");
            Console.WriteLine("4. Exit");
        }

        void deposite(CardHolder currentUser)
        {
            Console.WriteLine("How much $$ would you like to deposite? ");
            double deposite = Double.Parse(Console.ReadLine());
            currentUser.setBalance(currentUser.getBalance() + deposite);
            Console.WriteLine($"Thank you for your $$ . Your new balance is: {currentUser.getBalance}");
        }

        void withdraw(CardHolder currentUser)
        {
            Console.WriteLine("How much $$ would you like to withdraw? ");
            double withdrawal = Double.Parse(Console.ReadLine());
            if (currentUser.getBalance() > withdrawal)
            {
                Console.WriteLine("Insufficient balance :(");
            }
            else
            {
                currentUser.setBalance(currentUser.getBalance() - withdrawal);
                Console.WriteLine($"You are good to go! Thank You :)");
            }
        }

        void balance(CardHolder currentUser)
        {
            Console.WriteLine($"Current balance: {currentUser.getBalance()}");
        }


        List<CardHolder> cardHolders = new List<CardHolder>();
        cardHolders.Add(new CardHolder("12", 123, "me", "te", 34.23));
        cardHolders.Add(new CardHolder("14", 153, "m3e", "t3e", 34.23));

        Console.WriteLine("Welcome you ATM");
        Console.WriteLine("Please insert your debit card: ");
        string debitCardNum = "";
        CardHolder currentUser;
        while (true)
        {
            try
            {
                debitCardNum = Console.ReadLine();
                currentUser = cardHolders.FirstOrDefault(a => a.cardNum == debitCardNum);
                if (currentUser != null) break;
                else Console.WriteLine("Card not recognized. please try again");

            }
            catch
            {
                Console.WriteLine("Card not recognized. please try again");
            }
        }
        Console.WriteLine("Please enter your PIN: ");
        int userPin;
        while (true)
        {
            try
            {
                userPin = int.Parse(Console.ReadLine());
                if (currentUser.getPin() == userPin) break;
                else Console.WriteLine("Incorrect Pin. Please try again.");
            }
            catch
            {
                Console.WriteLine("Incorrect Pin. Please try again.");
            }
        }

        Console.WriteLine($"Welcome {currentUser.getFirstName()}: ");
        int option = 0;
        do
        {
            printOptions();
            try
            {
                option = int.Parse(Console.ReadLine());
            }
            catch
            {
                if(option == 1) deposite(currentUser);
                else if(option == 2) withdraw(currentUser);
                else if(option == 3) balance(currentUser);
                else option = 0;
            }
        }
        while(option != 4);
    }
}