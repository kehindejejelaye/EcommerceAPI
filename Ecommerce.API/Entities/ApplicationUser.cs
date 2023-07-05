using Microsoft.AspNetCore.Identity;
using System.Net;

namespace Ecommerce.API.Entities;

public class ApplicationUser : IdentityUser
{
    public ICollection<Order> Orders { get; set; }
    public ICollection<Address> Addresses { get; set; }

    public ICollection<Favorite> Favorites { get; set; }

    public ICollection<Review> Reviews { get; set; }

    public ICollection<ShoppingCartItem> ShoppingCartItems { get; set; }
    public ICollection<WishList> WishList { get; set; }
}
