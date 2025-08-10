using AutoMapper;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception.ExceptionBase;

namespace CashFlow.Application.UseCases.Expenses.Register;

public class RegisterExpenseUseCase : IRegisterExpenseUseCase
{
    private readonly IExpenseRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public RegisterExpenseUseCase(IExpenseRepository repository,  IUnitOfWork unitOfWork, IMapper mapper)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public async Task<ResponseRegisterExpenseJson> Execute(RequestRegisterExpenseJson request)
    {
        validate(request);

        var entity = _mapper.Map<Expense>(request);
        
        await _repository.Add(entity);
        
        await _unitOfWork.Commit();
        
        return _mapper.Map<ResponseRegisterExpenseJson>(entity);
    }

    private void validate(RequestRegisterExpenseJson request)
    {
        var validatedRequest = new RegisterExpenseValidator().Validate(request);

        if (!validatedRequest.IsValid)
        {
            var errorsList = validatedRequest.Errors.Select(e => e.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorsList);
        }
    }
}
