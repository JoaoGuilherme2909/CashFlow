namespace CashFlow.Application.UseCases.Expenses.Reports.Excel;

public interface IGenerateExpensesReportExcel
{
    public Task<byte[]> Execute(DateOnly month);
}
