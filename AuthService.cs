using System;
using System.Collections.Generic;

public class AuthService
{
    private Dictionary<string, User> _users = new();

    public bool SignUp(string email, string password)
    {
        if (UserExists(email))
        {
            Console.WriteLine(Messages.EmailExists);
            return false;
        }

        var user = new User
        {
            Email = email,
            Password = password
        };

        _users[email] = user;

        Console.WriteLine(Messages.SignUpSuccess);
        return true;
    }

    public bool SignIn(string email, string password)
    {
        if (!UserExists(email))
        {
            Console.WriteLine(Messages.InvalidLogin);
            return false;
        }

        var user = _users[email];

        if (!user.IsActive)
        {
            Console.WriteLine(Messages.UserNotActive);
            return false;
        }

        if (user.Password != password)
        {
            user.FailedLoginAttempts++;

            if (user.FailedLoginAttempts >= 3)
            {
                user.IsActive = false;
                Console.WriteLine(Messages.TooManyAttempts);
            }
            else
            {
                Console.WriteLine(Messages.InvalidLogin);
            }

            return false;
        }

        // reset failed attempts after successful login
        user.FailedLoginAttempts = 0;

        Console.WriteLine(Messages.LoginSuccess);
        return true;
    }

    private bool UserExists(string email)
    {
        return _users.ContainsKey(email);
    }

    // ✅ MUST be inside the class
    public List<User> GetAllUsers()
    {
        return new List<User>(_users.Values);
    }
}