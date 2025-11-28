namespace ATM
{
    public class BankingSystem
    {
        /// <summary>
        /// Decimal field to store the current balance.
        /// </summary>
        private decimal _balance;

        public BankingSystem(decimal initialBalance)
        {
            _balance = initialBalance;
        }

        /// <summary>
        /// Shows the current balance to the console.
        /// </summary>
        public void ShowBalance()
        {
            Console.WriteLine($"Balance: {_balance}");
        }

        /// <summary>
        /// Withdraws money from the balance if sufficient funds are available.
        /// </summary>
        public void WithdrawMoney(decimal amount)
        {
            if (amount <= 0) throw new Exception("Amount must be positive.");
            if (amount > _balance) throw new Exception("Not enough balance.");

            _balance -= amount;
        }

        /// <summary>
        /// Adds money to the balance.
        /// </summary>
        public void AddMoney(decimal amount)
        {
            if (amount <= 0) throw new Exception("Amount must be positive.");
            _balance += amount;
        }

        /// <summary>
        /// Gets the current balance.
        /// </summary>
        public decimal GetBalance() => _balance;
    }
}

