using System.Security.Claims;
using EmployeeApp.Infrastructure.Contracts;
using EmployeeApp.Services.Contracts.Users;
using MapsterMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace EmployeeApp.Services.Users;

public class UserService(
    IContractUnitOfWork unitOfWork,
    IMapper mapper,
    IPasswordHasher<User> passwordHasher,
    IHttpContextAccessor httpContextAccessor)
    : IUserService
{
    private readonly IMapper _mapper = mapper;


    public async Task<bool> AuthenticateAsync(string email, string password)
    {
        User? user = await unitOfWork.Users.GetByEmailAsync(email);

        if (user == null) return false;

        PasswordVerificationResult result = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);

        if (result == PasswordVerificationResult.Failed)
            return false;

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, email),
            new Claim(ClaimTypes.Role, "Admin")
        };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        if (httpContextAccessor.HttpContext != null)
            await httpContextAccessor.HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));

        return true;
    }
}