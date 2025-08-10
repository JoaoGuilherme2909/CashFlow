using CashFlow.Communication.Enums;

namespace CashFlow.Communication.Responses;

public class ResponseGetAllExpensesJson
{
   public List<ResponseGetExpenseJson> Expenses { get; set; }
   public Decimal Total { get; set; }
   public int count { get; set; }
}
