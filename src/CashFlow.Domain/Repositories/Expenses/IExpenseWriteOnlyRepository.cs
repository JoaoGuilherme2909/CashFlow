using CashFlow.Domain.Entities;

namespace CashFlow.Domain.Repositories.Expenses;

public interface IExpenseWriteOnlyRepository
{
    public Task Add(Expense expense);
}