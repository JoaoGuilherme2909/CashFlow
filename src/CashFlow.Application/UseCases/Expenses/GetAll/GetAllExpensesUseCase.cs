using AutoMapper;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionBase;

namespace CashFlow.Application.UseCases.Expenses.GetAll;

public class GetAllExpensesUseCase : IGetAllExpensesUseCase
{
    private readonly IExpenseRepository _repository;
    private readonly IMapper _mapper;

    public GetAllExpensesUseCase(IExpenseRepository repository, IMapper mapper)
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
        
        var MappedExpenses = expenses.expenses.Select(i => _mapper.Map<ResponseGetExpenseJson>(i)).ToList();

        return new ResponseGetAllExpensesJson() {count = expenses.count, Expenses = MappedExpenses, Total = expenses.total };
    }
}
