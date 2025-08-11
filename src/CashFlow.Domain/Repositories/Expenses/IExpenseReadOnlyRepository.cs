using CashFlow.Domain.Entities;

namespace CashFlow.Domain.Repositories.Expenses;

public interface IExpenseReadOnlyRepository
{
    public Task<(IEnumerable<Expense> expenses, int count, decimal total)> GetAll();

    public Task<Expense?> GetById(long id);
}