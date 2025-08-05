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
    
    public void add(Expense expense)
    {
        _dbContext.Expenses.Add(expense);
    }
}