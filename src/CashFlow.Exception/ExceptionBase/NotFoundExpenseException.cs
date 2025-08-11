using System.Net;

namespace CashFlow.Exception.ExceptionBase;

public class NotFoundExpenseException: CashFlowException
{
    public NotFoundExpenseException(string message): base(message){}
    
    public override int StatusCode => (int) HttpStatusCode.NotFound;

    public override List<string> GetErrors()
    {
        return new List<string>() {Message};
    }
}
