using CashFlow.Communication.Enums;
using CashFlow.Communication.Requests;
using FluentValidation;

namespace CashFlow.Application.UseCases.Expenses.Register;

public class RegisterExpenseValidator : AbstractValidator<RequestRegisterExpenseJson>
{
    public RegisterExpenseValidator()
    {
        RuleFor(expense => expense.Title)
            .NotEmpty()
            .WithMessage("Title is required");

        RuleFor(expense => expense.Amount)
            .GreaterThan(0)
            .WithMessage("Amount must be greater than 0");
        
        RuleFor(expense => expense.paymentType)
            .IsInEnum()
            .WithMessage("Payment type invalid");
        
        RuleFor(expense => expense.Date)
            .LessThanOrEqualTo(DateTime.UtcNow)
            .WithMessage("You cannot set an expense for the future");
    }
}
