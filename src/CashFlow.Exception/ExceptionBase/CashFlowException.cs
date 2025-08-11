namespace CashFlow.Exception.ExceptionBase;

public abstract class CashFlowException : SystemException
{
    public CashFlowException(string message) : base(message)
    {
    }

    public CashFlowException(){}
    
    public abstract int StatusCode { get; }
    public abstract List<string> GetErrors();
    
}
