using System.Text.Json;
using ATM;

public class UserBalance
{
    /// <summary>
    /// readonly field to store the file path for user balance data.
    /// </summary>
    private readonly string _filePath;

    /// <summary>
    /// UserBalance constructor to initialize file path based on username.
    /// </summary>
    public UserBalance(string username)
    {
        string projectDir = Directory.GetParent(Environment.CurrentDirectory)!.Parent!.Parent!.FullName;
        string dataDir = Path.Combine(projectDir, "Data");

        if (!Directory.Exists(dataDir))
        {
            Directory.CreateDirectory(dataDir);
        }
            

        _filePath = Path.Combine(dataDir, $"balance_{username}.json");

        if (!File.Exists(_filePath))
        {
            var f = File.Create(_filePath);
            File.WriteAllText(_filePath, "0");
        }
    }

    /// <summary>
    /// Saves the balance to the file.
    /// </summary>
    public void SaveBalance(decimal balance)
    {
        File.WriteAllText(_filePath, JsonSerializer.Serialize(balance));
    }

    /// <summary>
    /// Loads the balance from the file.
    /// </summary>
    public decimal LoadBalance()
    {
        if (!File.Exists(_filePath))
        {
            return 0;
        }

        string json = File.ReadAllText(_filePath);

        if (decimal.TryParse(json, out decimal b))
        {
            return b;
        }

        try
        {
            return JsonSerializer.Deserialize<decimal>(json);
        }
        catch
        {
            return 0;
        }
    }
}