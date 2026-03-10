using System.Security.Claims;
using EmployeeApp.Infrastructure.Contracts;
using EmployeeApp.Services.Contracts.Users;
using EmployeeApp.Services.Contracts.Users.Dto;
using MapsterMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace EmployeeApp.Services.Users;

public class UserService : IUserService
{
    private readonly IContractUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IHttpContextAccessor _httpContextAccessor;


    public UserService(IContractUnitOfWork unitOfWork, IMapper mapper, IPasswordHasher<User> passwordHasher,
        IHttpContextAccessor httpContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _passwordHasher = passwordHasher;
        _httpContextAccessor = httpContextAccessor;
    }


    public async Task<bool> AuthenticateAsync(string email, string password)
    {
        var user = await _unitOfWork.Users.GetByEmailAsync(email);

        if (user == null) return false;

        var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);

        if (result == PasswordVerificationResult.Failed)
            return false;


        // 3. Creamos la Identidad del usuario (Claims)
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, "123"), // ID del usuario
            new Claim(ClaimTypes.Name, email),
            new Claim(ClaimTypes.Role, "Admin") // Opcional
        };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        // 4. "Firmamos" la cookie en el navegador
        await _httpContextAccessor.HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity));

        return true;
    }
}