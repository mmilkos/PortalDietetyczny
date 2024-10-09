using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Moq;
using PortalDietetycznyAPI.Application._Commands;
using PortalDietetycznyAPI.Domain.Common;
using PortalDietetycznyAPI.Domain.Interfaces;
using PortalDietetycznyAPI.Infrastructure.Context;
using PortalDietetycznyAPI.Infrastructure.Repositories;

namespace PortalDietetycznyAPI.Tests.Common;

[Collection("Database tests")]
public class BaseTests : IDisposable
{
    protected Db _dbContext;
    protected readonly IMediator _mediator;
    private IDbContextTransaction _transaction;
    protected CloudinarySettings CloudinarySettings { get; set; }
    protected  Mock<IMediator> mediatorMock;
    
    protected readonly IServiceProvider _provider;
    public IConfiguration Configuration { get; }
    
    public BaseTests()
    {
        mediatorMock = new Mock<IMediator>();
        var configBuilder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory()) // use the output directory as the base path
            .AddJsonFile("appsettings.json", optional: false);
        
        Configuration = configBuilder.Build();
        
        var services = new ServiceCollection();

        var dbContextOptions = new DbContextOptionsBuilder<Db>()
            .UseNpgsql("Server=LAPTOP-MMILKOS;Database=PortalDietetycznyDbTests;Trusted_Connection=True;TrustServerCertificate=True")
            .Options;
        
        _dbContext = new Db(dbContextOptions);
        
        services.AddScoped<IPDRepository, PdRepository>();
        services.AddSingleton(_ => _dbContext);
        services.AddMediatR(typeof(AddIngredientCommand));
        services.Configure<CloudinarySettings>(Configuration.GetSection("CloudinarySettings"));
        
        _provider = services.BuildServiceProvider();
        _mediator = _provider.GetRequiredService<IMediator>();
        
        _transaction = _dbContext.Database.BeginTransaction();

        CloudinarySettings = _provider.GetRequiredService<IOptions<CloudinarySettings>>().Value;
    }

    public void Dispose()
    {
        _transaction.Rollback();
        _transaction.Dispose();
        _dbContext.Dispose();
    }
}