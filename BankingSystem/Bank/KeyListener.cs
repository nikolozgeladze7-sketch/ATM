internal class KeyListener
{
    /// <summary>
    /// Event triggered to check the balance.
    /// </summary>
    public event Action OnBalanceCheck = null!;
    public event Action<decimal> OnWithdraw = null!;
    public event Action<decimal> OnAddMoney = null!;

    /// <summary>
    /// Starts listening for key inputs to perform banking operations.
    /// </summary>
    public void StartListening()
    {
        while (true)
        {
            ConsoleKeyInfo key = InsertKey();

            if (key.Key == ConsoleKey.Escape)
                break;

            switch (key.Key)
            {
                case ConsoleKey.W:
                    Console.Write("Enter amount: ");
                    OnWithdraw?.Invoke(Convert.ToDecimal(Console.ReadLine()));
                    break;

                case ConsoleKey.A:
                    Console.Write("Enter amount: ");
                    OnAddMoney?.Invoke(Convert.ToDecimal(Console.ReadLine()));
                    break;

                case ConsoleKey.B:
                    OnBalanceCheck?.Invoke();
                    break;

                default:
                    Console.WriteLine("Invalid key!");
                    break;
            }
        }
    }
    /// <summary>
    /// Inserts a key and displays the menu options.
    /// </summary>
    private static ConsoleKeyInfo InsertKey()
    {
        Console.WriteLine("\nA - Add money");
        Console.WriteLine("B - Show balance");
        Console.WriteLine("W - Withdraw money");
        Console.WriteLine("ESC - Exit");
        return Console.ReadKey(true);
    }
}
