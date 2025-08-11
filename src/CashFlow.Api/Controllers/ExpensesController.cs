using CashFlow.Application.UseCases.Expenses.GetAll;
using CashFlow.Application.UseCases.Expenses.GetById;
using CashFlow.Application.UseCases.Expenses.Register;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Repositories.Expenses;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ExpensesController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisterExpenseJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register([FromServices] IRegisterExpenseUseCase useCase,
        [FromBody] RequestRegisterExpenseJson request)
    {
        var response = await useCase.Execute(request);

        return Created(String.Empty, response);
    }

    [HttpGet]
    [ProducesResponseType(typeof(ResponseGetAllExpensesJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllExpenses([FromServices] IGetAllExpensesUseCase useCase)
    {
        var response = await useCase.Execute();

        return Ok(response);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ResponseGetExpenseJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetExpenseById([FromRoute] long id, [FromServices] IGetExpenseById useCase)
    {
        var response = await useCase.Execute(id);
        
        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTask([FromServices] IExpenseWriteOnlyRepository useCase, [FromRoute] long id)
    {
        var response = await useCase.Delete();
    }
}
 