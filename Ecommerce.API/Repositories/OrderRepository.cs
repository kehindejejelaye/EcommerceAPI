using Ecommerce.API.Contracts;
using Ecommerce.API.Data;
using Ecommerce.API.Entities;

namespace Ecommerce.API.Repositories;

public class OrderRepository : BaseRepository<Order>, IOrderRepository
{
    public OrderRepository(EcommerceContext _ecomContext) : base(_ecomContext)
    {
    }

    public async void CreateOrder(Order order) => Create(order);
}
