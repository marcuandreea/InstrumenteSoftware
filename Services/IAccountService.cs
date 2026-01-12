// Services/IAccountService.cs
using Microsoft.AspNetCore.Identity;
using mvc.Models;
using mvc.ViewModels;

namespace mvc.Services;
public interface IAccountService
{
    Task<Users?> AuthenticateUserAsync(string email, string password);
    Task<IdentityResult> RegisterUserAsync(RegisterViewModel model);
    Task LogoutAsync();
    Task<Users?> LoginAsync(LoginViewModel model);
    Task AddUserRoleAsync(string email, string role);
}
