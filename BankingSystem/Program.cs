using ATM.Bank;

Console.WriteLine("Type 1 for Sign up, 2 for Login:");
var choice = Console.ReadLine();

/// Initialize user registration and login system
Register userStorage = new Register();
string ?username = null;

/// Handle user choice for signup or login
if (choice == "1")
{
    userStorage.Signup();
}
else if (choice == "2")
{
    username = userStorage.Login();
    if (username == null) return;
}
else
{
    Console.WriteLine("Invalid choice.");
    return;
}

/// Prompt for login if not already logged in
if (username == null)
{
    Console.WriteLine("Please login to continue...");
    username = userStorage.Login();
    if (username == null) return;
}

/// Initialize user balance storage and load starting balance
UserBalance balanceStorage = new UserBalance(username);
decimal startBalance = balanceStorage.LoadBalance();

/// Initialize banking system and key listener
BankingSystem banking = new BankingSystem(startBalance);
KeyListener listener = new KeyListener();

/// Subscribe to key listener events
listener.OnBalanceCheck += banking.ShowBalance;
listener.OnAddMoney += amount =>
{
    banking.AddMoney(amount);
    balanceStorage.SaveBalance(banking.GetBalance());
};
/// Subscribe to withdraw event
listener.OnWithdraw += amount =>
{
    banking.WithdrawMoney(amount);
    balanceStorage.SaveBalance(banking.GetBalance());
};

/// Start listening for user input
listener.StartListening();
