using Ecommerce.API.Entities;

namespace Ecommerce.API.Contracts
{
    public interface IAddressRepository
    {
        void AddAddress(Address address);
        void DeleteAddress(Address address);
        Task<IEnumerable<Address>> GetAddressesForUserByUserId(string userId);
        Task<Address?> GetAddressForUserById(string userId, string addressId);
        void UpdateAddress(Address address);
    }
}