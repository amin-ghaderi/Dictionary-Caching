using System.Text;

// Entry point of the application
// Provides a menu-driven interface with:
// - Sign Up
// - Sign In
// - Display Users (for debugging/admin)
// - Secure password input (masked)

var authService = new AuthService();

while (true)
{
    Console.WriteLine("=== Authentication System ===");
    Console.WriteLine("1. Sign Up");
    Console.WriteLine("2. Sign In");
    Console.WriteLine("3. Show Users (Admin)");
    Console.WriteLine("4. Exit");
    Console.Write("Select an option: ");

    var choice = Console.ReadLine();
    Console.WriteLine();

    switch (choice)
    {
        case "1":
            HandleSignUp(authService);
            break;

        case "2":
            HandleSignIn(authService);
            break;

        case "3":
            ShowUsers(authService);
            break;

        case "4":
            Console.WriteLine("Exiting application...");
            return;

        default:
            Console.WriteLine("Invalid option. Please try again.");
            break;
    }

    Console.WriteLine();
}


// ----------------------
// SIGN-UP HANDLER
// ----------------------
void HandleSignUp(AuthService authService)
{
    Console.Write("Enter email: ");
    var email = Console.ReadLine();

    Console.Write("Enter password: ");
    var password = ReadPassword(); // masked input

    if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
    {
        Console.WriteLine("Email and password cannot be empty.");
        return;
    }

    authService.SignUp(email, password);
}


// ----------------------
// SIGN-IN HANDLER
// ----------------------
void HandleSignIn(AuthService authService)
{
    Console.Write("Enter email: ");
    var email = Console.ReadLine();

    Console.Write("Enter password: ");
    var password = ReadPassword(); // masked input

    if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
    {
        Console.WriteLine("Email and password cannot be empty.");
        return;
    }

    authService.SignIn(email, password);
}


// ----------------------
// SHOW USERS (DEBUG / ADMIN)
// ----------------------

// Displays all users in the system
// NOTE: In real applications, never expose passwords!
void ShowUsers(AuthService authService)
{
    var users = authService.GetAllUsers();

    if (users.Count == 0)
    {
        Console.WriteLine("No users found.");
        return;
    }

    Console.WriteLine("=== Registered Users ===");

    foreach (var user in users)
    {
        Console.WriteLine($"Email: {user.Email}");
        Console.WriteLine($"Active: {user.IsActive}");
        Console.WriteLine($"Failed Attempts: {user.FailedLoginAttempts}");
        Console.WriteLine($"Created At: {user.CreatedAt}");
        Console.WriteLine("---------------------------");
    }
}


// ----------------------
// PASSWORD MASKING
// ----------------------

// Reads password input from console while masking characters with '*'
// Handles backspace properly
string ReadPassword()
{
    var password = new StringBuilder();

    while (true)
    {
        var key = Console.ReadKey(true);

        // Enter key → finish input
        if (key.Key == ConsoleKey.Enter)
        {
            Console.WriteLine();
            break;
        }

        // Backspace → remove last character
        if (key.Key == ConsoleKey.Backspace && password.Length > 0)
        {
            password.Remove(password.Length - 1, 1);
            Console.Write("\b \b");
        }
        else if (!char.IsControl(key.KeyChar))
        {
            password.Append(key.KeyChar);
            Console.Write("*"); // mask character
        }
    }

    return password.ToString();
}