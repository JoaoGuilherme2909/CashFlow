using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Expenses;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infraestructure.DataAccess.Repositories.Expenses;

internal class ExpensesRepository : IExpenseReadOnlyRepository, IExpenseWriteOnlyRepository, IExpenseUpdateOnlyRepository
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

    public async Task<Expense?> GetById(long id)
    {
        return await _dbContext.Expenses.AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
    }
    
    public async Task<int> Delete(long id)
    {
        var expense = await _dbContext.Expenses.Where(i => i.Id == id).ExecuteDeleteAsync();

        return expense;
    }

    public async Task<int> Update(Expense expense)
    {
        var rowsAffected = await _dbContext.Expenses.Where(e => e.Id == expense.Id).ExecuteUpdateAsync(setters => 
            setters.SetProperty(p => p.Title, expense.Title)
                   .SetProperty(p => p.Description, expense.Description)
                   .SetProperty(p => p.Date, expense.Date)
                   .SetProperty(p => p.PaymentType, expense.PaymentType)
                   .SetProperty(p => p.Amount, expense.Amount)
        );

        return rowsAffected;
    }

    public async Task<List<Expense>> FilterByMonth(DateOnly date)
    {
        var startDate = new DateTime(year: date.Year, month: date.Month, day: 1).Date;

        var daysInMonth = DateTime.DaysInMonth(year: date.Year, month: date.Month);
        var endDate = new DateTime(year: date.Year, month: date.Month, day: daysInMonth, hour: 23, minute: 59, second: 59);

        return await _dbContext
                            .Expenses
                            .AsNoTracking()
                            .Where(e => e.Date >= startDate && e.Date <= endDate)
                            .OrderBy(e => e.Date)
                            .ThenBy(e => e.Title)
                            .ToListAsync();
    }
}