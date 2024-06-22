using Application;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using Domain;
using System.Net.Mime;

namespace MVC.Controllers;
public class CustomersController : Controller
{
    private readonly IMediator _mediator;

    public CustomersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    // [Consumes(MediaTypeNames.Application.Json)]
    [Consumes("application/x-www-form-urlencoded")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CustomerModel))]
    public async Task<IActionResult> CreateCustomer(
        [FromBody] CreateCustomerRequest model)
    {
        var customer = await _mediator.Send(
            new CreateCustomerCommand(
                model.Name,
                model.Email,
                model.Phone,
                model.Address));

        return CreatedAtAction(nameof(GetCustomer), new { customerId = customer.Id }, customer.ToModel());
    }

    [HttpGet("{customerId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomerModel))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCustomer(
      [FromRoute] Guid customerId)
    {
        var customer = await _mediator.Send(
            new GetCustomerQuery(customerId));

        if (customer is null)
        {
            return NotFound();
        }

        return Ok(customer.ToModel());
    }

    [HttpPut("{customerId:guid}")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateCustomer(
    [FromRoute] Guid customerId,
    [FromBody] UpdateCustomerRequest model)
    {
        var customer = await _mediator.Send(
            new GetCustomerQuery(customerId));

        if (customer is null)
        {
            return NotFound();
        }

        await _mediator.Send(
            new UpdateCustomerCommand(
                customerId,
                model.Name,
                model.Email,
                model.Phone,
                model.Address));

        return NoContent();
    }

    [HttpDelete("{customerId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteCustomer(
    [FromRoute] Guid customerId)
    {
        var customer = await _mediator.Send(
            new GetCustomerQuery(customerId));

        if (customer is null)
        {
            return NotFound();
        }

        await _mediator.Send(
            new DeleteCustomerCommand(customerId));

        return NoContent();
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<CustomerModel>))]
    public async Task<IActionResult> ListCustomers(
     [FromQuery] string? name,
     [FromQuery] int pageSize = 10,
     [FromQuery] int pageNumber = 1)
    {
        var pageSizeLimit = 1000;

        if (pageSize > pageSizeLimit)
        {
            throw new ArgumentException(
                $"PageSize exceed the page size limit: {pageSizeLimit}", nameof(pageSize));
        }

        var customers = await _mediator.Send(
            new ListCustomerQuery(
                Name: name,
                PageSize: pageSize,
                PageNumber: pageNumber));

        Response.AddPagingHeaders(customers.Metadata);

        return Ok(customers.Select(o => o.ToModel()));
    }
}
