using CashFlow.Communication.Responses;
using CashFlow.Domain.Entities;

namespace CashFlow.Application.UseCases.Expenses.GetById;

public interface IGetExpenseById
{
    public Task<Expense> Execute(long id);
}