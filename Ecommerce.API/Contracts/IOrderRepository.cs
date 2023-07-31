using Ecommerce.API.Entities;

namespace Ecommerce.API.Contracts;

public interface IOrderRepository
{
    void CreateOrder(Order order);
}
