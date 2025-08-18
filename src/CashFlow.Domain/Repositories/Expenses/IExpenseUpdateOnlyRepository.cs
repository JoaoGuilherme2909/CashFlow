using CashFlow.Domain.Entities;

namespace CashFlow.Domain.Repositories.Expenses;

public interface IExpenseUpdateOnlyRepository
{
    public Task<int> Update(Expense expense);
}
