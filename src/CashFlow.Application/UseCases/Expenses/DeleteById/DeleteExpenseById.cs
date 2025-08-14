using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionBase;

namespace CashFlow.Application.UseCases.Expenses.DeleteById;

public class DeleteExpenseById : IDeleteExpenseByIdUseCase
{
    private readonly IExpenseWriteOnlyRepository _repository;
    
    public DeleteExpenseById(IExpenseWriteOnlyRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<int> Execute(long id)
    {
        var result = await _repository.Delete(id);

        if (result == 0)
        {
            throw new NotFoundExpenseException(ResourceErrorMessages.NOT_FOUND_EXPENSE);
        }
        
        return result;
    }
}