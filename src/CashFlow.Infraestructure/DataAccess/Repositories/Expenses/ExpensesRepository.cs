using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Expenses;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infraestructure.DataAccess.Repositories.Expenses;

internal class ExpensesRepository : IExpenseRepository
{
    private readonly CashFlowDbContext _dbContext;
    
    public ExpensesRepository(CashFlowDbContext dbContext)
    {
            _dbContext = dbContext;
    }
    
    public async Task Add(Expense expense)
    {
        await _dbContext.Expenses.AddAsync(expense);
    }

    public async Task<(IEnumerable<Expense> expenses, int count, decimal total)> GetAll()
    {
        var expenses = await _dbContext.Expenses.AsNoTracking().ToListAsync();

        var count = await _dbContext.Expenses.CountAsync();

        var total = await _dbContext.Expenses.SumAsync(i => i.Amount);

        return (expenses, count, total);
    }
}