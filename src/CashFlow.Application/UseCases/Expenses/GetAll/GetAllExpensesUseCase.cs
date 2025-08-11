using AutoMapper;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionBase;

namespace CashFlow.Application.UseCases.Expenses.GetAll;

public class GetAllExpensesUseCase : IGetAllExpensesUseCase
{
    private readonly IExpenseReadOnlyRepository _repository;
    private readonly IMapper _mapper;

    public GetAllExpensesUseCase(IExpenseReadOnlyRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ResponseGetAllExpensesJson> Execute()
    {
        var expenses = await _repository.GetAll();
       
        if(expenses.count == 0)
        {
            throw new NotFoundExpenseException(ResourceErrorMessages.NOT_FOUND_EXPENSE);
        }
        
        return new ResponseGetAllExpensesJson()
        {
            count = expenses.count, 
            Expenses = _mapper.Map<List<ResponseGetExpenseJson>>(expenses.expenses), 
            Total = expenses.total
        };
    }
}
