using Application;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using Domain;
using System.Net.Mime;

namespace MVC.Controllers;
public class ProductsController : Controller
{
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator)
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

        var products = await _mediator.Send(
            new ListProductQuery(
                Name: name,
                PageSize: pageSize,
                PageNumber: pageNumber));

        Response.AddPagingHeaders(products.Metadata);

        return View(products);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct(CreateProductCommand model)
    {
        var product = await _mediator.Send(model);
        return RedirectToAction("Index", "Products");
    }


    [HttpGet]
    public async Task<IActionResult> Update(Guid productId)
    {
        var product = await _mediator.Send(new GetProductQuery(productId));
        if (product == null)
        {
            return NotFound();
        }

        var model = new UpdateProductCommand(product.Id, product.Name, product.Price, product.Quantity, product.Description);
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Update(UpdateProductCommand model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        await _mediator.Send(model);
        return RedirectToAction("Index");
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteProduct(Guid productId)
    {
        var product = await _mediator.Send(
            new GetProductQuery(productId));

        if (product is null)
        {
            return NotFound();
        }

        await _mediator.Send(
            new DeleteProductCommand(productId));

        return NoContent();
    }

}
