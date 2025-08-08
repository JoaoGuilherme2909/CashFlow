using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Expenses;

namespace CashFlow.Infraestructure.DataAccess.Repositories.Expenses;

internal class ExpensesRepository : IExpenseRepository
{
    private readonly CashFlowDbContext _dbContext;
    
    public ExpensesRepository(CashFlowDbContext dbContext)
    {
            _dbContext = dbContext;
    }
    
    public async Task add(Expense expense)
    {
        await _dbContext.Expenses.AddAsync(expense);
    }
}