// Services/UserService.cs
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using mvc.IRepository;
using mvc.Models;

namespace mvc.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly UserManager<Users> _userManager;

    public UserService(IUserRepository userRepository, UserManager<Users> userManager)
    {
        _userRepository = userRepository;
        _userManager = userManager;
    }

    public async Task<IEnumerable<Users>> GetAllUsersAsync()
    {
        return await _userRepository.GetAllUsersAsync();
    }

    public async Task<Users?> GetUserByIdAsync(string id)
    {
        return await _userRepository.GetUserByIdAsync(id);
    }

    public async Task AddUserAsync(Users user, string role)
    {
        await _userRepository.AddUserAsync(user);
        var roleResult = await _userManager.AddToRoleAsync(user, role);
        if (!roleResult.Succeeded)
        {
            throw new Exception("Failed to assign role to the user.");
        }
    }

    public async Task UpdateUserAsync(Users user)
    {
        // Update the user details
        await _userRepository.UpdateUserAsync(user);

        // Update the user's role
        if (!string.IsNullOrEmpty(user.Tip_utilizator))
        {
            await _userRepository.UpdateUserRoleAsync(user, user.Tip_utilizator);
        }
    }
    public async Task DeleteUserAsync(Users user)
    {
        await _userRepository.DeleteUserAsync(user);
    }

    public async Task UpdateUserAccountTypeAsync(string userId, string Tip_utilizator)
    {
        var user = await _userRepository.GetUserByIdAsync(userId);
        if (user != null)
        {
            user.Tip_utilizator = Tip_utilizator;
            await _userRepository.UpdateUserAsync(user);
        }
    }

    public async Task<Users?> GetUserWithSubscriptionAsync(string userId)
    {
        var user = await _userRepository.GetUserWithSubscriptionAsync(userId);

        if (user?.Abonament == null)
        {
            user!.Abonament = new Abonament
            {
                Tip_Abonament = "N/A",
            };
        }
        return user;
    }
}
