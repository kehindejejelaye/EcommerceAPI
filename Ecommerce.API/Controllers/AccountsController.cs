using AutoMapper;
using Ecommerce.API.DTOs.User;
using Ecommerce.API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Ecommerce.API.Controllers;

public class AccountsController : ControllerBase
{
    private UserManager<ApplicationUser> _userManager;
    private SignInManager<ApplicationUser> _signInManager;
    private IConfiguration _config;
    private IMapper _mapper;

    public AccountsController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration config, IMapper mapper)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _config = config;
        _mapper = mapper;
    }

    private string CreateToken(string email, string id, string role)
    {
        var securityKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_config["Authentication:SecretForKey"]));

        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        List<Claim> claimsForToken = new List<Claim>
        {
            new Claim("sub", email),
            new Claim("id", id),
            new Claim("roles", role)
        };

        var jwtSecurityToken = new JwtSecurityToken(
            _config["Authentication:Issuer"],
            _config["Authentication:Audience"],
            claimsForToken,
            DateTime.UtcNow,
            DateTime.UtcNow.AddDays(30),
            signingCredentials
        );

        var tokenToReturn = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

        return tokenToReturn;
    }

    [HttpPost("register")]
    //[ValidateAntiForgeryToken]
    public async Task<ActionResult> Register([FromBody] RegisterUserDto user)
    {
        var appUser = _mapper.Map<ApplicationUser>(user);

        var createUserResult = await _userManager.CreateAsync(appUser, user.Password);

        if (createUserResult.Succeeded)
        {
            await _userManager.AddToRoleAsync(appUser, "User");
        }

        return !createUserResult.Succeeded ? BadRequest(createUserResult.Errors) : NoContent();
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login([FromBody] LoginUserDto user)
    {
        var userFromDb = await _userManager.FindByEmailAsync(user.Email);

        if (userFromDb == null) return BadRequest("Invalid Credentials");

        var loginResult = await _signInManager.PasswordSignInAsync(userFromDb.UserName, user.Password, user.RememberMe, false);

        if (!loginResult.Succeeded) return BadRequest("Invalid Credentials");

        var roles = await _userManager.GetRolesAsync(userFromDb);

        var userToReturn = _mapper.Map<UserDto>(userFromDb);

        userToReturn.Jwt = CreateToken(user.Email, userFromDb.Id, roles[0]);

        return Ok(userToReturn);
    }

}

