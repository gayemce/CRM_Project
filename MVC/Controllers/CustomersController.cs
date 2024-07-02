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

    public async Task<IActionResult> Index(
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

        return View(customers);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateCustomer(CreateCustomerCommand model)
    {
        var customer = await _mediator.Send(model);
        return RedirectToAction("Index", "Customers");
    }


    [HttpGet]
    public async Task<IActionResult> Update(Guid customerId)
    {
        var customer = await _mediator.Send(new GetCustomerQuery(customerId));
        if (customer == null)
        {
            return NotFound();
        }

        var model = new UpdateCustomerCommand(customer.Id, customer.Name, customer.Email, customer.Phone, customer.Address);
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Update(UpdateCustomerCommand model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        await _mediator.Send(model);
        return RedirectToAction("Index");
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteCustomer(Guid customerId)
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

}
