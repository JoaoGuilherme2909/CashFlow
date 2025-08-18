using CashFlow.Domain.Repositories.Expenses;
using ClosedXML.Excel;

namespace CashFlow.Application.UseCases.Expenses.Reports.Excel;

public class GenerateExpensesReportExcel : IGenerateExpensesReportExcel
{
    private readonly IExpenseReadOnlyRepository _repository;
    public GenerateExpensesReportExcel(IExpenseReadOnlyRepository repository)
    {
        _repository = repository;
    }

    public async Task<byte[]> Execute(DateOnly month)
    {
        var workBook = new XLWorkbook();

        workBook.Author = "CashFlow";

        workBook.Style.Font.FontSize = 12;
        workBook.Style.Font.FontName = "Times New Roman";

        month.ToString("Y");

        var workSheet = workBook.Worksheets.Add(month.ToString("Y"));
        
        InsertHeader(workSheet);

        var file = new MemoryStream();

        workBook.SaveAs(file);

        return file.ToArray();
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
