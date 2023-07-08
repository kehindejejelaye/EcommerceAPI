using AutoMapper;
using Ecommerce.API.Contracts;
using Ecommerce.API.DTOs.Address;
using Ecommerce.API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [ApiController]
    [Route("api/users/{userId}/addresses")]
    public class AddressesController : ControllerBase
    {
        private readonly IRepositoryManager _repoManager;
        private readonly IMapper _mapper;
        private UserManager<ApplicationUser> _userManager;

        public AddressesController(IRepositoryManager repoManager, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _repoManager = repoManager;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpGet("{addressId}", Name = "GetAddressForUserById")]
        public async Task<ActionResult> GetAddressForUserById(string userId, string addressId)
        {
            var address = await _repoManager.Address.GetAddressForUserById(userId, addressId);

            if (address == null)
            {
                return NotFound();
            }

            var addressDto = _mapper.Map<ReadAddressDto>(address);
            return Ok(addressDto);
        }

        [HttpGet]
        public async Task<ActionResult> GetAddressesForUserByUserId(string userId)
        {
            var addresses = await _repoManager.Address.GetAddressesForUserByUserId(userId);
            var addressDtos = _mapper.Map<IEnumerable<ReadAddressDto>>(addresses);
            return Ok(addressDtos);
        }

        [HttpPost]
        public async Task<ActionResult> AddAddress(string userId, [FromBody]CreateAddressDto addressDto)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound();
            }

            var addressEntity = _mapper.Map<Address>(addressDto);
            addressEntity.UserId = userId;

            _repoManager.Address.AddAddress(addressEntity);
            await _repoManager.SaveAsync();

            var createdAddressDto = _mapper.Map<ReadAddressDto>(addressEntity);
            return CreatedAtRoute("GetAddressForUserById", new { userId, addressId = createdAddressDto.Id }, createdAddressDto);
        }

        [HttpPut("{addressId}")]
        public async Task<IActionResult> UpdateAddress(string userId, string addressId, UpdateAddressDto addressDto)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound();
            }

            var addressEntity = await _repoManager.Address.GetAddressForUserById(userId, addressId);

            if (addressEntity == null)
            {
                return NotFound();
            }

            _mapper.Map(addressDto, addressEntity);
            addressEntity.UpdatedAt = DateTime.Now;

            _repoManager.Address.UpdateAddress(addressEntity);
            await _repoManager.SaveAsync();

            return NoContent();
        }

        [HttpDelete("{addressId}")]
        public async Task<IActionResult> DeleteAddress(string userId, string addressId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound();
            }

            var addressEntity = await _repoManager.Address.GetAddressForUserById(userId, addressId);

            if (addressEntity == null)
            {
                return NotFound();
            }

            _repoManager.Address.DeleteAddress(addressEntity);
            await _repoManager.SaveAsync();

            return NoContent();
        }
    }
}
