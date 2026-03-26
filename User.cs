using System;

public class User
{
    public string Email { get; set; }
    public string Password { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public bool IsActive { get; set; } = true;

    public int FailedLoginAttempts { get; set; } = 0;
}