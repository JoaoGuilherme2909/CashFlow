using CashFlow.Domain.Entities;

namespace CashFlow.Domain.Repositories.Expenses;

public interface IExpenseWriteOnlyRepository
{
    public Task Add(Expense expense);

    public Task<int> Delete(long id);
}