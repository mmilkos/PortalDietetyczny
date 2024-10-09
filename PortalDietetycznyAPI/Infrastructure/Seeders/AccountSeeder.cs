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
    
}