namespace CashFlow.Application.UseCases.Expenses.DeleteById;

public interface IDeleteExpenseByIdUseCase
{
    public Task<int> Execute(long id);
}