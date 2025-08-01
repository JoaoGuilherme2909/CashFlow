﻿using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception.ExceptionBase;
using PaymentType = CashFlow.Domain.Enums.PaymentType;

namespace CashFlow.Application.UseCases.Expenses.Register;

public class RegisterExpenseUseCase : IRegisterExpenseUseCase
{
    private readonly IExpenseRepository _repository;
    public RegisterExpenseUseCase(IExpenseRepository repository)
    {
        _repository = repository;
    }
    
    public ResponseRegisterExpenseJson Execute(RequestRegisterExpenseJson request)
    {
        validate(request);

        var entity = new Expense()
        {
            Amount = request.Amount, 
            Date = request.Date, 
            Description = request.Description, 
            Title = request.Title, 
            PaymentType = (PaymentType) request.paymentType
        };
        
        _repository.add(entity);
        
        return new ResponseRegisterExpenseJson() { Title = request.Title };
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
