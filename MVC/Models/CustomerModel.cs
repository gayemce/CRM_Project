using Domain;

namespace MVC.Models;

public record CustomerModel(
    Guid Id,
    string Name,
    string Email,
    string Phone,
    string Address,
    DateTime CreateDate,
    DateTime? UpdateDate);
    
public record CreateCustomerRequest(
    string Name,
    string Email,
    string Phone,
    string Address);

public record UpdateCustomerRequest(
    string Name,
    string Email,
    string Phone,
    string Address);

public static partial class ModelExtensions
{
    public static CustomerModel ToModel(this Customer customer) => new(
        customer.Id,
        customer.Name,
        customer.Email,
        customer.Phone,
        customer.Address,
        customer.CreateDate,
        customer.UpdateDate);
}
