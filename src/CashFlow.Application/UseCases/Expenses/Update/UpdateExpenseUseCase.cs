using AutoMapper;
using CashFlow.Application.Validators;
using CashFlow.Communication.Requests;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionBase;

namespace CashFlow.Application.UseCases.Expenses.Update;

public class UpdateExpenseUseCase : IUpdateExpenseUseCase
{
    private readonly IExpenseUpdateOnlyRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateExpenseUseCase(IExpenseUpdateOnlyRepository repository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task Execute(long id, RequestExpenseJson request)
    {
        validate(request);

        var expense = _mapper.Map<Expense>(request);
        expense.Id = id;
        var rowsAffected = await _repository.Update(expense);

        if(rowsAffected == 0)
            throw new NotFoundExpenseException(ResourceErrorMessages.NOT_FOUND_EXPENSE);

            await _unitOfWork.Commit();
    }

    private void validate(RequestExpenseJson request)
    {
        var validatedRequest = new RequestExpenseValidator().Validate(request);

        if (!validatedRequest.IsValid)
        {
            var errorsList = validatedRequest.Errors.Select(e => e.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorsList);
        }
    }
}
