using AutoMapper;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionBase;

namespace CashFlow.Application.UseCases.Expenses.GetById;

public class GetExpenseById : IGetExpenseById
{
    private readonly IExpenseReadOnlyRepository _repository;
    private readonly IMapper _mapper;
    
    public GetExpenseById(IExpenseReadOnlyRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<Expense> Execute(long id)
    {
        var expense = await _repository.GetById(id);
        
        if(expense == null)
        {
            throw new NotFoundExpenseException(ResourceErrorMessages.NOT_FOUND_EXPENSE);
        }
        
        return expense;
    }
}