using CashFlow.Communication.Enums;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Exception.ExceptionBase;

namespace CashFlow.Application.UseCases.Expenses.Register;

public class RegisterExpenseUseCase
{
    public ResponseRegisterExpenseJson Execute(RequestRegisterExpenseJson request)
    {
        validate(request);

        return new ResponseRegisterExpenseJson();
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
