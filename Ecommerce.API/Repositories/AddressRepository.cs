using Ecommerce.API.Contracts;
using Ecommerce.API.Data;
using Ecommerce.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.API.Repositories;

public class AddressRepository : BaseRepository<Address>, IAddressRepository
{
    public AddressRepository(EcommerceContext ecommerceContext) : base(ecommerceContext)
    {
    }

    public async Task<Address?> GetAddressForUserById(string userId, string addressId)
    {
        return await FindByCondition(a => a.Id == addressId && a.UserId == userId, trackChanges: false).Include(a => a.Country)
            .SingleOrDefaultAsync();
    }
    public async Task<Address?> GetPreferredAddressForUser(string userId)
    {
        return await FindByCondition(a => a.UserId == userId && a.IsPreferred == true, trackChanges: false).Include(a => a.Country)
            .SingleOrDefaultAsync();
    }

    public async Task<IEnumerable<Address>> GetAddressesForUserByUserId(string userId)
    {
        return await FindByCondition(a => a.UserId == userId, trackChanges: false)
            .Include(a => a.Country)
            .ToListAsync();
    }

    public void AddAddress(Address address) => Create(address);

    public void UpdateAddress(Address address) => Update(address);

    public void DeleteAddress(Address address) => Delete(address);
}
