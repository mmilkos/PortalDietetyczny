using Hangfire;
using MediatR;
using PortalDietetycznyAPI.Extensions;
using PortalDietetycznyAPI.Infrastructure.Seeders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication(builder.Configuration);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

/*
var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<AccountSeeder>();
await seeder.SeedAsync();
*/

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

const string origin = "https://localhost:4200";
app.UseCors(options => options
    .AllowAnyMethod()
    .AllowCredentials()
    .AllowAnyHeader()
    .WithOrigins(origin)
    .WithExposedHeaders("Content-Disposition"));

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseHangfireDashboard();



app.Run();
