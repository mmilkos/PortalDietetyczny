using System.ComponentModel.DataAnnotations;

namespace PortalDietetycznyAPI.Domain.Entities;

public class User : Entity
{
    public string UserName { get; private set; }
    public byte[] PasswordHash { get; private set; }
    public byte[] PasswordSalt { get; private set; }
    
    public User(string userName, byte[] passwordHash, byte[] passwordSalt)
    {
        UserName = userName;
        PasswordHash = passwordHash;
        PasswordSalt = passwordSalt;
    }

    public void ChangeCredentials(string userName, byte[] passwordHash, byte[] passwordSalt)
    {
        UserName = userName;
        PasswordHash = passwordHash;
        PasswordSalt = passwordSalt;
    }
}