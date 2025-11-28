public class UserBalance
{
    private readonly string _filePath;

    /// <summary>
    /// Stores the file path for the user's balance based on their username.
    /// </summary>
    public UserBalance(string username)
    {
        _filePath = $"balance_{username}.txt";
    }

    public void SaveBalance(decimal balance)
    {
        File.WriteAllText(_filePath, balance.ToString());
    }

    /// <summary>
    /// Loads the user's balance from the file. If the file does not exist or is invalid, returns 0.
    /// </summary>
    public decimal LoadBalance()
    {
        if (!File.Exists(_filePath))
            return 0;

        if (decimal.TryParse(File.ReadAllText(_filePath), out decimal b))
            return b;

        return 0;
    }
}