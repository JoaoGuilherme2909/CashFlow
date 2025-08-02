using CashFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infraestructure.DataAccess;

public class CashFlowDbContext : DbContext
{
    public DbSet<Expense> Expenses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = "Server=localhost;Database=cashFlow;Uid=root;Pwd=cashFlower;";
        
        optionsBuilder.UseMySql(connectionString, new MySqlServerVersion(new  Version(9, 4, 0)));
    }
}