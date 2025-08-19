using CashFlow.Domain.Enums;
using CashFlow.Domain.Repositories.Expenses;
using ClosedXML.Excel;

namespace CashFlow.Application.UseCases.Expenses.Reports.Excel;

public class GenerateExpensesReportExcel : IGenerateExpensesReportExcel
{
    private const String CURRENCY_SYMBOL = "$";
    private readonly IExpenseReadOnlyRepository _repository;
    public GenerateExpensesReportExcel(IExpenseReadOnlyRepository repository)
    {
        _repository = repository;
    }

    public async Task<byte[]> Execute(DateOnly month)
    {
        var expenses = await _repository.FilterByMonth(month);

        if (expenses.Count == 0)
            return [];

        using var workBook = new XLWorkbook();

        workBook.Author = "CashFlow";

        workBook.Style.Font.FontSize = 12;
        workBook.Style.Font.FontName = "Times New Roman";

        month.ToString("Y");

        var workSheet = workBook.Worksheets.Add(month.ToString("Y"));
        
        InsertHeader(workSheet);

        var row = 2;
        foreach (var expense in expenses)
        {
            workSheet.Cell($"A{row}").Value = expense.Title;
            
            workSheet.Cell($"B{row}").Value = expense.Date;
            
            workSheet.Cell($"C{row}").Value = ConvertPaymentType(expense.PaymentType);
            
            workSheet.Cell($"D{row}").Value = expense.Amount;
            workSheet.Cell($"D{row}").Style.NumberFormat.Format = $"-{CURRENCY_SYMBOL} #,##0.00";

            workSheet.Cell($"E{row}").Value = expense.Description;
            
            row++;
        }

        workSheet.Columns().AdjustToContents();

        var file = new MemoryStream();

        workBook.SaveAs(file);

        return file.ToArray();
    }

    private string ConvertPaymentType(PaymentType payment)
    {
        return payment switch
        {
            PaymentType.Cash => "Cash",
            PaymentType.CreditCard => "Credit Card",
            PaymentType.DebitCard => "Debit Card",
            PaymentType.EletronicTransfer => "Electronic Transfer",
            _ => string.Empty
        };
    }

    private void InsertHeader(IXLWorksheet workSheet)
    {
        workSheet.Cell("A1").Value = "Title";
        workSheet.Cell("B1").Value = "Date";
        workSheet.Cell("C1").Value = "Payment type";
        workSheet.Cell("D1").Value = "Amount";
        workSheet.Cell("E1").Value = "Description";
        workSheet.Cells("A1:E1").Style.Font.Bold = true;
        workSheet.Cells("A1:E1").Style.Fill.BackgroundColor = XLColor.FromHtml("#F5C2B6");
        workSheet.Cells("A1").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        workSheet.Cells("B1").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        workSheet.Cells("C1").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        workSheet.Cells("D1").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
        workSheet.Cells("E1").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
    }
}
