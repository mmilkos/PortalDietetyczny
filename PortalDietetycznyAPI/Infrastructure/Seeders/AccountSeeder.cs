using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using PortalDietetycznyAPI.Domain.Entities;
using PortalDietetycznyAPI.Infrastructure.Context;

namespace PortalDietetycznyAPI.Infrastructure.Seeders;

public class AccountSeeder
{
    private readonly Db _db;
    
    public AccountSeeder(Db db)
    {
        _db = db;
    }

    public async Task SeedAsync()
    {
        var canConnect = await _db.Database.CanConnectAsync();
        var anyUsers = await _db.Users.AnyAsync();
        
        if (anyUsers == false && canConnect)
        {
            var userName = "admin";
            var password = "admin@3116";
            
            using var hmac = new HMACSHA512();
            
           var user = new User(
                userName: userName, 
                passwordHash: hmac.ComputeHash(Encoding.UTF8.GetBytes(password)),
                passwordSalt: hmac.Key);

           await _db.Users.AddAsync(user);
           await _db.SaveChangesAsync();
        }
    }
}