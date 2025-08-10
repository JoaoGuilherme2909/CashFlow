namespace CashFlow.Exception.ExceptionBase;

public class NotFoundExpenseException: CashFlowException
{
    public string Error { get; set; }

    public NotFoundExpenseException(string error)
    {
        Error = error;
    }
}
