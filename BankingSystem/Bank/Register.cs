using System.Text.Json;
using ATM.Bank;

public class Register
{
    /// <summary>
    /// Readonly field to store the file path for user data.
    /// </summary>
    private readonly string _filePath;

    /// <summary>
    /// Register constructor to initialize file path.
    /// </summary>
    public Register()
    {
        string projectDir = Directory.GetParent(Environment.CurrentDirectory)!.Parent!.Parent!.FullName;
        string dataDir = Path.Combine(projectDir, "Data");

        if (!Directory.Exists(dataDir))
            Directory.CreateDirectory(dataDir);

        _filePath = Path.Combine(dataDir, "users.json");

        if (!File.Exists(_filePath))
        {
            using var f = File.Create(_filePath); // important!
        }
    }
    /// <summary>
    /// Signup method to register a new user.
    /// </summary>
    public void Signup()
    {
        Console.WriteLine("Enter username:");
        var username = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(username))
        {
            Console.WriteLine("Username cannot be empty.");
            return;
        }

        if (UserExists(username))
        {
            Console.WriteLine("Username already exists.");
            return;
        }

        Console.WriteLine("Enter password:");
        var password = Console.ReadLine();

        var newUser = new User
        {
            Username = username,
            Password = password ?? string.Empty
        };

        List<User> users = LoadUsers();
        users.Add(newUser);
        SaveUsers(users);

        Console.WriteLine("User created successfully!");
    }

    /// <summary>
    /// Login method to authenticate an existing user.
    /// </summary>
    /// <returns></returns>
    public string Login()
    {
        Console.WriteLine("Enter username:");
        var username = Console.ReadLine();

        Console.WriteLine("Enter password:");
        var password = Console.ReadLine();

        List<User> users = LoadUsers();
        var user = users.FirstOrDefault(u => u.Username == username && u.Password == password);

        if (user == null)
        {
            Console.WriteLine("Invalid username or password!");
            return null ?? string.Empty;
        }

        Console.WriteLine($"Welcome, {username}!");
        return username ?? string.Empty;
    }

    /// <summary>
    /// UserExists method to check if a username already exists.
    /// </summary>
    public bool UserExists(string username)
    {
        List<User> users = LoadUsers();
        return users.Any(x => x.Username == username);
    }

    /// <summary>
    /// Loads the list of users from the file.
    /// </summary>
    private List<User> LoadUsers()
    {
        if (!File.Exists(_filePath)) return new List<User>();

        string json = File.ReadAllText(_filePath);
        if (string.IsNullOrWhiteSpace(json)) return new List<User>();

        return JsonSerializer.Deserialize<List<User>>(json) ?? new List<User>();
    }

    /// <summary>
    /// Saves the list of users to the file.
    /// </summary>
    private void SaveUsers(List<User> users)
    {
        string json = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_filePath, json);
    }
}